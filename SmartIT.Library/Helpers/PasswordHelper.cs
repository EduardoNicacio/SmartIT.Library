// <copyright file="PasswordHelper.cs" company="Eduardo Claudio Nicacio">
// Copyright Eduardo Claudio Nicacio. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>23/08/2024</date>
// <summary>A password helper class.</summary>

namespace SmartIT.Library.Helpers
{
	using SmartIT.Library.Utilities;
	using System;
	using System.Security.Cryptography;
	using System.Threading.Tasks;
	using System.Web.UI.MobileControls.Adapters;

	/// <summary>
	/// A password helper class.
	/// </summary>
	public class PasswordHelper
	{
		private static char[] startingChars = new char[2] { '<', '&' };
		private static char[] punctuations = "!@#$%^&*()_-+=[{]};:>|./?".ToCharArray();
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
			return GeneratePasswordInternal(pwdLength, pwdLength / 2);
		}

		/// <summary>
		/// Asynchronously generates a pseudo-random password with the specified length. Useful to generate a one-time password for the "Forgot password" functionality.
		/// </summary>
		/// <param name="pwdLength">The password lenght.</param>
		/// <remarks>The password lenght must be between 1 and 128 charactetrs; ideally, between 12 and 72 characters.</remarks>
		/// <returns>The pseudo-random password with the specified lenght.</returns>
		/// <exception cref="System.ArgumentException">Throws an ArgumentException if the password lenght is out of the lower and upper limits.</exception>
		public static Task<string> GeneratePasswordAsync(int pwdLength)
		{
			return Task.Run(() => GeneratePassword(pwdLength));
		}

		/// <summary>
		/// Generates a pseudo-random salt that will be stored together with the password in the database.
		/// </summary>
		/// <returns>A pseudo-random string containing letters, digits and special chars.</returns>
		public static string GenerateSalt()
		{
			return GeneratePasswordInternal(16, 8);
		}

		/// <summary>
		/// Asynchronously generates a pseudo-random salt that will be stored together with the password in the database.
		/// </summary>
		/// <returns>A pseudo-random string containing letters, digits and special chars.</returns>
		public static Task<string> GenerateSaltAsync()
		{
			return Task.Run(() => GenerateSalt());
		}

		/// <summary>
		/// Computes the final digest for the informed password.
		/// </summary>
		/// <returns>The final digest for the informed password in hexadecimal format.</returns>
		public string ComputeDigest()
		{
			return Hash.GetSha256Hash(Hash.GetSha256Hash(_password + _salt) + _pepper);
		}

		/// <summary>
		/// Asynchronously computes the final digest for the informed password.
		/// </summary>
		/// <returns>The final digest for the informed password in hexadecimal format.</returns>
		public Task<string> ComputeDigestAsync()
		{
			return Task.Run(() => ComputeDigest());
		}

		/// <summary>
		/// Generates a random password of the specified length.
		/// </summary>
		/// <param name="length">The number of characters in the generated password. The length must be between 1 and 128 characters.</param>
		/// <param name="numberOfNonAlphanumericCharacters">The minimum number of non-alphanumeric characters (such as @, #, !, %, &, and so on) in the generated password.</param>
		/// <returns>A random password of the specified length.</returns>
		/// <exception cref="ArgumentException"></exception>
		static string GeneratePasswordInternal(int length, int numberOfNonAlphanumericCharacters) 
		{
			if (length < 1 || length > 128)
			{
				throw new ArgumentException(nameof(length));
			}

			if (numberOfNonAlphanumericCharacters > length || numberOfNonAlphanumericCharacters < 0)
			{
				throw new ArgumentException(nameof(numberOfNonAlphanumericCharacters));
			}

			string text;
			int matchIndex;
			do
			{
				byte[] array = new byte[length];
				char[] array2 = new char[length];
				int num = 0;
				new RNGCryptoServiceProvider().GetBytes(array);
				for (int i = 0; i < length; i++)
				{
					int num2 = array[i] % 87;
					if (num2 < 10)
					{
						array2[i] = (char)(48 + num2);
						continue;
					}

					if (num2 < 36)
					{
						array2[i] = (char)(65 + num2 - 10);
						continue;
					}

					if (num2 < 62)
					{
						array2[i] = (char)(97 + num2 - 36);
						continue;
					}

					array2[i] = punctuations[num2 - 62];
					num++;
				}

				if (num < numberOfNonAlphanumericCharacters)
				{
					Random random = new Random();
					for (int j = 0; j < numberOfNonAlphanumericCharacters - num; j++)
					{
						int num3;
						do
						{
							num3 = random.Next(0, length);
						}
						while (!char.IsLetterOrDigit(array2[num3]));
						array2[num3] = punctuations[random.Next(0, punctuations.Length)];
					}
				}

				text = new string(array2);
			}
			while (IsDangerousString(text, out matchIndex));
			return text;
		}

		/// <summary>
		/// Verifies if a string contains dangerous chars that would enable cross-site injection.
		/// </summary>
		/// <param name="s">The given string.</param>
		/// <param name="matchIndex">The match index, if any.</param>
		/// <returns>True, if the informed string contains dangerours chars; false instead.</returns>
		internal static bool IsDangerousString(string s, out int matchIndex)
		{
			matchIndex = 0;
			int startIndex = 0;
			while (true)
			{
				int num = s.IndexOfAny(startingChars, startIndex);
				if (num < 0)
				{
					return false;
				}

				if (num == s.Length - 1)
				{
					break;
				}

				matchIndex = num;
				switch (s[num])
				{
					case '<':
						if (IsAtoZ(s[num + 1]) || s[num + 1] == '!' || s[num + 1] == '/' || s[num + 1] == '?')
						{
							return true;
						}

						break;
					case '&':
						if (s[num + 1] == '#')
						{
							return true;
						}

						break;
				}

				startIndex = num + 1;
			}

			return false;
		}

		/// <summary>
		/// Verifies if a given char is within the A-Z boundaries.
		/// </summary>
		/// <param name="c">The informed char.</param>
		/// <returns>True, or false.</returns>
		private static bool IsAtoZ(char c)
		{
			if (c < 'a' || c > 'z')
			{
				if (c >= 'A')
				{
					return c <= 'Z';
				}

				return false;
			}

			return true;
		}
	}
}
