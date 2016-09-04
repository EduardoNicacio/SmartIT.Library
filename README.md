The SmartIT.Library is a library witten in .Net Framework and C# with the ADO.Net flexibility and power in mind. Feel free to download, modify and expand this framework.

Main features:

#SmartIT.Library

1. String Extensions: provides the inumerous string formatters, specially for the Brazilian standards;
2. CRC16: implements the CRC16 hash generation;
3. CRC32: implements the CRC32 hash generation;
4. CRC64: implements the CRC64 hash generation;
5. AuthenticationHelper: provides methods to authenticate users on Active Directory;
6. EnumToList: converts an Enum class to a list;
7. Event viewer: provides methods to white log messages into Windows Event Log System;
8. ExportToExcel: provides funcionality to easily export a data source to an Excel file;
9. Hash: provides methods to easily generate CRC16, CRC32, CRC64, MD5, SHA1, SHA256, SHA384 and SHA512 hashes for both strings and input streams;
10. Misc: provides miscelaneous methods to limit strings, get Enum descriptions, get Enum stringvalue attribute, remove special chars, format date and time to Unix format, get a class properties through reflection, and a generic comparer class that helps in GridView sorting;
11. StringValueAttribute: adds a new attribute to classes or enum types (StringValue);
12. SystemNetMail: an implementation to easily send mail messages using the System.Net.Mail class;
13. SystemWebMail: an implementation to easily send mail messages using the (deprecated) System.Web.Mail class;
13. Validation: provides methods to many types of validations (IsNumber, IsDate, IsDecimal, IsEmail, etc);
14. MaskTextBox: before the advent of jQuery validation and jQuery mask plugins, this class has provided funcionality to generate html error messages, even in HTML format, in Asp.Net applications.

#SmartIT.Library.Data

1. DAOBase: the base DAO class to manipulate data;
2. ManagerBase: the manager base class to the DAO classes;
3. ActiveConnectionAttribute: class that maps the StringValueAttribute in the DAO classes;
4. BizValidationException: class that extends the Exception class and manipulates the error messages (in plain text or HTML);
5. DataBaseProviderFactory: class responsible to create and destroy database connections. Supports SQL Server, Oracle or OleDB connections;
6. DbHelper: provides access to te database and contains methods that facilitate the data manipulation;
7. DBNullHelper: class that contains methods to convert values between Microsoft C# and SQL Server/Oracle data types;
8. SafeDataReader: class that implements the IDataReader interface and 'fixes' any null values before they are returned to our business code.

Please, see also the `smartit.webforms.snippet` in the main directory. It may helps you to easily write the DAO and BLL classes.
