// <copyright file="AuthenticationHelper.cs" company="Eduardo Claudio Nicacio">
// Copyright Eduardo Claudio Nicacio. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>15/03/2015</date>
// <summary>Authentication helper.</summary>

namespace SmartIT.Library.Utility
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Security.Principal;
    using System.Web;

    /// <summary>
    /// Authentication helper.
    /// </summary>
    public static class AuthenticationHelper
    {
        /// <summary>
        /// Get Windows user.
        /// </summary>
        /// <returns>Key-Value Pair.</returns>
        public static KeyValuePair<bool, string> GetWindowsUser()
        {
            KeyValuePair<bool, string> result;
            bool activeDirectoryUser = false;

            var wi = WindowsIdentity.GetCurrent();

            string windowsLogin = wi != null ? wi.Name : HttpContext.Current.User.Identity.Name;

            int hasDomain = windowsLogin.IndexOfAny(new char[] {'\\'}, 1, windowsLogin.Length);
            string domain = string.Empty;

            if (hasDomain > 0 && !string.IsNullOrWhiteSpace(windowsLogin))
            {
                windowsLogin = windowsLogin.Remove(0, hasDomain + 1);
                domain = wi != null ? wi.Name.Substring(0, hasDomain + 1).Replace(@"\", string.Empty) : string.Empty;
            }

            var mainDomain = ConfigurationManager.AppSettings["Domain"];

            if (domain.ToUpperInvariant().Equals(mainDomain.ToUpperInvariant()))
            {
                activeDirectoryUser = true;
            }

            result = new KeyValuePair<bool, string>(activeDirectoryUser, windowsLogin);

            return result;
        }
    }
}