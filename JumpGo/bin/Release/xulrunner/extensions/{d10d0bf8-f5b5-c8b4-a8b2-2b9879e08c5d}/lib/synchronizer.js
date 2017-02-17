/*
 * This file is part of Adblock Plus <https://adblockplus.org/>,
 * Copyright (C) 2006-2016 Eyeo GmbH
 *
 * Adblock Plus is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License version 3 as
 * published by the Free Software Foundation.
 *
 * Adblock Plus is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with Adblock Plus.  If not, see <http://www.gnu.org/licenses/>.
 */

/**
 * @fileOverview Manages synchronization of filter subscriptions.
 */

Cu.import("resource://gre/modules/XPCOMUtils.jsm");
Cu.import("resource://gre/modules/Services.jsm");

var {Downloader, Downloadable,
    MILLIS_IN_SECOND, MILLIS_IN_MINUTE, MILLIS_IN_HOUR, MILLIS_IN_DAY} = require("downloader");
var {Filter, CommentFilter} = require("filterClasses");
var {FilterStorage} = require("filterStorage");
var {FilterNotifier} = require("filterNotifier");
var {Prefs} = require("prefs");
var {Subscription, DownloadableSubscription} = require("subscriptionClasses");
var {Utils} = require("utils");

var INITIAL_DELAY = 1 * MILLIS_IN_MINUTE;
var CHECK_INTERVAL = 1 * MILLIS_IN_HOUR;
var DEFAULT_EXPIRATION_INTERVAL = 5 * MILLIS_IN_DAY;

/**
 * The object providing actual downloading functionality.
 * @type Downloader
 */
var downloader = null;

/**
 * This object is responsible for downloading filter subscriptions whenever
 * necessary.
 * @class
 */
var Synchronizer = exports.Synchronizer =
{
  /**
   * Called on module startup.
   */
  init: function()
  {
    downloader = new Downloader(this._getDownloadables.bind(this), INITIAL_DELAY, CHECK_INTERVAL);
    onShutdown.add(function()
    {
      downloader.cancel();
    });

    downloader.onExpirationChange = this._onExpirationChange.bind(this);
    downloader.onDownloadStarted = this._onDownloadStarted.bind(this);
    downloader.onDownloadSuccess = this._onDownloadSuccess.bind(this);
    downloader.onDownloadError = this._onDownloadError.bind(this);
  },

  /**
   * Checks whether a subscription is currently being downloaded.
   * @param {String} url  URL of the subscription
   * @return {Boolean}
   */
  isExecuting: function(url)
  {
    return downloader.isDownloading(url);
  },

  /**
   * Starts the download of a subscription.
   * @param {DownloadableSubscription} subscription  Subscription to be downloaded
   * @param {Boolean} manual  true for a manually started download (should not trigger fallback requests)
   */
  execute: function(subscription, manual)
  {
    downloader.download(this._getDownloadable(subscription, manual));
  },

  /**
   * Yields Downloadable instances for all subscriptions that can be downloaded.
   */
  _getDownloadables: function*()
  {
    if (!Prefs.subscriptions_autoupdate)
      return;

    for (let subscription of FilterStorage.subscriptions)
    {
      if (subscription instanceof DownloadableSubscription)
        yield this._getDownloadable(subscription, false);
    }
  },

  /**
   * Creates a Downloadable instance for a subscription.
   */
  _getDownloadable: function(/**Subscription*/ subscription, /**Boolean*/ manual) /**Downloadable*/
  {
    let result = new Downloadable(subscription.url);
    if (subscription.lastDownload != subscription.lastSuccess)
      result.lastError = subscription.lastDownload * MILLIS_IN_SECOND;
    result.lastCheck = subscription.lastCheck * MILLIS_IN_SECOND;
    result.lastVersion = subscription.version;
    result.softExpiration = subscription.softExpiration * MILLIS_IN_SECOND;
    result.hardExpiration = subscription.expires * MILLIS_IN_SECOND;
    result.manual = manual;
    result.downloadCount = subscription.downloadCount;
    return result;
  },

  _onExpirationChange: function(downloadable)
  {
    let subscription = Subscription.fromURL(downloadable.url);
    subscription.lastCheck = Math.round(downloadable.lastCheck / MILLIS_IN_SECOND);
    subscription.softExpiration = Math.round(downloadable.softExpiration / MILLIS_IN_SECOND);
    subscription.expires = Math.round(downloadable.hardExpiration / MILLIS_IN_SECOND);
  },

  _onDownloadStarted: function(downloadable)
  {
    let subscription = Subscription.fromURL(downloadable.url);
    FilterNotifier.triggerListeners("subscription.downloading", subscription);
  },

  _onDownloadSuccess: function(downloadable, responseText, errorCallback, redirectCallback)
  {
    let lines = responseText.split(/[\r\n]+/);
    let match = /\[Adblock(?:\s*Plus\s*([\d\.]+)?)?\]/i.exec(lines[0]);
    if (!match)
      return errorCallback("synchronize_invalid_data");
    let minVersion = match[1];

    // Don't remove parameter comments immediately but add them to a list first,
    // they need to be considered in the checksum calculation.
    let remove = [];
    let params = {
      redirect: null,
      homepage: null,
      title: null,
      version: null,
      expires: null
    };
    for (let i = 0; i < lines.length; i++)
    {
      let match = /^\s*!\s*(\w+)\s*:\s*(.*)/.exec(lines[i]);
      if (match)
      {
        let keyword = match[1].toLowerCase();
        let value = match[2];
        if (keyword in params)
        {
          params[keyword] = value;
          remove.push(i);
        }
        else if (keyword == "checksum")
        {
          lines.splice(i--, 1);
          let checksum = Utils.generateChecksum(lines);
          if (checksum && checksum != value.replace(/=+$/, ""))
            return errorCallback("synchronize_checksum_mismatch");
        }
      }
    }

    if (params.redirect)
      return redirectCallback(params.redirect);

    // Handle redirects
    let subscription = Subscription.fromURL(downloadable.redirectURL || downloadable.url);
    if (downloadable.redirectURL && downloadable.redirectURL != downloadable.url)
    {
      let oldSubscription = Subscription.fromURL(downloadable.url);
      subscription.title = oldSubscription.title;
      subscription.disabled = oldSubscription.disabled;
      subscription.lastCheck = oldSubscription.lastCheck;

      let listed = (oldSubscription.url in FilterStorage.knownSubscriptions);
      if (listed)
        FilterStorage.removeSubscription(oldSubscription);

      delete Subscription.knownSubscriptions[oldSubscription.url];

      if (listed)
        FilterStorage.addSubscription(subscription);
    }

    // The download actually succeeded
    subscription.lastSuccess = subscription.lastDownload = Math.round(Date.now() / MILLIS_IN_SECOND);
    subscription.downloadStatus = "synchronize_ok";
    subscription.downloadCount = downloadable.downloadCount;
    subscription.errors = 0;

    // Remove lines containing parameters
    for (let i = remove.length - 1; i >= 0; i--)
      lines.splice(remove[i], 1);

    // Process parameters
    if (params.homepage)
    {
      let url;
      try
      {
        url = new URL(params.homepage);
      }
      catch (e)
      {
        url = null;
      }

      if (url && (url.protocol == "http:" || url.protocol == "https:"))
        subscription.homepage = url.href;
    }

    if (params.title)
    {
      subscription.title = params.title;
      subscription.fixedTitle = true;
    }
    else
      subscription.fixedTitle = false;

    subscription.version = (params.version ? parseInt(params.version, 10) : 0);

    let expirationInterval = DEFAULT_EXPIRATION_INTERVAL;
    if (params.expires)
    {
      let match = /^(\d+)\s*(h)?/.exec(params.expires);
      if (match)
      {
        let interval = parseInt(match[1], 10);
        if (match[2])
          expirationInterval = interval * MILLIS_IN_HOUR;
        else
          expirationInterval = interval * MILLIS_IN_DAY;
      }
    }

    let [softExpiration, hardExpiration] = downloader.processExpirationInterval(expirationInterval);
    subscription.softExpiration = Math.round(softExpiration / MILLIS_IN_SECOND);
    subscription.expires = Math.round(hardExpiration / MILLIS_IN_SECOND);

    if (minVersion)
      subscription.requiredVersion = minVersion;
    else
      delete subscription.requiredVersion;

    // Process filters
    lines.shift();
    let filters = [];
    for (let line of lines)
    {
      line = Filter.normalize(line);
      if (line)
        filters.push(Filter.fromText(line));
    }

    FilterStorage.updateSubscriptionFilters(subscription, filters);

    return undefined;
  },

  _onDownloadError: function(downloadable, downloadURL, error, channelStatus, responseStatus, redirectCallback)
  {
    let subscription = Subscription.fromURL(downloadable.url);
    subscription.lastDownload = Math.round(Date.now() / MILLIS_IN_SECOND);
    subscription.downloadStatus = error;

    // Request fallback URL if necessary - for automatic updates only
    if (!downloadable.manual)
    {
      subscription.errors++;

      if (redirectCallback && subscription.errors >= Prefs.subscriptions_fallbackerrors && /^https?:\/\//i.test(subscription.url))
      {
        subscription.errors = 0;

        let fallbackURL = Prefs.subscriptions_fallbackurl;
        let {addonVersion} = require("info");
        fallbackURL = fallbackURL.replace(/%VERSION%/g, encodeURIComponent(addonVersion));
        fallbackURL = fallbackURL.replace(/%SUBSCRIPTION%/g, encodeURIComponent(subscription.url));
        fallbackURL = fallbackURL.replace(/%URL%/g, encodeURIComponent(downloadURL));
        fallbackURL = fallbackURL.replace(/%ERROR%/g, encodeURIComponent(error));
        fallbackURL = fallbackURL.replace(/%CHANNELSTATUS%/g, encodeURIComponent(channelStatus));
        fallbackURL = fallbackURL.replace(/%RESPONSESTATUS%/g, encodeURIComponent(responseStatus));

        let request = new XMLHttpRequest();
        request.mozBackgroundRequest = true;
        request.open("GET", fallbackURL);
        request.overrideMimeType("text/plain");
        request.channel.loadFlags = request.channel.loadFlags |
                                    request.channel.INHIBIT_CACHING |
                                    request.channel.VALIDATE_ALWAYS;
        request.addEventListener("load", function(ev)
        {
          if (onShutdown.done)
            return;

          if (!(subscription.url in FilterStorage.knownSubscriptions))
            return;

          let match = /^(\d+)(?:\s+(\S+))?$/.exec(request.responseText);
          if (match && match[1] == "301" && match[2] && /^https?:\/\//i.test(match[2])) // Moved permanently
            redirectCallback(match[2]);
          else if (match && match[1] == "410")        // Gone
          {
            let data = "[Adblock]\n" + subscription.filters.map((f) => f.text).join("\n");
            redirectCallback("data:text/plain," + encodeURIComponent(data));
          }
        }, false);
        request.send(null);
      }
    }
  },
};
Synchronizer.init();
