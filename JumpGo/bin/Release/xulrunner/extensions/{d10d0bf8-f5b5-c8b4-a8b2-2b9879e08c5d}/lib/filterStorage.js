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
 * @fileOverview FilterStorage class responsible for managing user's subscriptions and filters.
 */

Cu.import("resource://gre/modules/Services.jsm");
Cu.import("resource://gre/modules/FileUtils.jsm");
Cu.import("resource://gre/modules/XPCOMUtils.jsm");

let {IO} = require("io");
let {Prefs} = require("prefs");
let {Filter, ActiveFilter} = require("filterClasses");
let {Subscription, SpecialSubscription, ExternalSubscription} = require("subscriptionClasses");
let {FilterNotifier} = require("filterNotifier");
let {Utils} = require("utils");

/**
 * Version number of the filter storage file format.
 * @type Integer
 */
let formatVersion = 4;

/**
 * This class reads user's filters from disk, manages them in memory and writes them back.
 * @class
 */
let FilterStorage = exports.FilterStorage =
{
  /**
   * Version number of the patterns.ini format used.
   * @type Integer
   */
  get formatVersion()
  {
    return formatVersion;
  },

  /**
   * File that the filter list has been loaded from and should be saved to
   * @type nsIFile
   */
  get sourceFile()
  {
    let file = null;
    if (Prefs.patternsfile)
    {
      // Override in place, use it instead of placing the file in the regular data dir
      file = IO.resolveFilePath(Prefs.patternsfile);
    }
    if (!file)
    {
      // Place the file in the data dir
      file = IO.resolveFilePath(Prefs.data_directory);
      if (file)
        file.append("patterns.ini");
    }
    if (!file)
    {
      // Data directory pref misconfigured? Try the default value
      try
      {
        file = IO.resolveFilePath(Services.prefs.getDefaultBranch("extensions.adblockplus.").getCharPref("data_directory"));
        if (file)
          file.append("patterns.ini");
      } catch(e) {}
    }

    if (!file)
      Cu.reportError("Adblock Plus: Failed to resolve filter file location from extensions.adblockplus.patternsfile preference");

    // Property is configurable because of the test suite.
    Object.defineProperty(this, "sourceFile", {value: file, configurable: true});
    return file;
  },

  /**
   * Will be set to true if no patterns.ini file exists.
   * @type Boolean
   */
  firstRun: false,

  /**
   * Map of properties listed in the filter storage file before the sections
   * start. Right now this should be only the format version.
   */
  fileProperties: Object.create(null),

  /**
   * List of filter subscriptions containing all filters
   * @type Subscription[]
   */
  subscriptions: [],

  /**
   * Map of subscriptions already on the list, by their URL/identifier
   * @type Object
   */
  knownSubscriptions: Object.create(null),

  /**
   * Finds the filter group that a filter should be added to by default. Will
   * return null if this group doesn't exist yet.
   */
  getGroupForFilter: function(/**Filter*/ filter) /**SpecialSubscription*/
  {
    let generalSubscription = null;
    for (let subscription of FilterStorage.subscriptions)
    {
      if (subscription instanceof SpecialSubscription && !subscription.disabled)
      {
        // Always prefer specialized subscriptions
        if (subscription.isDefaultFor(filter))
          return subscription;

        // If this is a general subscription - store it as fallback
        if (!generalSubscription && (!subscription.defaults || !subscription.defaults.length))
          generalSubscription = subscription;
      }
    }
    return generalSubscription;
  },

  /**
   * Adds a filter subscription to the list
   * @param {Subscription} subscription filter subscription to be added
   * @param {Boolean} silent  if true, no listeners will be triggered (to be used when filter list is reloaded)
   */
  addSubscription: function(subscription, silent)
  {
    if (subscription.url in FilterStorage.knownSubscriptions)
      return;

    FilterStorage.subscriptions.push(subscription);
    FilterStorage.knownSubscriptions[subscription.url] = subscription;
    addSubscriptionFilters(subscription);

    if (!silent)
      FilterNotifier.triggerListeners("subscription.added", subscription);
  },

  /**
   * Removes a filter subscription from the list
   * @param {Subscription} subscription filter subscription to be removed
   * @param {Boolean} silent  if true, no listeners will be triggered (to be used when filter list is reloaded)
   */
  removeSubscription: function(subscription, silent)
  {
    for (let i = 0; i < FilterStorage.subscriptions.length; i++)
    {
      if (FilterStorage.subscriptions[i].url == subscription.url)
      {
        removeSubscriptionFilters(subscription);

        FilterStorage.subscriptions.splice(i--, 1);
        delete FilterStorage.knownSubscriptions[subscription.url];
        if (!silent)
          FilterNotifier.triggerListeners("subscription.removed", subscription);
        return;
      }
    }
  },

  /**
   * Moves a subscription in the list to a new position.
   * @param {Subscription} subscription filter subscription to be moved
   * @param {Subscription} [insertBefore] filter subscription to insert before
   *        (if omitted the subscription will be put at the end of the list)
   */
  moveSubscription: function(subscription, insertBefore)
  {
    let currentPos = FilterStorage.subscriptions.indexOf(subscription);
    if (currentPos < 0)
      return;

    let newPos = insertBefore ? FilterStorage.subscriptions.indexOf(insertBefore) : -1;
    if (newPos < 0)
      newPos = FilterStorage.subscriptions.length;

    if (currentPos < newPos)
      newPos--;
    if (currentPos == newPos)
      return;

    FilterStorage.subscriptions.splice(currentPos, 1);
    FilterStorage.subscriptions.splice(newPos, 0, subscription);
    FilterNotifier.triggerListeners("subscription.moved", subscription);
  },

  /**
   * Replaces the list of filters in a subscription by a new list
   * @param {Subscription} subscription filter subscription to be updated
   * @param {Filter[]} filters new filter list
   */
  updateSubscriptionFilters: function(subscription, filters)
  {
    removeSubscriptionFilters(subscription);
    subscription.oldFilters = subscription.filters;
    subscription.filters = filters;
    addSubscriptionFilters(subscription);
    FilterNotifier.triggerListeners("subscription.updated", subscription);
    delete subscription.oldFilters;
  },

  /**
   * Adds a user-defined filter to the list
   * @param {Filter} filter
   * @param {SpecialSubscription} [subscription] particular group that the filter should be added to
   * @param {Integer} [position] position within the subscription at which the filter should be added
   * @param {Boolean} silent  if true, no listeners will be triggered (to be used when filter list is reloaded)
   */
  addFilter: function(filter, subscription, position, silent)
  {
    if (!subscription)
    {
      if (filter.subscriptions.some(s => s instanceof SpecialSubscription && !s.disabled))
        return;   // No need to add
      subscription = FilterStorage.getGroupForFilter(filter);
    }
    if (!subscription)
    {
      // No group for this filter exists, create one
      subscription = SpecialSubscription.createForFilter(filter);
      this.addSubscription(subscription);
      return;
    }

    if (typeof position == "undefined")
      position = subscription.filters.length;

    if (filter.subscriptions.indexOf(subscription) < 0)
      filter.subscriptions.push(subscription);
    subscription.filters.splice(position, 0, filter);
    if (!silent)
      FilterNotifier.triggerListeners("filter.added", filter, subscription, position);
  },

  /**
   * Removes a user-defined filter from the list
   * @param {Filter} filter
   * @param {SpecialSubscription} [subscription] a particular filter group that
   *      the filter should be removed from (if ommited will be removed from all subscriptions)
   * @param {Integer} [position]  position inside the filter group at which the
   *      filter should be removed (if ommited all instances will be removed)
   */
  removeFilter: function(filter, subscription, position)
  {
    let subscriptions = (subscription ? [subscription] : filter.subscriptions.slice());
    for (let i = 0; i < subscriptions.length; i++)
    {
      let subscription = subscriptions[i];
      if (subscription instanceof SpecialSubscription)
      {
        let positions = [];
        if (typeof position == "undefined")
        {
          let index = -1;
          do
          {
            index = subscription.filters.indexOf(filter, index + 1);
            if (index >= 0)
              positions.push(index);
          } while (index >= 0);
        }
        else
          positions.push(position);

        for (let j = positions.length - 1; j >= 0; j--)
        {
          let position = positions[j];
          if (subscription.filters[position] == filter)
          {
            subscription.filters.splice(position, 1);
            if (subscription.filters.indexOf(filter) < 0)
            {
              let index = filter.subscriptions.indexOf(subscription);
              if (index >= 0)
                filter.subscriptions.splice(index, 1);
            }
            FilterNotifier.triggerListeners("filter.removed", filter, subscription, position);
          }
        }
      }
    }
  },

  /**
   * Moves a user-defined filter to a new position
   * @param {Filter} filter
   * @param {SpecialSubscription} subscription filter group where the filter is located
   * @param {Integer} oldPosition current position of the filter
   * @param {Integer} newPosition new position of the filter
   */
  moveFilter: function(filter, subscription, oldPosition, newPosition)
  {
    if (!(subscription instanceof SpecialSubscription) || subscription.filters[oldPosition] != filter)
      return;

    newPosition = Math.min(Math.max(newPosition, 0), subscription.filters.length - 1);
    if (oldPosition == newPosition)
      return;

    subscription.filters.splice(oldPosition, 1);
    subscription.filters.splice(newPosition, 0, filter);
    FilterNotifier.triggerListeners("filter.moved", filter, subscription, oldPosition, newPosition);
  },

  /**
   * Increases the hit count for a filter by one
   * @param {Filter} filter
   */
  increaseHitCount: function(filter)
  {
    if (!Prefs.savestats || !(filter instanceof ActiveFilter))
      return;

    filter.hitCount++;
    filter.lastHit = Date.now();
  },

  /**
   * Resets hit count for some filters
   * @param {Filter[]} filters  filters to be reset, if null all filters will be reset
   */
  resetHitCounts: function(filters)
  {
    if (!filters)
    {
      filters = [];
      for (let text in Filter.knownFilters)
        filters.push(Filter.knownFilters[text]);
    }
    for (let filter of filters)
    {
      filter.hitCount = 0;
      filter.lastHit = 0;
    }
  },

  _loading: false,

  /**
   * Loads all subscriptions from the disk
   * @param {nsIFile} [sourceFile] File to read from
   */
  loadFromDisk: function(sourceFile)
  {
    if (this._loading)
      return;

    this._loading = true;

    let readFile = function(sourceFile, backupIndex)
    {
      let parser = new INIParser();
      IO.readFromFile(sourceFile, parser, function(e)
      {
        if (!e && parser.subscriptions.length == 0)
        {
          // No filter subscriptions in the file, this isn't right.
          e = new Error("No data in the file");
        }

        if (e)
          Cu.reportError(e);

        if (e && !explicitFile)
        {
          // Attempt to load a backup
          sourceFile = this.sourceFile;
          if (sourceFile)
          {
            let [, part1, part2] = /^(.*)(\.\w+)$/.exec(sourceFile.leafName) || [null, sourceFile.leafName, ""];

            sourceFile = sourceFile.clone();
            sourceFile.leafName = part1 + "-backup" + (++backupIndex) + part2;

            IO.statFile(sourceFile, function(e, statData)
            {
              if (!e && statData.exists)
                readFile(sourceFile, backupIndex);
              else
                doneReading(parser);
            });
            return;
          }
        }
        doneReading(parser);
      }.bind(this));
    }.bind(this);

    var doneReading = function(parser)
    {
      // Old special groups might have been converted, remove them if they are empty
      let specialMap = {"~il~": true, "~wl~": true, "~fl~": true, "~eh~": true};
      let knownSubscriptions = Object.create(null);
      for (let i = 0; i < parser.subscriptions.length; i++)
      {
        let subscription = parser.subscriptions[i];
        if (subscription instanceof SpecialSubscription && subscription.filters.length == 0 && subscription.url in specialMap)
          parser.subscriptions.splice(i--, 1);
        else
          knownSubscriptions[subscription.url] = subscription;
      }

      this.fileProperties = parser.fileProperties;
      this.subscriptions = parser.subscriptions;
      this.knownSubscriptions = knownSubscriptions;
      Filter.knownFilters = parser.knownFilters;
      Subscription.knownSubscriptions = parser.knownSubscriptions;

      if (parser.userFilters)
      {
        for (let i = 0; i < parser.userFilters.length; i++)
        {
          let filter = Filter.fromText(parser.userFilters[i]);
          this.addFilter(filter, null, undefined, true);
        }
      }

      this._loading = false;
      FilterNotifier.triggerListeners("load");

      if (sourceFile != this.sourceFile)
        this.saveToDisk();

    }.bind(this);

    let explicitFile;
    if (sourceFile)
    {
      explicitFile = true;
      readFile(sourceFile, 0);
    }
    else
    {
      explicitFile = false;
      sourceFile = FilterStorage.sourceFile;

      let callback = function(e, statData)
      {
        if (e || !statData.exists)
        {
          this.firstRun = true;
          this._loading = false;
          FilterNotifier.triggerListeners("load");
        }
        else
          readFile(sourceFile, 0);
      }.bind(this);

      if (sourceFile)
        IO.statFile(sourceFile, callback);
      else
        callback(true);
    }
  },

  _generateFilterData: function*(subscriptions)
  {
    yield "# Adblock Plus preferences";
    yield "version=" + formatVersion;

    let saved = Object.create(null);
    let buf = [];

    // Save filter data
    for (let i = 0; i < subscriptions.length; i++)
    {
      let subscription = subscriptions[i];
      for (let j = 0; j < subscription.filters.length; j++)
      {
        let filter = subscription.filters[j];
        if (!(filter.text in saved))
        {
          filter.serialize(buf);
          saved[filter.text] = filter;
          for (let k = 0; k < buf.length; k++)
            yield buf[k];
          buf.splice(0);
        }
      }
    }

    // Save subscriptions
    for (let i = 0; i < subscriptions.length; i++)
    {
      let subscription = subscriptions[i];

      yield "";

      subscription.serialize(buf);
      if (subscription.filters.length)
      {
        buf.push("", "[Subscription filters]")
        subscription.serializeFilters(buf);
      }
      for (let k = 0; k < buf.length; k++)
        yield buf[k];
      buf.splice(0);
    }
  },

  /**
   * Will be set to true if saveToDisk() is running (reentrance protection).
   * @type Boolean
   */
  _saving: false,

  /**
   * Will be set to true if a saveToDisk() call arrives while saveToDisk() is
   * already running (delayed execution).
   * @type Boolean
   */
  _needsSave: false,

  /**
   * Saves all subscriptions back to disk
   * @param {nsIFile} [targetFile] File to be written
   */
  saveToDisk: function(targetFile)
  {
    let explicitFile = true;
    if (!targetFile)
    {
      targetFile = FilterStorage.sourceFile;
      explicitFile = false;
    }
    if (!targetFile)
      return;

    if (!explicitFile && this._saving)
    {
      this._needsSave = true;
      return;
    }

    // Make sure the file's parent directory exists
    try {
      targetFile.parent.create(Ci.nsIFile.DIRECTORY_TYPE, FileUtils.PERMS_DIRECTORY);
    } catch (e) {}

    let writeFilters = function()
    {
      IO.writeToFile(targetFile, this._generateFilterData(subscriptions), function(e)
      {
        if (!explicitFile)
          this._saving = false;

        if (e)
          Cu.reportError(e);

        if (!explicitFile && this._needsSave)
        {
          this._needsSave = false;
          this.saveToDisk();
        }
        else
          FilterNotifier.triggerListeners("save");
      }.bind(this));
    }.bind(this);

    let checkBackupRequired = function(callbackNotRequired, callbackRequired)
    {
      if (explicitFile || Prefs.patternsbackups <= 0)
        callbackNotRequired();
      else
      {
        IO.statFile(targetFile, function(e, statData)
        {
          if (e || !statData.exists)
            callbackNotRequired();
          else
          {
            let [, part1, part2] = /^(.*)(\.\w+)$/.exec(targetFile.leafName) || [null, targetFile.leafName, ""];
            let newestBackup = targetFile.clone();
            newestBackup.leafName = part1 + "-backup1" + part2;
            IO.statFile(newestBackup, function(e, statData)
            {
              if (!e && (!statData.exists || (Date.now() - statData.lastModified) / 3600000 >= Prefs.patternsbackupinterval))
                callbackRequired(part1, part2)
              else
                callbackNotRequired();
            });
          }
        });
      }
    }.bind(this);

    let removeLastBackup = function(part1, part2)
    {
      let file = targetFile.clone();
      file.leafName = part1 + "-backup" + Prefs.patternsbackups + part2;
      IO.removeFile(file, (e) => renameBackup(part1, part2, Prefs.patternsbackups - 1));
    }.bind(this);

    let renameBackup = function(part1, part2, index)
    {
      if (index > 0)
      {
        let fromFile = targetFile.clone();
        fromFile.leafName = part1 + "-backup" + index + part2;

        let toName = part1 + "-backup" + (index + 1) + part2;

        IO.renameFile(fromFile, toName, (e) => renameBackup(part1, part2, index - 1));
      }
      else
      {
        let toFile = targetFile.clone();
        toFile.leafName = part1 + "-backup" + (index + 1) + part2;

        IO.copyFile(targetFile, toFile, writeFilters);
      }
    }.bind(this);

    // Do not persist external subscriptions
    let subscriptions = this.subscriptions.filter((s) => !(s instanceof ExternalSubscription));
    if (!explicitFile)
      this._saving = true;

    checkBackupRequired(writeFilters, removeLastBackup);
  },

  /**
   * Returns the list of existing backup files.
   */
  getBackupFiles: function() /**nsIFile[]*/
  {
    // TODO: This method should be asynchronous
    let result = [];

    let [, part1, part2] = /^(.*)(\.\w+)$/.exec(FilterStorage.sourceFile.leafName) || [null, FilterStorage.sourceFile.leafName, ""];
    for (let i = 1; ; i++)
    {
      let file = FilterStorage.sourceFile.clone();
      file.leafName = part1 + "-backup" + i + part2;
      if (file.exists())
        result.push(file);
      else
        break;
    }
    return result;
  }
};

/**
 * Joins subscription's filters to the subscription without any notifications.
 * @param {Subscription} subscription filter subscription that should be connected to its filters
 */
function addSubscriptionFilters(subscription)
{
  if (!(subscription.url in FilterStorage.knownSubscriptions))
    return;

  for (let filter of subscription.filters)
    filter.subscriptions.push(subscription);
}

/**
 * Removes subscription's filters from the subscription without any notifications.
 * @param {Subscription} subscription filter subscription to be removed
 */
function removeSubscriptionFilters(subscription)
{
  if (!(subscription.url in FilterStorage.knownSubscriptions))
    return;

  for (let filter of subscription.filters)
  {
    let i = filter.subscriptions.indexOf(subscription);
    if (i >= 0)
      filter.subscriptions.splice(i, 1);
  }
}

/**
 * IO.readFromFile() listener to parse filter data.
 * @constructor
 */
function INIParser()
{
  this.fileProperties = this.curObj = {};
  this.subscriptions = [];
  this.knownFilters = Object.create(null);
  this.knownSubscriptions = Object.create(null);
}
INIParser.prototype =
{
  linesProcessed: 0,
  subscriptions: null,
  knownFilters: null,
  knownSubscriptions : null,
  wantObj: true,
  fileProperties: null,
  curObj: null,
  curSection: null,
  userFilters: null,

  process: function(val)
  {
    let origKnownFilters = Filter.knownFilters;
    Filter.knownFilters = this.knownFilters;
    let origKnownSubscriptions = Subscription.knownSubscriptions;
    Subscription.knownSubscriptions = this.knownSubscriptions;
    let match;
    try
    {
      if (this.wantObj === true && (match = /^(\w+)=(.*)$/.exec(val)))
        this.curObj[match[1]] = match[2];
      else if (val === null || (match = /^\s*\[(.+)\]\s*$/.exec(val)))
      {
        if (this.curObj)
        {
          // Process current object before going to next section
          switch (this.curSection)
          {
            case "filter":
            case "pattern":
              if ("text" in this.curObj)
                Filter.fromObject(this.curObj);
              break;
            case "subscription":
              let subscription = Subscription.fromObject(this.curObj);
              if (subscription)
                this.subscriptions.push(subscription);
              break;
            case "subscription filters":
            case "subscription patterns":
              if (this.subscriptions.length)
              {
                let subscription = this.subscriptions[this.subscriptions.length - 1];
                for (let text of this.curObj)
                {
                  let filter = Filter.fromText(text);
                  subscription.filters.push(filter);
                  filter.subscriptions.push(subscription);
                }
              }
              break;
            case "user patterns":
              this.userFilters = this.curObj;
              break;
          }
        }

        if (val === null)
          return;

        this.curSection = match[1].toLowerCase();
        switch (this.curSection)
        {
          case "filter":
          case "pattern":
          case "subscription":
            this.wantObj = true;
            this.curObj = {};
            break;
          case "subscription filters":
          case "subscription patterns":
          case "user patterns":
            this.wantObj = false;
            this.curObj = [];
            break;
          default:
            this.wantObj = undefined;
            this.curObj = null;
        }
      }
      else if (this.wantObj === false && val)
        this.curObj.push(val.replace(/\\\[/g, "["));
    }
    finally
    {
      Filter.knownFilters = origKnownFilters;
      Subscription.knownSubscriptions = origKnownSubscriptions;
    }

    // Allow events to be processed every now and then.
    // Note: IO.readFromFile() will deal with the potential reentrance here.
    this.linesProcessed++;
    if (this.linesProcessed % 1000 == 0)
      Utils.yield();
  }
};
