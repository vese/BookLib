using System;
using System.Text;

using Microsoft.EntityFrameworkCore;

using Librarian;
using Librarian.DB;

using CommonHelpers;

namespace DBCorrector
{
    partial class Program
    {
        static readonly StringBuilder _QueryTextBuilder_ = new StringBuilder();

        /// <exception cref="HAppFailureException" />
        /// <exception cref="OperationFailedException" />
        static void DestroyStructure_Command()
        {
            bool inTransaction = true;
            if ( CommandParameter != null )
                if ( HasNotInTransactionParameter() )
                    inTransaction = false;
                else
                    ThrowIncorrectCommandParameter();
            PrintOperation( "Уничтожение структуры данных" );
            _QueryTextBuilder_.Clear();
            AppendQueryLine( "begin try" );
            AppendDropTable( DBTableNames.Users );
            AppendQueryLine( "end try" );
            AppendQueryLine( "begin catch" );
            AppendQueryLine( "throw" );
            AppendQueryLine( "end catch" );
            try
            {
                using (var db = new DBC(false))
                {
                    if ( inTransaction )
                        db.Database.BeginTransaction();
                    db.Database.ExecuteSqlCommand(_QueryTextBuilder_.ToString());
                    if ( inTransaction )
                        db.Database.CommitTransaction();
                }
            }
            catch (Exception e)
            {
                throw new OperationFailedException( e.Message );
            }
        }

        static void AppendQueryLine(string text)
        {
            _QueryTextBuilder_.AppendLine( text );
        }
        static void AppendDropTable(string tableName)
        {
            _QueryTextBuilder_.AppendFormat(
                "if OBJECT_ID('{0}','U') is not null drop table [{0}]",
                tableName );
            _QueryTextBuilder_.AppendLine();
        }
    }
}
