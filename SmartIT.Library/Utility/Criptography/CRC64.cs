// <copyright file="Crc64.cs" company="SmartIT Technologies LLC.">
// Copyright SmartIT Technologies LLC. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>26/05/2016</date>
// <summary>CRC64 implementation. Includes ISO complient solution.</summary>

namespace SmartIT.Library.Utility.Criptography
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;

    /// <summary>
    /// Implements a 64-bit CRC hash algorithm for a given polynomial.
    /// </summary>
    /// <remarks>
    /// For ISO 3309 compliant 64-bit CRC's use Crc64Iso.
    /// </remarks>
    public class Crc64 : HashAlgorithm
    {
        /// <summary>
        /// Default seed.
        /// </summary>
        public const UInt64 DefaultSeed = 0x0;

        readonly UInt64[] table;

        readonly UInt64 seed;
        UInt64 hash;

        /// <summary>
        /// Creates a new instance of the <see cref="Crc64"/> class.
        /// </summary>
        /// <param name="polynomial"></param>
        public Crc64(UInt64 polynomial)
            : this(polynomial, DefaultSeed)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Crc64"/> class.
        /// </summary>
        /// <param name="polynomial">Custom polynomial.</param>
        /// <param name="seed">Custom seed.</param>
        public Crc64(UInt64 polynomial, UInt64 seed)
        {
            table = InitializeTable(polynomial);
            this.seed = seed;
            this.hash = seed;
        }

        /// <summary>
        /// Initializes an implementation of the System.Security.Cryptography.HashAlgorithm class.
        /// </summary>
        public override void Initialize()
        {
            hash = seed;
        }

        /// <summary>
        /// When overridden in a derived class, routes data written to the object into the hash algorithm for computing the hash.
        /// </summary>
        /// <param name="array">The input to compute the hash code for.</param>
        /// <param name="ibStart">The offset into the byte array from which to begin using data.</param>
        /// <param name="cbSize">The number of bytes in the byte array to use as data.</param>
        protected override void HashCore(byte[] array, int ibStart, int cbSize)
        {
            hash = CalculateHash(hash, table, array, ibStart, cbSize);
        }

        /// <summary>
        /// When overridden in a derived class, finalizes the hash computation after the last data is processed by the cryptographic stream object.
        /// </summary>
        /// <returns>The computed hash code.</returns>
        protected override byte[] HashFinal()
        {
            var hashBuffer = UInt64ToBigEndianBytes(hash);
            HashValue = hashBuffer;
            return hashBuffer;
        }

        /// <summary>
        /// The hash size.
        /// </summary>
        /// <value>64.</value>
        public override int HashSize { get { return 64; } }

        /// <summary>
        /// Computes the hash value for the specified region of the specified byte array.
        /// </summary>
        /// <param name="seed">Unsigned 64bits integer.</param>
        /// <param name="table">The hash table.</param>
        /// <param name="buffer">The input to compute the hash code for.</param>
        /// <param name="start">The offset into the byte array from which to begin using data.</param>
        /// <param name="size">The number of bytes in the byte array to use as data.</param>
        /// <returns>Unsigned 64bits integer.</returns>
        protected static UInt64 CalculateHash(UInt64 seed, UInt64[] table, IList<byte> buffer, int start, int size)
        {
            var tmpHash = seed;
            for (var i = start; i < start + size; i++)
                unchecked
                {
                    tmpHash = (tmpHash >> 8) ^ table[(buffer[i] ^ tmpHash) & 0xff];
                }
            return tmpHash;
        }

        /// <summary>
        /// Converts an unsigned 64bits integer number into a BigEndian byte array.
        /// </summary>
        /// <param name="uint32">Unsigned 64bits integer.</param>
        /// <returns>BigEndian byte array.</returns>
        static byte[] UInt64ToBigEndianBytes(UInt64 value)
        {
            var result = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(result);
            }
            return result;
        }

        /// <summary>
        /// Initializes the hash table.
        /// </summary>
        /// <param name="polynomial">Polynomial.</param>
        /// <returns>Unsigned 64bits integer array.</returns>
        static UInt64[] InitializeTable(UInt64 polynomial)
        {
            if (polynomial == Crc64Iso.Iso3309Polynomial && Crc64Iso.Table != null)
            {
                return Crc64Iso.Table;
            }
            var createTable = CreateTable(polynomial);
            if (polynomial == Crc64Iso.Iso3309Polynomial)
            {
                Crc64Iso.Table = createTable;
            }
            return createTable;
        }

        /// <summary>
        /// Creates the table hash.
        /// </summary>
        /// <param name="polynomial">Polynomial.</param>
        /// <returns>Unsigned long array.</returns>
        protected static ulong[] CreateTable(ulong polynomial)
        {
            var createTable = new UInt64[256];
            for (var i = 0; i < 256; ++i)
            {
                var entry = (UInt64)i;
                for (var j = 0; j < 8; ++j)
                {
                    if ((entry & 1) == 1)
                    {
                        entry = (entry >> 1) ^ polynomial;
                    }
                    else
                    {
                        entry = entry >> 1;
                    }
                }
                createTable[i] = entry;
            }
            return createTable;
        }
    }

    /// <summary>
    /// Implements a 64-bit ISO 3309 compliant CRC hash algorithm for a given polynomial.
    /// </summary>
    /// <remarks>
    /// For ISO 3309 compliant 64-bit CRC's use this class.
    /// </remarks>
    public class Crc64Iso : Crc64
    {
        internal static UInt64[] Table;

        /// <summary>
        /// The default polynomial.
        /// </summary>
        public const UInt64 Iso3309Polynomial = 0xD800000000000000;

        /// <summary>
        /// Creates a new instance of the <see cref="Crc64Iso"/> class.
        /// </summary>
        public Crc64Iso()
            : base(Iso3309Polynomial)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Crc64Iso"/> class.
        /// </summary>
        /// <param name="seed">Custom seed.</param>
        public Crc64Iso(UInt64 seed)
            : base(Iso3309Polynomial, seed)
        {
        }

        /// <summary>
        /// Computes the hash.
        /// </summary>
        /// <param name="buffer">Byte array.</param>
        /// <returns>Unsigned 64bits integer.</returns>
        public static UInt64 Compute(byte[] buffer)
        {
            return Compute(DefaultSeed, buffer);
        }

        /// <summary>
        /// Computes the hash.
        /// </summary>
        /// <param name="seed">Custom seed.</param>
        /// <param name="buffer">Byte array.</param>
        /// <returns>Unsigned 64bits integer.</returns>
        public static UInt64 Compute(UInt64 seed, byte[] buffer)
        {
            if (Table == null)
            {
                Table = CreateTable(Iso3309Polynomial);
            }
            return CalculateHash(seed, Table, buffer, 0, buffer.Length);
        }
    }
}