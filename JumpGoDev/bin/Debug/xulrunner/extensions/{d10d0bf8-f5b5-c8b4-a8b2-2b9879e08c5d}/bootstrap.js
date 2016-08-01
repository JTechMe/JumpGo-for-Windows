/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

const Cc = Components.classes;
const Ci = Components.interfaces;
const Cr = Components.results;
const Cu = Components.utils;

let {Services} = Cu.import("resource://gre/modules/Services.jsm", {});

Cu.importGlobalProperties(["atob", "btoa", "File", "URL",
    "TextDecoder", "TextEncoder"]);
Cu.importGlobalProperties(["XMLHttpRequest"]);

let addonData = null;

function startup(params, reason)
{
  addonData = params;
  Services.obs.addObserver(RequireObserver, "adblockplus-require", true);
  onShutdown.add(function()
  {
    Services.obs.removeObserver(RequireObserver, "adblockplus-require");
  });

  require("main");
}

function shutdown(params, reason)
{
  let windowNames = ["abp:subscriptionSelection", "abp:composer", "abp:filters"];
  for (let i = 0; i < windowNames.length; i++)
  {
    let enumerator = Services.wm.getEnumerator(windowNames[i]);
    while (enumerator.hasMoreElements())
    {
      let window = enumerator.getNext().QueryInterface(Ci.nsIDOMWindow);
      window.setTimeout("window.close()", 0); // Closing immediately might not work due to modal windows
      try
      {
        window.close();
      } catch(e) {}
    }
  }
  onShutdown.done = true;
  for (let i = shutdownHandlers.length - 1; i >= 0; i --)
  {
    try
    {
      shutdownHandlers[i]();
    }
    catch (e)
    {
      Cu.reportError(e);
    }
  }
  shutdownHandlers = null;

  // Make sure to release our ties to the modules even if the sandbox cannot be
  // released for some reason.
  for (let key in require.scopes)
  {
    let scope = require.scopes[key];
    let list = Object.keys(scope);
    for (let i = 0; i < list.length; i++)
      scope[list[i]] = null;
  }
  require.scopes = null;
  addonData = null;
}

function install(params, reason) {}

function uninstall(params, reason)
{
  const ADDON_UNINSTALL = 6;  // https://developer.mozilla.org/en/Extensions/Bootstrapped_extensions#Reason_constants
  if (reason == ADDON_UNINSTALL)
  {
    // Users often uninstall/reinstall extension to "fix" issues. Clear current
    // version number on uninstall to rerun first-run actions in this scenario.
    Services.prefs.clearUserPref("extensions.adblockplus.currentVersion");
  }
}
let shutdownHandlers = [];
let onShutdown =
{
  done: false,
  add: function(handler)
  {
    if (shutdownHandlers.indexOf(handler) < 0)
      shutdownHandlers.push(handler);
  },
  remove: function(handler)
  {
    let index = shutdownHandlers.indexOf(handler);
    if (index >= 0)
      shutdownHandlers.splice(index, 1);
  }
};

function require(module)
{
  let scopes = require.scopes;
  if (!(module in scopes))
  {
    if (module == "info")
    {
      let applications = {"{a23983c0-fd0e-11dc-95ff-0800200c9a66}": "fennec", "toolkit@mozilla.org": "toolkit", "{ec8030f7-c20a-464f-9b0e-13a3a9e97384}": "firefox", "dlm@emusic.com": "emusic", "{92650c4d-4b8e-4d2a-b7eb-24ecf4f6b63a}": "seamonkey", "{aa3c5121-dab2-40e2-81ca-7ea25febc110}": "fennec2", "{a79fe89b-6662-4ff4-8e88-09950ad4dfde}": "conkeror", "{aa5ca914-c309-495d-91cf-3141bbb04115}": "midbrowser", "songbird@songbirdnest.com": "songbird", "{55aba3ac-94d3-41a8-9e25-5c21fe874539}": "adblockbrowser", "prism@developer.mozilla.org": "prism", "{3550f703-e582-4d05-9a08-453d09bdfdc6}": "thunderbird"};
      let appInfo = Services.appinfo;

      scopes[module] = {};
      scopes[module].exports =
      {
        addonID: addonData.id,
        addonVersion: addonData.version,
        addonRoot: addonData.resourceURI.spec,
        addonName: "adblockplus",
        application: (appInfo.ID in applications ? applications[appInfo.ID] : "other"),
        applicationVersion: appInfo.version,
        platform: "gecko",
        platformVersion: appInfo.platformVersion
      };
    }
    else
    {
      let url = addonData.resourceURI.spec + "lib/" + module + ".js";
      scopes[module] = {
        Cc, Ci, Cr, Cu, atob, btoa, File, URL, require,
        
        onShutdown,
        
        XMLHttpRequest,
        
        exports: {}};
      Services.scriptloader.loadSubScript(url, scopes[module]);
    }
  }
  return scopes[module].exports;
}
require.scopes = Object.create(null);
require.scopes["prefs.json"] = {exports: {"defaults": {"subscriptions_fallbackerrors": 5, "subscriptions_fallbackurl": "https://adblockplus.org/getSubscription?version=%VERSION%&url=%SUBSCRIPTION%&downloadURL=%URL%&error=%ERROR%&channelStatus=%CHANNELSTATUS%&responseStatus=%RESPONSESTATUS%", "subscriptions_autoupdate": true, "clearStatsOnHistoryPurge": true, "previewimages": true, "defaultstatusbaraction": 0, "notificationurl": "https://notification.adblockplus.org/notification.json", "notifications_showui": false, "report_submiturl": "https://reports.adblockplus.org/submitReport?version=1&guid=%GUID%&lang=%LANG%", "showinstatusbar": false, "patternsbackupinterval": 24, "sendReport_key": "", "enable_key": "", "currentVersion": "0.0", "detachsidebar": false, "data_directory": "adblockplus", "blockableItemsSize": {"width": 200, "height": 200}, "savestats": true, "sidebar_key": "Accel Shift V, Accel Shift U", "notifications_ignoredcategories": [], "please_kill_startup_performance": false, "filters_key": "Accel Shift E, Accel Shift F, Accel Shift O", "whitelistschemes": "about chrome file irc moz-safe-about news resource snews x-jsd addbook cid imap mailbox nntp pop data javascript moz-icon", "frameobjects": true, "fastcollapse": false, "subscriptions_exceptionsurl": "https://easylist-downloads.adblockplus.org/exceptionrules.txt", "subscriptions_antiadblockurl": "https://easylist-downloads.adblockplus.org/antiadblockfilters.txt", "hideContributeButton": false, "flash_scrolltoitem": true, "subscriptions_exceptionscheckbox": true, "recentReports": [], "suppress_first_run_page": false, "enabled": true, "notificationdata": {}, "subscriptions_listurl": "https://adblockplus.org/subscriptions2.xml", "patternsbackups": 5, "composer_default": 2, "documentation_link": "https://adblockplus.org/redirect?link=%LINK%&lang=%LANG%", "defaulttoolbaraction": 0}, "preconfigurable": ["suppress_first_run_page"]}};
Cu.import("resource://gre/modules/XPCOMUtils.jsm");

let RequireObserver =
{
  observe: function(subject, topic, data)
  {
    if (topic == "adblockplus-require")
    {
      subject.wrappedJSObject.exports = require(data);
    }
  },

  QueryInterface: XPCOMUtils.generateQI([Ci.nsISupportsWeakReference, Ci.nsIObserver])
};