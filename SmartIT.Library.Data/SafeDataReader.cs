// <copyright file="SafeDataReader.cs" company="Eduardo Claudio Nicacio">
// Copyright Eduardo Claudio Nicacio. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>03/07/2014</date>
// <summary>This is a DataReader that 'fixes' any null values before they are returned to our business code.</summary>

namespace SmartIT.Library.Data
{
    using System;
    using System.Data;

    /// <summary>
    /// This is a DataReader that 'fixes' any null values before they are returned to our business code.
    /// </summary>
    public class SafeDataReader : IDataReader
    {
        /// <summary>
        /// Interface DataReader.
        /// </summary>
        private readonly IDataReader dataReader;

        /// <summary>
        /// To detect redundant calls.
        /// </summary>
        private bool disposedValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="SafeDataReader" /> class.
        /// </summary>
        /// <param name="dataReader">The source DataReader object containing the data.</param>
        public SafeDataReader(IDataReader dataReader)
        {
            this.dataReader = dataReader;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="SafeDataReader" /> class.
        /// </summary>
        ~SafeDataReader()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets the RecordsAffected property value from the underlying datareader.
        /// </summary>
        /// <returns>The number of affected records.</returns>
        /// <value>Obrigatorio pelo SONAR.</value>
        public int RecordsAffected
        {
            get
            {
                return dataReader.RecordsAffected;
            }
        }

        /// <summary>
        /// Gets the depth property value from the datareader.
        /// </summary>
        /// <returns>The level of nesting.</returns>
        /// <value> Obrigatorio pelo SONAR.</value>
        public int Depth
        {
            get
            {
                return dataReader.Depth;
            }
        }

        /// <summary>
        /// Gets the FieldCount property from the datareader.
        /// </summary>
        /// <returns>When not positioned in a valid recordset, 0; otherwise, the number of columns in the current record. The default is -1.</returns>
        /// <value> Obrigatorio pelo SONAR.</value>
        public int FieldCount
        {
            get
            {
                return dataReader.FieldCount;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the IsClosed property value from the datareader.
        /// </summary>
        /// <returns>True, if the DataReader is closed; otherwise, false.</returns>
        /// <value>Obrigatorio pelo SONAR.</value>
        public bool IsClosed
        {
            get
            {
                return dataReader.IsClosed;
            }
        }

        /// <summary>
        /// Gets a reference to the underlying data reader object that actually contains the data from the data source.
        /// </summary>
        /// <value>Valor de IDataReader.</value>
        protected IDataReader DataReader
        {
            get { return dataReader; }
        }

        /// <summary>
        /// Gets a value from the datareader.
        /// </summary>
        /// <param name="name">Name of the column containing the value.</param>
        /// <returns> An object representing the value of the column.</returns>
        public object this[string name]
        {
            get
            {
                object val = dataReader[name];

                if (DBNull.Value.Equals(val))
                {
                    return null;
                }
                else
                {
                    return val;
                }
            }
        }

        /// <summary>
        /// Gets a value from the datareader.
        /// </summary>
        /// <param name="value">Ordinal column position of the value.</param>
        /// <returns> An object representing the value of the column.</returns>
        public virtual object this[int value]
        {
            get
            {
                if (dataReader.IsDBNull(value))
                {
                    return null;
                }
                else
                {
                    return dataReader[value];
                }
            }
        }

        /// <summary>
        /// Gets a string value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns empty string for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        /// <returns>String com o valor.</returns>
        public string GetString(string name)
        {
            return GetString(dataReader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets a string value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns empty string for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        /// <returns>String com o valor.</returns>
        public virtual string GetString(int i)
        {
            return dataReader.IsDBNull(i) ? string.Empty : dataReader.GetString(i);
        }

        /// <summary>
        /// Gets a value of type <see cref="System.Object" /> from the datareader.
        /// </summary>
        /// <param name="name">Name of the column containing the value.</param>
        /// <returns>Objeto com o valor.</returns>
        public object GetValue(string name)
        {
            return GetValue(dataReader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets a value of type <see cref="System.Object" /> from the datareader.
        /// </summary>
        /// <param name="i">Ordinal column position of the value.</param>
        /// <returns>Objeto com o valor.</returns>
        public virtual object GetValue(int i)
        {
            return dataReader.IsDBNull(i) ? null : dataReader.GetValue(i);
        }

        /// <summary>
        /// Gets an integer from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        /// <returns>Inteiro com o valor.</returns>
        public int GetInt32(string name)
        {
            return GetInt32(dataReader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets an integer from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        /// <returns>Inteiro com o valor.</returns>
        public virtual int GetInt32(int i)
        {
            return dataReader.IsDBNull(i) ? 0 : Convert.ToInt32(dataReader[i]);
        }

        /// <summary>
        /// Gets a double from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        /// <returns> The double value of the field.</returns>
        public double GetDouble(string name)
        {
            return GetDouble(dataReader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets a double from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        /// <returns> The double value of the field.</returns>
        public virtual double GetDouble(int i)
        {
            return dataReader.IsDBNull(i) ? 0 : dataReader.GetDouble(i);
        }

        /// <summary>
        /// Gets a <see cref="SmartDate" /> from the datareader.
        /// </summary>
        /// <remarks>
        /// A null is converted into min possible date
        /// See Chapter 5 for more details on the SmartDate class.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        /// <returns> The DateTime value of the field if not null; otherwise, null.</returns>
        public DateTime? GetNullableDateTime(string name)
        {
            return GetNullableDateTime(dataReader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets a <see cref="SmartDate" /> from the datareader.
        /// </summary>
        /// <remarks>
        /// A null is converted into the min possible date
        /// See Chapter 5 for more details on the SmartDate class.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        /// <returns> The DateTime value of the field if not null; otherwise, null.</returns>
        public virtual DateTime? GetNullableDateTime(int i)
        {
            if (dataReader.IsDBNull(i))
            {
                return null;
            }
            else
            {
                return dataReader.GetDateTime(i);
            }
        }

        /// <summary>
        /// Gets a Guid value from the datareader.
        /// </summary>
        /// <param name="name">Name of the column containing the value.</param>
        /// <returns> Returns Guid. Empty for null.</returns>
        public System.Guid GetGuid(string name)
        {
            return GetGuid(dataReader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets a Guid value from the datareader.
        /// </summary>
        /// <param name="i">Ordinal column position of the value.</param>
        /// <returns> Returns Guid. Empty for null.</returns>
        public virtual System.Guid GetGuid(int i)
        {
            return dataReader.IsDBNull(i) ? Guid.Empty : dataReader.GetGuid(i);
        }

        /// <summary>
        /// Reads the next row of data from the datareader.
        /// </summary>
        /// <returns>True or false.</returns>
        public bool Read()
        {
            return dataReader.Read();
        }

        /// <summary>
        /// Moves to the next result set in the datareader.
        /// </summary>
        /// <returns>True or false.</returns>
        public bool NextResult()
        {
            return dataReader.NextResult();
        }

        /// <summary>
        /// Closes the datareader.
        /// </summary>
        public void Close()
        {
            dataReader.Close();
        }

        /// <summary>
        /// Gets a boolean value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns <see langword="false" /> for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        /// <returns>True or false.</returns>
        public bool GetBoolean(string name)
        {
            return GetBoolean(dataReader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets a boolean value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns <see langword="false" /> for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        /// <returns>True or false.</returns>
        public virtual bool GetBoolean(int i)
        {
            bool result;

            if (dataReader.IsDBNull(i))
            {
                result = false;
            }
            else
            {
                result = dataReader.GetBoolean(i);
            }

            return result;
        }

        /// <summary>
        /// Gets a byte value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        /// <returns>Byte value of the column.</returns>
        public byte GetByte(string name)
        {
            return GetByte(dataReader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets a byte value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        /// <returns>Byte value of the column.</returns>
        public virtual byte GetByte(int i)
        {
            return dataReader.IsDBNull(i) ? (byte)0 : dataReader.GetByte(i);
        }

        /// <summary>
        /// Invokes the GetBytes method of the underlying datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        /// <param name="fieldOffset">Offset position within the field.</param>
        /// <param name="buffer">Array containing the data.</param>
        /// <param name="bufferOffset">Offset position within the buffer.</param>
        /// <param name="length">Length of data to read.</param>
        /// <returns>The Long value of the column.</returns>
        public long GetBytes(string name, long fieldOffset, byte[] buffer, int bufferOffset, int length)
        {
            return GetBytes(dataReader.GetOrdinal(name), fieldOffset, buffer, bufferOffset, length);
        }

        /// <summary>
        /// Invokes the GetBytes method of the underlying datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="value">Ordinal column position of the value.</param>
        /// <param name="fieldOffset">Offset position within the field.</param>
        /// <param name="buffer">Array containing the data.</param>
        /// <param name="bufferOffset">Offset position within the buffer.</param>
        /// <param name="length">Length of data to read.</param>
        /// <returns>The Long value of the column.</returns>
        public virtual long GetBytes(int value, long fieldOffset, byte[] buffer, int bufferOffset, int length)
        {
            return dataReader.IsDBNull(value) ? 0L : dataReader.GetBytes(value, fieldOffset, buffer, bufferOffset, length);
        }

        /// <summary>
        /// Gets a char value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns Char.MinValue for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        /// <returns>The char value of the param.</returns>
        public char GetChar(string name)
        {
            return GetChar(dataReader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets a char value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns Char.MinValue for null.
        /// </remarks>
        /// <param name="value">Ordinal column position of the value.</param>
        /// <returns>The char value of the param.</returns>
        public virtual char GetChar(int value)
        {
            if (dataReader.IsDBNull(value))
            {
                return char.MinValue;
            }
            else
            {
                char[] myChar = new char[1];
                dataReader.GetChars(value, 0, myChar, 0, 1);
                return myChar[0];
            }
        }

        /// <summary>
        /// Invokes the GetChars method of the underlying datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        /// <param name="fieldOffset">Offset position within the field.</param>
        /// <param name="buffer">Array containing the data.</param>
        /// <param name="bufferOffset">Offset position within the buffer.</param>
        /// <param name="length">Length of data to read.</param>
        /// <returns>The Long value of the column.</returns>
        public long GetChars(string name, long fieldOffset, char[] buffer, int bufferOffset, int length)
        {
            return GetChars(dataReader.GetOrdinal(name), fieldOffset, buffer, bufferOffset, length);
        }

        /// <summary>
        /// Invokes the GetChars method of the underlying datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="value">Ordinal column position of the value.</param>
        /// <param name="fieldOffset">Offset position within the field.</param>
        /// <param name="buffer">Array containing the data.</param>
        /// <param name="bufferOffset">Offset position within the buffer.</param>
        /// <param name="length">Length of data to read.</param>
        /// <returns>The Long value of the column.</returns>
        public virtual long GetChars(int value, long fieldOffset, char[] buffer, int bufferOffset, int length)
        {
            return dataReader.IsDBNull(value) ? 0L : dataReader.GetChars(value, fieldOffset, buffer, bufferOffset, length);
        }

        /// <summary>
        /// Invokes the GetData method of the underlying datareader.
        /// </summary>
        /// <param name="name">Name of the column containing the value.</param>
        /// <returns>An IDataReader object.</returns>
        public IDataReader GetData(string name)
        {
            return GetData(dataReader.GetOrdinal(name));
        }

        /// <summary>
        /// Invokes the GetData method of the underlying datareader.
        /// </summary>
        /// <param name="i">Ordinal column position of the value.</param>
        /// <returns>An IDataReader object.</returns>
        public virtual IDataReader GetData(int i)
        {
            return dataReader.GetData(i);
        }

        /// <summary>
        /// Invokes the GetDataTypeName method of the underlying datareader.
        /// </summary>
        /// <param name="name">Name of the column containing the value.</param>
        /// <returns>The value of the column indicated.</returns>
        public string GetDataTypeName(string name)
        {
            return GetDataTypeName(dataReader.GetOrdinal(name));
        }

        /// <summary>
        /// Invokes the GetDataTypeName method of the underlying datareader.
        /// </summary>
        /// <param name="value">Ordinal column position of the value.</param>
        /// <returns>Returns the Data Type of the column.</returns>
        public virtual string GetDataTypeName(int value)
        {
            return dataReader.GetDataTypeName(value);
        }

        /// <summary>
        /// Gets a date value from the datareader.
        /// </summary>
        /// <param name="name">Name of the column containing the value.</param>
        /// <returns>Returns DateTime. MinValue for null.</returns>
        public virtual DateTime GetDateTime(string name)
        {
            return GetDateTime(dataReader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets a date value from the datareader.
        /// </summary>
        /// <param name="i">Ordinal column position of the value.</param>
        /// <returns>Returns DateTime. MinValue for null.</returns>
        public virtual DateTime GetDateTime(int i)
        {
            return dataReader.IsDBNull(i) ? DateTime.MinValue : dataReader.GetDateTime(i);
        }

        /// <summary>
        /// Gets a decimal value from the datareader.
        /// </summary>
        /// <param name="name">Name of the column containing the value.</param>
        /// <returns>Returns 0 for null.</returns>
        public decimal GetDecimal(string name)
        {
            return GetDecimal(dataReader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets a decimal value from the datareader.
        /// </summary>
        /// <param name="i">Ordinal column position of the value.</param>
        /// <returns>Returns 0 for null.</returns>
        public virtual decimal GetDecimal(int i)
        {
            return dataReader.IsDBNull(i) ? (decimal)0 : dataReader.GetDecimal(i);
        }

        /// <summary>
        /// Invokes the GetFieldType method of the underlying datareader.
        /// </summary>
        /// <param name="name">Name of the column containing the value.</param>
        /// <returns>Return the Type of the column.</returns>
        public Type GetFieldType(string name)
        {
            return GetFieldType(dataReader.GetOrdinal(name));
        }

        /// <summary>
        /// Invokes the GetFieldType method of the underlying datareader.
        /// </summary>
        /// <param name="i">Ordinal column position of the value.</param>
        /// <returns>Return the Type of the column.</returns>
        public virtual Type GetFieldType(int i)
        {
            return dataReader.GetFieldType(i);
        }

        /// <summary>
        /// Gets a Single value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        /// <returns>The Float value of the column, or 0 for null.</returns>
        public float GetFloat(string name)
        {
            return GetFloat(dataReader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets a Single value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        /// <returns>The Float value of the column, or 0 for null.</returns>
        public virtual float GetFloat(int i)
        {
            return dataReader.IsDBNull(i) ? 0F : dataReader.GetFloat(i);
        }

        /// <summary>
        /// Gets a Short value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        /// <returns>The short value of the column, or 0 for null.</returns>
        public short GetInt16(string name)
        {
            return GetInt16(dataReader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets a Short value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        /// <returns>The short value of the column, or 0 for null.</returns>
        public virtual short GetInt16(int i)
        {
            return dataReader.IsDBNull(i) ? (short)0 : dataReader.GetInt16(i);
        }

        /// <summary>
        /// Gets a Long value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        /// <returns>The long value of the column, or 0 for null.</returns>
        public long GetInt64(string name)
        {
            return GetInt64(dataReader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets a Long value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        /// <returns>The long value of the column, or 0 for null.</returns>
        public virtual long GetInt64(int i)
        {
            return dataReader.IsDBNull(i) ? 0L : dataReader.GetInt64(i);
        }

        /// <summary>
        /// Invokes the GetName method of the underlying datareader.
        /// </summary>
        /// <param name="value">Ordinal column position of the value.</param>
        /// <returns>The string value of the column.</returns>
        public virtual string GetName(int value)
        {
            return dataReader.GetName(value);
        }

        /// <summary>
        /// Gets an ordinal value from the datareader.
        /// </summary>
        /// <param name="name">Name of the column containing the value.</param>
        /// <returns>The int value of the column.</returns>
        public int GetOrdinal(string name)
        {
            return dataReader.GetOrdinal(name);
        }

        /// <summary>
        /// Invokes the GetSchemaTable method of the underlying datareader.
        /// </summary>
        /// <returns>The DataTable schema.</returns>
        public DataTable GetSchemaTable()
        {
            return dataReader.GetSchemaTable();
        }

        /// <summary>
        /// Invokes the GetValues method of the underlying datareader.
        /// </summary>
        /// <param name="values">An array of System.Object to copy the values into.</param>
        /// <returns>The number of instances of System.Object in the array.</returns>
        public int GetValues(object[] values)
        {
            return dataReader.GetValues(values);
        }

        /// <summary>
        /// Invokes the IsDBNull method of the underlying datareader.
        /// </summary>
        /// <param name="value">Ordinal column position of the value.</param>
        /// <returns>True if the param is null; otherwise, false.</returns>
        public virtual bool IsDBNull(int value)
        {
            return dataReader.IsDBNull(value);
        }

        /// <summary>
        /// Dispose the object.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) below.
            Dispose(true);
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">True if called by the public Dispose method.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue && disposing)
            {
                // free unmanaged resources when explicitly called
                dataReader.Dispose();
            }

            // free shared unmanaged resources
            disposedValue = true;
        }
    }
}