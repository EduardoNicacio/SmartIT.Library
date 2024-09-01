// <copyright file="Crc32.cs" company="Eduardo Claudio Nicacio">
// Copyright Eduardo Claudio Nicacio. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>26/05/2016</date>
// <summary>CRC32 implementation.</summary>

namespace SmartIT.Library.Utilities.Hashes
{
	using System;
	using System.Collections.Generic;
	using System.Security.Cryptography;

	/// <summary>
	/// Implements a 32-bit CRC hash algorithm compatible with Zip etc.
	/// </summary>
	/// <remarks>
	/// Crc32 should only be used for backward compatibility with older file formats
	/// and algorithms. It is not secure enough for new applications.
	/// If you need to call multiple times for the same data either use the HashAlgorithm
	/// interface or remember that the result of one Compute call needs to be ~ (XOR) before
	/// being passed in as the seed for the next Compute call.
	/// </remarks>
	public sealed class Crc32 : HashAlgorithm
	{
		/// <summary>
		/// The default polynomial.
		/// </summary>
		public const UInt32 DefaultPolynomial = 0xedb88320u;
		/// <summary>
		/// The default seed.
		/// </summary>
		public const UInt32 DefaultSeed = 0xffffffffu;

		static UInt32[] defaultTable;

		readonly UInt32 seed;
		readonly UInt32[] table;
		UInt32 hash;

		/// <summary>
		/// Creates a new instance of the <see cref="Crc32"/> class.
		/// </summary>
		public Crc32()
			: this(DefaultPolynomial, DefaultSeed)
		{
		}

		/// <summary>
		/// Creates a new instance of the <see cref="Crc32"/> class.
		/// </summary>
		/// <param name="polynomial">Custim polynomial.</param>
		/// <param name="seed">Custom seed.</param>
		public Crc32(UInt32 polynomial, UInt32 seed)
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
			hash = CalculateHash(table, hash, array, ibStart, cbSize);
		}

		/// <summary>
		/// When overridden in a derived class, finalizes the hash computation after the last data is processed by the cryptographic stream object.
		/// </summary>
		/// <returns>The computed hash code.</returns>
		protected override byte[] HashFinal()
		{
			var hashBuffer = UInt32ToBigEndianBytes(~hash);
			HashValue = hashBuffer;
			return hashBuffer;
		}

		/// <summary>
		/// Gets the hash size.
		/// </summary>
		/// <value>32.</value>
		public override int HashSize { get { return 32; } }

		/// <summary>
		/// Computes the hash.
		/// </summary>
		/// <param name="buffer">Byte array.</param>
		/// <returns>Unsigned 32bits integer.</returns>
		public static UInt32 Compute(byte[] buffer)
		{
			return Compute(DefaultSeed, buffer);
		}

		/// <summary>
		/// Computes the hash.
		/// </summary>
		/// <param name="seed">Custom seed.</param>
		/// <param name="buffer">Byte array.</param>
		/// <returns>Unsigned 32bits integer.</returns>
		public static UInt32 Compute(UInt32 seed, byte[] buffer)
		{
			return Compute(DefaultPolynomial, seed, buffer);
		}

		/// <summary>
		/// Computes the hash.
		/// </summary>
		/// <param name="polynomial">Custom polynomial.</param>
		/// <param name="seed">Custom seed.</param>
		/// <param name="buffer">Byte array.</param>
		/// <returns>Unsigned 32bits integer.</returns>
		public static UInt32 Compute(UInt32 polynomial, UInt32 seed, byte[] buffer)
		{
			return ~CalculateHash(InitializeTable(polynomial), seed, buffer, 0, buffer.Length);
		}

		/// <summary>
		/// Initializes the hash table.
		/// </summary>
		/// <param name="polynomial">Polynomial.</param>
		/// <returns>Unsigned 32bits integer array.</returns>
		static UInt32[] InitializeTable(UInt32 polynomial)
		{
			if (polynomial == DefaultPolynomial && defaultTable != null)
				return defaultTable;

			var createTable = new UInt32[256];
			for (var i = 0; i < 256; i++)
			{
				var entry = (UInt32)i;
				for (var j = 0; j < 8; j++)
					if ((entry & 1) == 1)
						entry = (entry >> 1) ^ polynomial;
					else
						entry = entry >> 1;
				createTable[i] = entry;
			}

			if (polynomial == DefaultPolynomial)
				defaultTable = createTable;

			return createTable;
		}

		/// <summary>
		/// Computes the hash value for the specified region of the specified byte array.
		/// </summary>
		/// <param name="table">The hash table.</param>
		/// <param name="seed">The seed.</param>
		/// <param name="buffer">The input to compute the hash code for.</param>
		/// <param name="start">The offset into the byte array from which to begin using data.</param>
		/// <param name="size">The number of bytes in the byte array to use as data.</param>
		/// <returns>Unsigned 32bits integer.</returns>
		static UInt32 CalculateHash(UInt32[] table, UInt32 seed, IList<byte> buffer, int start, int size)
		{
			var tmpHash = seed;
			for (var i = start; i < start + size; i++)
			{
				tmpHash = (tmpHash >> 8) ^ table[buffer[i] ^ tmpHash & 0xff];
			}
			return tmpHash;
		}

		/// <summary>
		/// Converts an unsigned 32bits integer number into a BigEndian byte array.
		/// </summary>
		/// <param name="uint32">Unsigned 32bits integer.</param>
		/// <returns>BigEndian byte array.</returns>
		static byte[] UInt32ToBigEndianBytes(UInt32 uint32)
		{
			var result = BitConverter.GetBytes(uint32);
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(result);
			}
			return result;
		}
	}
}