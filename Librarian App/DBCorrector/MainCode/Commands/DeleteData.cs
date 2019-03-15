using System;

using Microsoft.EntityFrameworkCore;

using Librarian;
using Librarian.DB;

using CommonHelpers;

namespace DBCorrector
{
    partial class Program
    {
        /// <exception cref="HAppFailureException" />
        /// <exception cref="OperationFailedException" />
        static void DeleteData_Command()
        {
            bool inTransaction = true;
            if ( CommandParameter != null )
                if ( HasNotInTransactionParameter() )
                    inTransaction = false;
                else
                    ThrowIncorrectCommandParameter();
            DoDeleteData( inTransaction );
        }

        /// <exception cref="OperationFailedException" />
        static void DoDeleteData(bool inTransaction)
        {
            HConsole.PrintOperation( "Удаление данных (очистка таблиц)" );
            QueryTextBuilder.Clear();
            AppendQueryLine( "BEGIN TRY" );
            ComposeDeleteTablesScript();
            AppendQueryLine( "END TRY" );
            AppendQueryLine( "BEGIN CATCH" );
            AppendQueryLine( "THROW" );
            AppendQueryLine( "END CATCH" );
            try
            {
                using (var db = new DBC(false))
                {
                    if ( inTransaction )
                        db.Database.BeginTransaction();
                    db.Database.ExecuteSqlCommand( QueryTextBuilder.ToString() );
                    if ( inTransaction )
                        db.Database.CommitTransaction();
                }
            }
            catch (Exception e)
            {
                throw new OperationFailedException( e.Message );
            }
        }

        static void ComposeDeleteTablesScript()
        {
            AppendDeleteTable( DBTableNames.Users );
        }
        static void AppendDeleteTable(string tableName)
        {
            QueryTextBuilder.AppendFormat( "delete [{0}]", tableName );
            QueryTextBuilder.AppendLine();
        }
    }
}
