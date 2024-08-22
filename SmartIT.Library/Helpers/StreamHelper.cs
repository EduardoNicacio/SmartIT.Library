// <copyright file="StreamHelper.cs" company="Eduardo Claudio Nicacio">
// Copyright Eduardo Claudio Nicacio. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>19/10/2016</date>
// <summary>Stream helper class.</summary>

namespace SmartIT.Library.Helpers
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
            using (var ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}