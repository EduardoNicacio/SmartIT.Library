// <copyright file="AuthenticationHelper.cs" company="Eduardo Claudio Nicacio">
// Copyright Eduardo Claudio Nicacio. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>15/03/2015</date>
// <summary>Authentication helper.</summary>

namespace SmartIT.Library.Utilities
{
	using System;
	using System.Collections.Generic;
	using System.Configuration;
	using System.Security.Principal;
	using System.Threading.Tasks;

	/// <summary>
	/// Authentication helper.
	/// </summary>
	public static class AuthenticationHelper
	{
		/// <summary>
		/// Gets the Windows user.
		/// </summary>
		/// <returns>Key-Value Pair.</returns>
		public static KeyValuePair<bool, string> GetWindowsUser()
		{
			KeyValuePair<bool, string> result;
			bool activeDirectoryUser = false;

			var wi = WindowsIdentity.GetCurrent();

			string windowsLogin = wi != null ? wi.Name : Environment.UserName;

			int hasDomain = windowsLogin.IndexOfAny(new char[] { '\\' }, 0, windowsLogin.Length);
			string domain = string.Empty;

			if (hasDomain > 0 && !string.IsNullOrWhiteSpace(windowsLogin))
			{
				windowsLogin = windowsLogin.Remove(0, hasDomain + 1);
				domain = wi != null ? wi.Name.Substring(0, hasDomain + 1).Replace(@"\", string.Empty) : string.Empty;
			}

			ConfigurationManager.RefreshSection("appSettings");

			var mainDomain = ConfigurationManager.AppSettings.Count > 0 ? ConfigurationManager.AppSettings["Domain"] : domain;

			if (!string.IsNullOrWhiteSpace(domain) &&
				!string.IsNullOrWhiteSpace(mainDomain) &&
				domain.ToUpperInvariant().Equals(mainDomain.ToUpperInvariant()))
			{
				activeDirectoryUser = true;
			}

			result = new KeyValuePair<bool, string>(activeDirectoryUser, windowsLogin);

			return result;
		}

		/// <summary>
		/// Asynchronously gets the Windows user.
		/// </summary>
		/// <returns>Key-Value Pair.</returns>
		public static Task<KeyValuePair<bool, string>> GetWindowsUserAsync()
		{
			return Task.Run(() => GetWindowsUser());
		}
	}
}