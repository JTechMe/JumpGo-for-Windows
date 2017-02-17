/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

Cu.import("resource://gre/modules/Services.jsm");

let validModifiers = Object.create(null);
validModifiers.ACCEL = null;
validModifiers.CTRL = "control";
validModifiers.CONTROL = "control";
validModifiers.SHIFT = "shift";
validModifiers.ALT = "alt";
validModifiers.META = "meta";

/**
 * Sets the correct value of validModifiers.ACCEL.
 */
function initAccelKey()
{
  validModifiers.ACCEL = "control";
  try
  {
    let accelKey = Services.prefs.getIntPref("ui.key.accelKey");
    if (accelKey == Ci.nsIDOMKeyEvent.DOM_VK_CONTROL)
      validModifiers.ACCEL = "control";
    else if (accelKey == Ci.nsIDOMKeyEvent.DOM_VK_ALT)
      validModifiers.ACCEL = "alt";
    else if (accelKey == Ci.nsIDOMKeyEvent.DOM_VK_META)
      validModifiers.ACCEL = "meta";
  }
  catch(e)
  {
    Cu.reportError(e);
  }
}

exports.KeySelector = KeySelector;

/**
 * This class provides capabilities to find and use available keyboard shortcut
 * keys.
 * @param {ChromeWindow} window   the window where to look up existing shortcut
 *                                keys
 * @constructor
 */
function KeySelector(window)
{
  this._initExistingShortcuts(window);
}
KeySelector.prototype =
{
  /**
   * Map listing existing shortcut keys as its keys.
   * @type Object
   */
  _existingShortcuts: null,

  /**
   * Sets up _existingShortcuts property for a window.
   */
  _initExistingShortcuts: function(/**ChromeWindow*/ window)
  {
    if (!validModifiers.ACCEL)
      initAccelKey();

    this._existingShortcuts = Object.create(null);

    let keys = window.document.getElementsByTagName("key");
    for (let i = 0; i < keys.length; i++)
    {
      let key = keys[i];
      let keyData =
      {
        shift: false,
        meta: false,
        alt: false,
        control: false,
        char: null,
        code: null
      };

      let keyChar = key.getAttribute("key");
      if (keyChar && keyChar.length == 1)
        keyData.char = keyChar.toUpperCase();

      let keyCode = key.getAttribute("keycode");
      if (keyCode && "DOM_" + keyCode.toUpperCase() in Ci.nsIDOMKeyEvent)
        keyData.code = Ci.nsIDOMKeyEvent["DOM_" + keyCode.toUpperCase()];

      if (!keyData.char && !keyData.code)
        continue;

      let keyModifiers = key.getAttribute("modifiers");
      if (keyModifiers)
        for (let modifier of keyModifiers.toUpperCase().match(/\w+/g))
          if (modifier in validModifiers)
            keyData[validModifiers[modifier]] = true;

      let canonical = [keyData.shift, keyData.meta, keyData.alt, keyData.control, keyData.char || keyData.code].join(" ");
      this._existingShortcuts[canonical] = true;
    }
  },

  /**
   * Selects a keyboard shortcut variant that isn't already taken,
   * parses it into an object.
   */
  selectKey: function(/**String*/ variants) /**Object*/
  {
    for (let variant of variants.split(/\s*,\s*/))
    {
      if (!variant)
        continue;

      let keyData =
      {
        shift: false,
        meta: false,
        alt: false,
        control: false,
        char: null,
        code: null,
        codeName: null
      };
      for (let part of variant.toUpperCase().split(/\s+/))
      {
        if (part in validModifiers)
          keyData[validModifiers[part]] = true;
        else if (part.length == 1)
          keyData.char = part;
        else if ("DOM_VK_" + part in Ci.nsIDOMKeyEvent)
        {
          keyData.code = Ci.nsIDOMKeyEvent["DOM_VK_" + part];
          keyData.codeName = "VK_" + part;
        }
      }

      if (!keyData.char && !keyData.code)
        continue;

      let canonical = [keyData.shift, keyData.meta, keyData.alt, keyData.control, keyData.char || keyData.code].join(" ");
      if (canonical in this._existingShortcuts)
        continue;

      return keyData;
    }

    return null;
  }
};

/**
 * Creates the text representation for a key.
 * @static
 */
KeySelector.getTextForKey = function (/**Object*/ key) /**String*/
{
  if (!key)
    return null;

  if (!("text" in key))
  {
    key.text = null;
    try
    {
      let stringBundle = Services.strings.createBundle("chrome://global-platform/locale/platformKeys.properties");
      let parts = [];
      if (key.control)
        parts.push(stringBundle.GetStringFromName("VK_CONTROL"));
      if (key.alt)
        parts.push(stringBundle.GetStringFromName("VK_ALT"));
      if (key.meta)
        parts.push(stringBundle.GetStringFromName("VK_META"));
      if (key.shift)
        parts.push(stringBundle.GetStringFromName("VK_SHIFT"));
      if (key.char)
        parts.push(key.char.toUpperCase());
      else
      {
        let stringBundle2 = Services.strings.createBundle("chrome://global/locale/keys.properties");
        parts.push(stringBundle2.GetStringFromName(key.codeName));
      }
      key.text = parts.join(stringBundle.GetStringFromName("MODIFIER_SEPARATOR"));
    }
    catch (e)
    {
      Cu.reportError(e);
      return null;
    }
  }
  return key.text;
};

/**
 * Tests whether a keypress event matches the given key.
 * @static
 */
KeySelector.matchesKey = function(/**Event*/ event, /**Object*/ key) /**Boolean*/
{
  if (event.defaultPrevented || !key)
    return false;
  if (key.shift != event.shiftKey || key.alt != event.altKey)
    return false;
  if (key.meta != event.metaKey || key.control != event.ctrlKey)
    return false;

  if (key.char && event.charCode && String.fromCharCode(event.charCode).toUpperCase() == key.char)
    return true;
  if (key.code && event.keyCode && event.keyCode == key.code)
    return true;
  return false;
};
