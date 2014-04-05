using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using umbraco;
using umbraco.MacroEngines;
using umbraco.MacroEngines.Library;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Core;
using System.Text.RegularExpressions;

namespace SectionG.Code
{

    public enum Language
    {
        En = 0,
        Fr
    }

    public static class Utilities
    {

        public static Language DefaultLanguage = Language.Fr;

        /// <summary>
        /// Gets the language from the browser. Defaults to DefaultLanguage.
        /// </summary>
        /// <returns></returns>
        public static Language GetCurrentLanguageFromBrowser()
        {
            HttpRequest Request = HttpContext.Current.Request;
            if (Request.UserLanguages != null)
            {
                var lang = Request.UserLanguages[0];
                
                if (!string.IsNullOrWhiteSpace(lang) && lang.ToLowerInvariant().Contains("fr"))
                {
                    return Language.Fr;
                }

                if (!string.IsNullOrWhiteSpace(lang) && lang.ToLowerInvariant().Contains("en"))
                {
                    return Language.En;
                }

            }
            return DefaultLanguage;
        }

        /// <summary>
        /// This takes the language code (from url) and makes sure the cookie is set accordingly.
        /// </summary>
        /// <param name="langCode"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static void ManageLanguageCookie(string langCode, HttpResponseBase response)
        {
            // Get the cookie
            var languageCookie = HttpContext.Current.Request.Cookies["Language"];
            if (languageCookie == null)
            {
                languageCookie = new HttpCookie("Language"); // Create if needed
            }

            languageCookie.Value = langCode;
            languageCookie.Expires = DateTime.Now.AddDays(10);
            response.SetCookie(languageCookie);
        }

    }

}