// <copyright file="StreamHelper.cs" company="Eduardo Claudio Nicacio">
// Copyright Eduardo Claudio Nicacio. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>19/10/2016</date>
// <summary>Stream helper class.</summary>

namespace SmartIT.Library.Helpers
{
	using System.IO;
	using System.Threading.Tasks;

	/// <summary>
	/// Stream helper static class.
	/// </summary>
	public static class StreamHelper
	{
		/// <summary>
		/// Converts a stream into a byte array.
		/// </summary>
		/// <param name="input">Input stream.</param>
		/// <returns>Byte array.</returns>
		public static byte[] StreamToByteArray(Stream input)
		{
			if (input is null) 
			{
				throw new System.ArgumentNullException(nameof(input));
			}
			using (var ms = new MemoryStream())
			{
				input.CopyTo(ms);
				return ms.ToArray();
			}
		}

		/// <summary>
		/// Asynchronously converts a stream into a byte array.
		/// </summary>
		/// <param name="input">Input stream.</param>
		/// <returns>Byte array.</returns>
		public static Task<byte[]> StreamToByteArrayAsync(Stream input) 
		{
			return Task.Run(() => StreamToByteArray(input));
		}
	}
}