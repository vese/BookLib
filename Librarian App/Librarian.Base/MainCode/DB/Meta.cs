using System;

namespace Librarian.DB
{
    /// <summary>
    /// Различные константные характеристики БД (размеры, длины, и.т.п.)
    /// </summary>
    public class DBConstants
    {
        public const int NormalName_MaxLength = 50;
        public const string NormalNameType_Expression = "nvarchar(50)";

        public const int LongName_MaxLength = 100;
        public const string LongNameType_Expression = "nvarchar(100)";

        public const int TinyName_MaxLength = 10;
        public const string TinyNameType_Expression = "nvarchar(10)";
   }
   /// <summary>
   /// Имена таблиц в SQL
   /// </summary>
   public class DBTableNames
   {
       public const string Users = "Users";
   }
}
