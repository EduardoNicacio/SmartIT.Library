The SmartIT.Library is a library written in .Net Framework and C# with the ADO.Net flexibility and power in mind. Feel free to download, modify and expand this framework.

Main features:

#SmartIT.Library

1. String Extensions: provides the inumerous string formatters, specially for the Brazilian standards;
2. Criptography
  2.a. Base64: Implements the Base64 encoder and decoder;
  2.b. CaesarCipher: Implements the Caesar algorithm;
  2.c. CRC16: implements the CRC16 hash generation;
  2.d. CRC32: implements the CRC32 hash generation;
  2.e. CRC64: implements the CRC64 hash generation;
  2.f. Rot13: implements the ROT13 [Caesar(string, 13)] algorithm;
  2.f. Rot47: implements the ROT47 algorithm;
6. AuthenticationHelper: provides methods to authenticate users on Active Directory;
7. EnumToList: converts an Enum class to a list;
8. EventViewer: provides methods to white log messages into Windows Event Log System;
9. ExportToExcel: provides funcionality to easily export a data source to an Excel file;
10. Hash: provides methods to easily generate CRC16, CRC32, CRC64, MD5, SHA1, SHA256, SHA384 and SHA512 hashes from either strings or byte-streams;
11. Misc: provides miscelaneous methods to limit strings, get Enum descriptions, get Enum stringvalue attribute, remove special chars, format date and time to Unix format, get a class properties through reflection, and a generic comparer class that helps in GridView sorting;
12. StringValueAttribute: adds a new attribute to classes or enum types (StringValue);
13. SystemNetMail: an implementation to easily send mail messages using the System.Net.Mail class;
14. SystemWebMail: an implementation to easily send mail messages using the (deprecated) System.Web.Mail class;
15. Validation: provides methods to many types of validations (IsNumber, IsDate, IsDecimal, IsEmail, etc);
16. MaskTextBox: before the advent of jQuery validation and jQuery mask plugins, this class has provided funcionality to generate html error messages, even in HTML format, in Asp.Net applications.

#SmartIT.Library.Data

1. DAOBase: the base DAO class to manipulate data;
2. ManagerBase: the manager base class to the DAO classes;
3. ActiveConnectionAttribute: class that maps the StringValueAttribute in the DAO classes;
4. BizValidationException: class that extends the Exception class and manipulates the error messages (in plain text or HTML);
5. DataBaseProviderFactory: class responsible to create and destroy database connections. Supports SQL Server, Oracle or OleDB connections;
6. DbHelper: provides access to databases and contains methods that make data manipulation easier;
7. DbNullHelper: class that contains methods to convert values between Microsoft C# and SQL Server/Oracle data types;
8. SafeDataReader: class that implements the IDataReader interface and 'fixes' any null values before they are returned to our business code.

Please, see also the code snippet called `smartit.webforms.snippet` in the main directory. It may helps you to easily write the DAO and BLL classes.
