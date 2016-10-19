// <copyright file="Base64.cs" company="SmartIT Technologies LLC.">
// Copyright SmartIT Technologies LLC. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>19/10/2016</date>
// <summary>Stream helper static class.</summary>

namespace SmartIT.Library.Helper
{
    using System.IO;

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
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}