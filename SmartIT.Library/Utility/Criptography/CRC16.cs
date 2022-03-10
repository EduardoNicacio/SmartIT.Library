// <copyright file="Crc16.cs" company="SmartIT Technologies LLC.">
// Copyright SmartIT Technologies LLC. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>26/05/2016</date>
// <summary>CRC16 implementation.</summary>

namespace SmartIT.Library.Utility.Criptography
{
    using System;

    /// <summary>
    /// CRC16 implementation class.
    /// </summary>
    public static class Crc16
    {
        const ushort polynomial = 0xA001;
        static readonly ushort[] table = new ushort[256];

        /// <summary>
        /// Creates a new instance of the <see cref="Crc16"/> class.
        /// </summary>
        static Crc16()
        {
            ushort value;
            ushort temp;

            for (ushort i = 0; i < table.Length; ++i)
            {
                value = 0;
                temp = i;

                for (byte j = 0; j < 8; ++j)
                {
                    if (((value ^ temp) & 0x0001) != 0)
                    {
                        value = (ushort)((value >> 1) ^ polynomial);
                    }
                    else
                    {
                        value >>= 1;
                    }
                    temp >>= 1;
                }
                table[i] = value;
            }
        }

        /// <summary>
        /// Computes the checksum.
        /// </summary>
        /// <param name="bytes">Byte array.</param>
        /// <returns>Checksum.</returns>
        public static ushort ComputeHash(byte[] bytes)
        {
            ushort crc = 0;
            for (int i = 0; i < bytes.Length; ++i)
            {
                byte index = (byte)(crc ^ bytes[i]);
                crc = (ushort)((crc >> 8) ^ table[index]);
            }
            return crc;
        }

        /// <summary>
        /// Computes the checksum.
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <returns>Checksum.</returns>
        public static ushort ComputeHash(string input)
        {
            byte[] bytes = HexToBytes(input);
            ushort crc = 0;
            for (int i = 0; i < bytes.Length; ++i)
            {
                byte index = (byte)(crc ^ bytes[i]);
                crc = (ushort)((crc >> 8) ^ table[index]);
            }
            return crc;
        }

        /// <summary>
        /// Converts a string into a byte array.
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <returns>Byte array.</returns>
        public static byte[] HexToBytes(string input)
        {
            byte[] result = new byte[input.Length / 2];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Convert.ToByte(input.Substring(2 * i, 2), 16);
            }
            return result;
        }
    }
}