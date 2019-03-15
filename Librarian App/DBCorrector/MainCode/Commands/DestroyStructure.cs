﻿using System;

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
        static void DestroyStructure_Command()
        {
            bool inTransaction = true;
            if ( CommandParameter != null )
                if ( HasNotInTransactionParameter() )
                    inTransaction = false;
                else
                    ThrowIncorrectCommandParameter();
            DoDestroyStructure( inTransaction );
        }

        /// <exception cref="OperationFailedException" />
        static void DoDestroyStructure(bool inTransaction)
        {
            HConsole.PrintOperation( "Уничтожение структуры данных" );
            QueryTextBuilder.Clear();
            AppendQueryLine( "BEGIN TRY" );
            ComposeDropTablesScript();
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

        static void ComposeDropTablesScript()
        {
            AppendDropTable( DBTableNames.Users );
        }

        static void AppendQueryLine(string text)
        {
            QueryTextBuilder.AppendLine( text );
        }
        static void AppendDropTable(string tableName)
        {
            QueryTextBuilder.AppendFormat(
                "if OBJECT_ID('{0}','U') is not null drop table [{0}]",
                tableName );
            QueryTextBuilder.AppendLine();
        }
    }
}
