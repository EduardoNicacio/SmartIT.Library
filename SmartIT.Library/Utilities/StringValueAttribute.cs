// <copyright file="StringValueAttribute.cs" company="Eduardo Claudio Nicacio">
// Copyright Eduardo Claudio Nicacio. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>03/07/2014</date>
// <summary>Class that adds the StringValue attribute to Enum types.</summary>

namespace SmartIT.Library.Utilities
{
	/// <summary>
	/// Class that adds the StringValue attribute to Enum types.
	/// </summary>
	public class StringValueAttribute : System.Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="StringValueAttribute" /> class.
		/// </summary>
		/// <param name="value"> Value property.</param>
		public StringValueAttribute(string value)
		{
			Value = value;
		}

		/// <summary>
		/// Gets the Value property.
		/// </summary>
		public string Value { get; }
	}
}