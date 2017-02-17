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
 * @fileOverview Element hiding implementation.
 */

Cu.import("resource://gre/modules/Services.jsm");

var {Utils} = require("utils");
var {IO} = require("io");
var {Prefs} = require("prefs");
var {ElemHideException} = require("filterClasses");
var {FilterNotifier} = require("filterNotifier");

/**
 * Lookup table, filters by their associated key
 * @type Object
 */
var filterByKey = Object.create(null);

/**
 * Lookup table, keys of the filters by filter text
 * @type Object
 */
var keyByFilter = Object.create(null);

/**
 * Lookup table, keys are known element hiding exceptions
 * @type Object
 */
var knownExceptions = Object.create(null);

/**
 * Lookup table, lists of element hiding exceptions by selector
 * @type Object
 */
var exceptions = Object.create(null);

/**
 * Currently applied stylesheet URL
 * @type nsIURI
 */
var styleURL = null;

/**
 * Element hiding component
 * @class
 */
var ElemHide = exports.ElemHide =
{
  /**
   * Indicates whether filters have been added or removed since the last apply() call.
   * @type Boolean
   */
  isDirty: false,

  /**
   * Inidicates whether the element hiding stylesheet is currently applied.
   * @type Boolean
   */
  applied: false,

  /**
   * Called on module startup.
   */
  init: function()
  {
    Prefs.addListener(function(name)
    {
      if (name == "enabled")
        ElemHide.apply();
    });
    onShutdown.add(() => ElemHide.unapply());

    let styleFile = IO.resolveFilePath(Prefs.data_directory);
    styleFile.append("elemhide.css");
    styleURL = Services.io.newFileURI(styleFile).QueryInterface(Ci.nsIFileURL);
  },

  /**
   * Removes all known filters
   */
  clear: function()
  {
    filterByKey = Object.create(null);
    keyByFilter = Object.create(null);
    knownExceptions = Object.create(null);
    exceptions = Object.create(null);
    ElemHide.isDirty = false;
    ElemHide.unapply();
  },

  /**
   * Add a new element hiding filter
   * @param {ElemHideFilter} filter
   */
  add: function(filter)
  {
    if (filter instanceof ElemHideException)
    {
      if (filter.text in knownExceptions)
        return;

      let selector = filter.selector;
      if (!(selector in exceptions))
        exceptions[selector] = [];
      exceptions[selector].push(filter);
      knownExceptions[filter.text] = true;
    }
    else
    {
      if (filter.text in keyByFilter)
        return;

      let key;
      do {
        key = Math.random().toFixed(15).substr(5);
      } while (key in filterByKey);

      filterByKey[key] = filter;
      keyByFilter[filter.text] = key;
      ElemHide.isDirty = true;
    }
  },

  /**
   * Removes an element hiding filter
   * @param {ElemHideFilter} filter
   */
  remove: function(filter)
  {
    if (filter instanceof ElemHideException)
    {
      if (!(filter.text in knownExceptions))
        return;

      let list = exceptions[filter.selector];
      let index = list.indexOf(filter);
      if (index >= 0)
        list.splice(index, 1);
      delete knownExceptions[filter.text];
    }
    else
    {
      if (!(filter.text in keyByFilter))
        return;

      let key = keyByFilter[filter.text];
      delete filterByKey[key];
      delete keyByFilter[filter.text];
      ElemHide.isDirty = true;
    }
  },

  /**
   * Checks whether an exception rule is registered for a filter on a particular
   * domain.
   */
  getException: function(/**Filter*/ filter, /**String*/ docDomain) /**ElemHideException*/
  {
    if (!(filter.selector in exceptions))
      return null;

    let list = exceptions[filter.selector];
    for (let i = list.length - 1; i >= 0; i--)
      if (list[i].isActiveOnDomain(docDomain))
        return list[i];

    return null;
  },

  /**
   * Will be set to true if apply() is running (reentrance protection).
   * @type Boolean
   */
  _applying: false,

  /**
   * Will be set to true if an apply() call arrives while apply() is already
   * running (delayed execution).
   * @type Boolean
   */
  _needsApply: false,

  /**
   * Generates stylesheet URL and applies it globally
   */
  apply: function()
  {
    if (this._applying)
    {
      this._needsApply = true;
      return;
    }

    if (!ElemHide.isDirty || !Prefs.enabled)
    {
      // Nothing changed, looks like we merely got enabled/disabled
      if (Prefs.enabled && !ElemHide.applied)
      {
        try
        {
          Utils.styleService.loadAndRegisterSheet(styleURL, Ci.nsIStyleSheetService.USER_SHEET);
          ElemHide.applied = true;
        }
        catch (e)
        {
          Cu.reportError(e);
        }
      }
      else if (!Prefs.enabled && ElemHide.applied)
      {
        ElemHide.unapply();
      }

      return;
    }

    IO.writeToFile(styleURL.file, this._generateCSSContent(), function(e)
    {
      this._applying = false;

      // _generateCSSContent is throwing NS_ERROR_NOT_AVAILABLE to indicate that
      // there are no filters. If that exception is passed through XPCOM we will
      // see a proper exception here, otherwise a number.
      let noFilters = (e == Cr.NS_ERROR_NOT_AVAILABLE || (e && e.result == Cr.NS_ERROR_NOT_AVAILABLE));
      if (noFilters)
      {
        e = null;
        IO.removeFile(styleURL.file, function(e) {});
      }
      else if (e)
        Cu.reportError(e);

      if (this._needsApply)
      {
        this._needsApply = false;
        this.apply();
      }
      else if (!e)
      {
        ElemHide.isDirty = false;

        ElemHide.unapply();

        if (!noFilters)
        {
          try
          {
            Utils.styleService.loadAndRegisterSheet(styleURL, Ci.nsIStyleSheetService.USER_SHEET);
            ElemHide.applied = true;
          }
          catch (e)
          {
            Cu.reportError(e);
          }
        }

        FilterNotifier.triggerListeners("elemhideupdate");
      }
    }.bind(this));

    this._applying = true;
  },

  _generateCSSContent: function*()
  {
    // Grouping selectors by domains
    let domains = Object.create(null);
    let hasFilters = false;
    for (let key in filterByKey)
    {
      let filter = filterByKey[key];
      let domain = filter.selectorDomain || "";

      let list;
      if (domain in domains)
        list = domains[domain];
      else
      {
        list = Object.create(null);
        domains[domain] = list;
      }
      list[filter.selector] = key;
      hasFilters = true;
    }

    if (!hasFilters)
      throw Cr.NS_ERROR_NOT_AVAILABLE;

    function escapeChar(match)
    {
      return "\\" + match.charCodeAt(0).toString(16) + " ";
    }

    // Return CSS data
    let cssTemplate = "-moz-binding: url(about:abp-elemhidehit?%ID%#dummy) !important;";
    for (let domain in domains)
    {
      let rules = [];
      let list = domains[domain];

      if (domain)
        yield ('@-moz-document domain("' + domain.split(",").join('"),domain("') + '"){').replace(/[^\x01-\x7F]/g, escapeChar);
      else
      {
        // Only allow unqualified rules on a few protocols to prevent them from blocking chrome
        yield '@-moz-document url-prefix("http://"),url-prefix("https://"),'
                  + 'url-prefix("mailbox://"),url-prefix("imap://"),'
                  + 'url-prefix("news://"),url-prefix("snews://"){';
      }

      for (let selector in list)
        yield selector.replace(/[^\x01-\x7F]/g, escapeChar) + "{" + cssTemplate.replace("%ID%", list[selector]) + "}";
      yield '}';
    }
  },

  /**
   * Unapplies current stylesheet URL
   */
  unapply: function()
  {
    if (ElemHide.applied)
    {
      try
      {
        Utils.styleService.unregisterSheet(styleURL, Ci.nsIStyleSheetService.USER_SHEET);
      }
      catch (e)
      {
        Cu.reportError(e);
      }
      ElemHide.applied = false;
    }
  },

  /**
   * Retrieves the currently applied stylesheet URL
   * @type String
   */
  get styleURL()
  {
    return ElemHide.applied ? styleURL.spec : null;
  },

  /**
   * Retrieves an element hiding filter by the corresponding protocol key
   */
  getFilterByKey: function(/**String*/ key) /**Filter*/
  {
    return (key in filterByKey ? filterByKey[key] : null);
  },

  /**
   * Returns a list of all selectors active on a particular domain (currently
   * used only in Chrome, Opera and Safari).
   */
  getSelectorsForDomain: function(/**String*/ domain, /**Boolean*/ specificOnly)
  {
    let result = [];
    let keys = Object.getOwnPropertyNames(filterByKey);
    for (let key of keys)
    {
      let filter = filterByKey[key];
      if (specificOnly && (!filter.domains || filter.domains[""]))
        continue;

      if (filter.isActiveOnDomain(domain) && !this.getException(filter, domain))
        result.push(filter.selector);
    }
    return result;
  }
};
