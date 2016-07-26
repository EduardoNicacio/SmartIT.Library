// <copyright file="AuthenticationHelper.cs" company="SmartIT Technologies LLC.">
// Copyright SmartIT Technologies LLC. All rights reserved.
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
        /// <returns>KeyValuePair.</returns>
        public static KeyValuePair<bool, string> ObterUsuarioWindows()
        {
            KeyValuePair<bool, string> retorno;
            bool usuarioAD = false;

            var wi = WindowsIdentity.GetCurrent();

            string windowsLogin = wi != null ? wi.Name : HttpContext.Current.User.Identity.Name;

            int hasDomain = windowsLogin.IndexOfAny(new char[] {'\\'}, 1, windowsLogin.Length);
            string domain = string.Empty;

            if (hasDomain > 0)
            {
                windowsLogin = windowsLogin.Remove(0, hasDomain + 1);
                domain = wi.Name.Substring(0, hasDomain + 1).Replace(@"\", string.Empty);
            }

            var domainPrincipal = ConfigurationManager.AppSettings["Domain"];

            if (domain.ToLowerInvariant().Equals(domainPrincipal.ToLowerInvariant()))
            {
                usuarioAD = true;
            }

            retorno = new KeyValuePair<bool, string>(usuarioAD, windowsLogin);

            return retorno;
        }
    }
}