// <copyright file="PasswordHelper.cs" company="Eduardo Claudio Nicacio">
// Copyright Eduardo Claudio Nicacio. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>23/08/2024</date>
// <summary>A password helper class.</summary>

namespace SmartIT.Library.Helpers
{
	using SmartIT.Library.Utilities;
	using System.Web.Security;

	/*
	/// table: UserId (UUID), UserName nvarchar(128), Email nvarchar(256), Password nvarchar(128), PasswordSalt nvarchar(128)
	 */
	/// <summary>
	/// A password helper class.
	/// </summary>
	public class PasswordHelper
	{
		private readonly string _password;
		private readonly string _salt;
		private readonly string _pepper;
		private const string PEPPER = "|c^.:?m)#q+(]V;}[Z(})/?-;$]+@!|^/8*_9.$&.&!(?=^!Wx?[@%+&-@b;)>N;&+*w[>$2+_$%l;+h+#zhs^{e?&=*(}X_%|:}]]}*X[+)Er%J/-=;Q0{:+=%c7:^$/:_)hxF+*){2|;(>:*N^+!_&|}B.$})?[V=[+v({-:-@9-Z$j?.[-}(@MHx+}(}Mz_S(7#4}{..>@G|!+++{+C=|_}=+r^@&$0;L*|kz-;$++/N3$=}?;%&]]*/^#^!+:*{]-x^$g{|?*))_=B@^.#%L;g|+)#[nq}?y(_(m;]S^I$*q=l-[_/?}&-!k^(+[_{Z|&:^%!_)!=p%=)=wYd-#.UP$%s1{*l%+[%?!c+7=@=.;{+M)!^}&d/]{];(&}";

		/// <summary>
		/// Initializes a new instance of the <see cref="PasswordHelper"/> class.
		/// </summary>
		/// <param name="password">The user-informed password.</param>
		/// <param name="salt">The randomly generated salt.</param>
		/// <param name="pepper">The secret key kept safe.</param>
		public PasswordHelper(string password, string salt, string pepper = null)
		{
			_password = password;
			_salt = salt;
			_pepper = pepper ?? PEPPER;
		}

		/// <summary>
		/// Generates a pseudo-random password with the specified length. Useful to generate a one-time password for the "Forgot password" functionality.
		/// </summary>
		/// <param name="pwdLength">The password lenght.</param>
		/// <remarks>The password lenght must be between 1 and 128 charactetrs; ideally, between 12 and 72 characters.</remarks>
		/// <returns>The pseudo-random password with the specified lenght.</returns>
		/// <exception cref="System.ArgumentException">Throws an ArgumentException if the password lenght is out of the lower and upper limits.</exception>
		public static string GeneratePassword(int pwdLength)
		{
			if (pwdLength < 1 || pwdLength > 128)
			{
				throw new System.ArgumentException(nameof(pwdLength));
			}
			return Membership.GeneratePassword(pwdLength, pwdLength / 2);
		}

		/// <summary>
		/// Generates a pseudo-random salt that will be stored together with the password in the database.
		/// </summary>
		/// <returns>A pseudo-random string containing letters, digits and special chars.</returns>
		public static string GenerateSalt()
		{
			return Membership.GeneratePassword(16, 8);
		}

		/// <summary>
		/// Computes the final digest for the informed password.
		/// </summary>
		/// <returns>The final digest for the informed password in hexadecimal format.</returns>
		public string ComputeDigest()
		{
			return Hash.GetSha256Hash(Hash.GetSha256Hash(_password + _salt) + _pepper);
		}
	}
}
