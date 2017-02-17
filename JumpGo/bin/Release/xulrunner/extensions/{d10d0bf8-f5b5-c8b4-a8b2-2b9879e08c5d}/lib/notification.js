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
 * @fileOverview Handles notifications.
 */

Cu.import("resource://gre/modules/Services.jsm");

var {Prefs} = require("prefs");
var {Downloader, Downloadable, MILLIS_IN_MINUTE, MILLIS_IN_HOUR, MILLIS_IN_DAY} = require("downloader");
var {Utils} = require("utils");
var {Matcher, defaultMatcher} = require("matcher");
var {Filter, RegExpFilter, WhitelistFilter} = require("filterClasses");

var INITIAL_DELAY = 1 * MILLIS_IN_MINUTE;
var CHECK_INTERVAL = 1 * MILLIS_IN_HOUR;
var EXPIRATION_INTERVAL = 1 * MILLIS_IN_DAY;
var TYPE = {
  information: 0,
  question: 1,
  critical: 2
};

var showListeners = [];
var questionListeners = {};

function getNumericalSeverity(notification)
{
  return (notification.type in TYPE ? TYPE[notification.type] : TYPE.information);
}

function saveNotificationData()
{
  // HACK: JSON values aren't saved unless they are assigned a different object.
  Prefs.notificationdata = JSON.parse(JSON.stringify(Prefs.notificationdata));
}

function localize(translations, locale)
{
  if (locale in translations)
    return translations[locale];

  let languagePart = locale.substring(0, locale.indexOf("-"));
  if (languagePart && languagePart in translations)
    return translations[languagePart];

  let defaultLocale = "en-US";
  return translations[defaultLocale];
}

/**
 * The object providing actual downloading functionality.
 * @type Downloader
 */
var downloader = null;
var localData = [];

/**
 * Regularly fetches notifications and decides which to show.
 * @class
 */
var Notification = exports.Notification =
{
  /**
   * Called on module startup.
   */
  init: function()
  {
    downloader = new Downloader(this._getDownloadables.bind(this), INITIAL_DELAY, CHECK_INTERVAL);
    downloader.onExpirationChange = this._onExpirationChange.bind(this);
    downloader.onDownloadSuccess = this._onDownloadSuccess.bind(this);
    downloader.onDownloadError = this._onDownloadError.bind(this);
    onShutdown.add(() => downloader.cancel());
  },

  /**
   * Yields a Downloadable instances for the notifications download.
   */
  _getDownloadables: function*()
  {
    let downloadable = new Downloadable(Prefs.notificationurl);
    if (typeof Prefs.notificationdata.lastError === "number")
      downloadable.lastError = Prefs.notificationdata.lastError;
    if (typeof Prefs.notificationdata.lastCheck === "number")
      downloadable.lastCheck = Prefs.notificationdata.lastCheck;
    if (typeof Prefs.notificationdata.data === "object" && "version" in Prefs.notificationdata.data)
      downloadable.lastVersion = Prefs.notificationdata.data.version;
    if (typeof Prefs.notificationdata.softExpiration === "number")
      downloadable.softExpiration = Prefs.notificationdata.softExpiration;
    if (typeof Prefs.notificationdata.hardExpiration === "number")
      downloadable.hardExpiration = Prefs.notificationdata.hardExpiration;
    if (typeof Prefs.notificationdata.downloadCount === "number")
      downloadable.downloadCount = Prefs.notificationdata.downloadCount;
    yield downloadable;
  },

  _onExpirationChange: function(downloadable)
  {
    Prefs.notificationdata.lastCheck = downloadable.lastCheck;
    Prefs.notificationdata.softExpiration = downloadable.softExpiration;
    Prefs.notificationdata.hardExpiration = downloadable.hardExpiration;
    saveNotificationData();
  },

  _onDownloadSuccess: function(downloadable, responseText, errorCallback, redirectCallback)
  {
    try
    {
      let data = JSON.parse(responseText);
      for (let notification of data.notifications)
      {
        if ("severity" in notification)
        {
          if (!("type" in notification))
            notification.type = notification.severity;
          delete notification.severity;
        }
      }
      Prefs.notificationdata.data = data;
    }
    catch (e)
    {
      Cu.reportError(e);
      errorCallback("synchronize_invalid_data");
      return;
    }

    Prefs.notificationdata.lastError = 0;
    Prefs.notificationdata.downloadStatus = "synchronize_ok";
    [Prefs.notificationdata.softExpiration, Prefs.notificationdata.hardExpiration] = downloader.processExpirationInterval(EXPIRATION_INTERVAL);
    Prefs.notificationdata.downloadCount = downloadable.downloadCount;
    saveNotificationData();

    Notification.showNext();
  },

  _onDownloadError: function(downloadable, downloadURL, error, channelStatus, responseStatus, redirectCallback)
  {
    Prefs.notificationdata.lastError = Date.now();
    Prefs.notificationdata.downloadStatus = error;
    saveNotificationData();
  },

  /**
   * Adds a listener for notifications to be shown.
   * @param {Function} listener Listener to be invoked when a notification is
   *                   to be shown
   */
  addShowListener: function(listener)
  {
    if (showListeners.indexOf(listener) == -1)
      showListeners.push(listener);
  },

  /**
   * Removes the supplied listener.
   * @param {Function} listener Listener that was added via addShowListener()
   */
  removeShowListener: function(listener)
  {
    let index = showListeners.indexOf(listener);
    if (index != -1)
      showListeners.splice(index, 1);
  },

  /**
   * Determines which notification is to be shown next.
   * @param {String} url URL to match notifications to (optional)
   * @return {Object} notification to be shown, or null if there is none
   */
  _getNextToShow: function(url)
  {
    function checkTarget(target, parameter, name, version)
    {
      let minVersionKey = parameter + "MinVersion";
      let maxVersionKey = parameter + "MaxVersion";
      return !((parameter in target && target[parameter] != name) ||
               (minVersionKey in target && Services.vc.compare(version, target[minVersionKey]) < 0) ||
               (maxVersionKey in target && Services.vc.compare(version, target[maxVersionKey]) > 0));
    }

    let remoteData = [];
    if (typeof Prefs.notificationdata.data == "object" && Prefs.notificationdata.data.notifications instanceof Array)
      remoteData = Prefs.notificationdata.data.notifications;

    let notifications = localData.concat(remoteData);
    if (notifications.length === 0)
      return null;

    let {addonName, addonVersion, application, applicationVersion, platform, platformVersion} = require("info");
    let notificationToShow = null;
    for (let notification of notifications)
    {
      if (typeof notification.type === "undefined" || notification.type !== "critical")
      {
        let shown = Prefs.notificationdata.shown;
        if (shown instanceof Array && shown.indexOf(notification.id) != -1)
          continue;
        if (Prefs.notifications_ignoredcategories.indexOf("*") != -1)
          continue;
      }

      if (typeof url === "string" || notification.urlFilters instanceof Array)
      {
        if (Prefs.enabled && typeof url === "string" && notification.urlFilters instanceof Array)
        {
          let host;
          try
          {
            host = new URL(url).hostname;
          }
          catch (e)
          {
            host = "";
          }

          let exception = defaultMatcher.matchesAny(url, RegExpFilter.typeMap.DOCUMENT, host, false, null);
          if (exception instanceof WhitelistFilter)
            continue;

          let matcher = new Matcher();
          for (let urlFilter of notification.urlFilters)
            matcher.add(Filter.fromText(urlFilter));
          if (!matcher.matchesAny(url, RegExpFilter.typeMap.DOCUMENT, host, false, null))
            continue;
        }
        else
          continue;
      }

      if (notification.targets instanceof Array)
      {
        let match = false;
        for (let target of notification.targets)
        {
          if (checkTarget(target, "extension", addonName, addonVersion) &&
              checkTarget(target, "application", application, applicationVersion) &&
              checkTarget(target, "platform", platform, platformVersion))
          {
            match = true;
            break;
          }
        }
        if (!match)
          continue;
      }

      if (!notificationToShow
          || getNumericalSeverity(notification) > getNumericalSeverity(notificationToShow))
        notificationToShow = notification;
    }

    return notificationToShow;
  },

  /**
   * Invokes the listeners added via addShowListener() with the next
   * notification to be shown.
   * @param {String} url URL to match notifications to (optional)
   */
  showNext: function(url)
  {
    let notification = Notification._getNextToShow(url);
    if (notification)
      for (let showListener of showListeners)
        showListener(notification);
  },

  /**
   * Marks a notification as shown.
   * @param {String} id ID of the notification to be marked as shown
   */
  markAsShown: function(id)
  {
    var data = Prefs.notificationdata;

    if (!(data.shown instanceof Array))
      data.shown = [];
    if (data.shown.indexOf(id) != -1)
      return;

    data.shown.push(id);
    saveNotificationData();
  },

  /**
   * Localizes the texts of the supplied notification.
   * @param {Object} notification notification to translate
   * @param {String} locale the target locale (optional, defaults to the
   *                        application locale)
   * @return {Object} the translated texts
   */
  getLocalizedTexts: function(notification, locale)
  {
    locale = locale || Utils.appLocale;
    let textKeys = ["title", "message"];
    let localizedTexts = [];
    for (let key of textKeys)
    {
      if (key in notification)
      {
        if (typeof notification[key] == "string")
          localizedTexts[key] = notification[key];
        else
          localizedTexts[key] = localize(notification[key], locale);
      }
    }
    return localizedTexts;
  },

  /**
   * Adds a local notification.
   * @param {Object} notification notification to add
   */
  addNotification: function(notification)
  {
    if (localData.indexOf(notification) == -1)
      localData.push(notification);
  },

  /**
   * Removes an existing local notification.
   * @param {Object} notification notification to remove
   */
  removeNotification: function(notification)
  {
    let index = localData.indexOf(notification);
    if (index > -1)
      localData.splice(index, 1);
  },

  /**
   * Adds a listener for question-type notifications
   */
  addQuestionListener: function(/**string*/ id, /**function(approved)*/ listener)
  {
    if (!(id in questionListeners))
      questionListeners[id] = [];
    if (questionListeners[id].indexOf(listener) === -1)
      questionListeners[id].push(listener);
  },

  /**
   * Removes a listener that was previously added via addQuestionListener
   */
  removeQuestionListener: function(/**string*/ id, /**function(approved)*/ listener)
  {
    if (!(id in questionListeners))
      return;
    let index = questionListeners[id].indexOf(listener);
    if (index > -1)
      questionListeners[id].splice(index, 1);
    if (questionListeners[id].length === 0)
      delete questionListeners[id];
  },

  /**
   * Notifies question listeners about interactions with a notification
   * @param {String} id notification ID
   * @param {Boolean} approved indicator whether notification has been approved or not
   */
  triggerQuestionListeners: function(id, approved)
  {
    if (!(id in questionListeners))
      return;
    let listeners = questionListeners[id];
    for (let listener of listeners)
      listener(approved);
  },

  /**
   * Toggles whether notifications of a specific category should be ignored
   * @param {String} category notification category identifier
   * @param {Boolean} [forceValue] force specified value
   */
  toggleIgnoreCategory: function(category, forceValue)
  {
    let categories = Prefs.notifications_ignoredcategories;
    let index = categories.indexOf(category);
    if (index == -1 && forceValue !== false)
    {
      categories.push(category);
      Prefs.notifications_showui = true;
    }
    else if (index != -1 && forceValue !== true)
      categories.splice(index, 1);

    // HACK: JSON values aren't saved unless they are assigned a different object.
    Prefs.notifications_ignoredcategories = JSON.parse(JSON.stringify(categories));
  }
};
Notification.init();
