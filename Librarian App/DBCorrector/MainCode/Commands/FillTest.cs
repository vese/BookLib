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
        static void FillTest_Command()
        {
            bool withCleanup = false;
            if ( CommandParameter != null )
                if ( CommandParameter.Equals( "WithCleanup",
                                              StringComparison.OrdinalIgnoreCase ) )
                    withCleanup = true;
                else
                    ThrowIncorrectCommandParameter();
            DoFillTest( withCleanup );
        }

        /// <exception cref="OperationFailedException" />
        static void DoFillTest(bool withCleanup)
        {
            try
            {
                using (var db = new DBC(false))
                {
                    db.Database.BeginTransaction();
                    if ( withCleanup )
                    {
                        HConsole.PrintOperation( "Предварительная очистка всех таблиц" );
                        QueryTextBuilder.Clear();
                        AppendQueryLine( "BEGIN TRY" );
                        ComposeDeleteTablesScript();
                        AppendQueryLine( "END TRY" );
                        AppendQueryLine( "BEGIN CATCH" );
                        AppendQueryLine( "THROW" );
                        AppendQueryLine( "END CATCH" );
                        db.Database.ExecuteSqlCommand( QueryTextBuilder.ToString() );
                    }
                    HConsole.PrintOperation( "Добавление тестовых данных (наполнение таблиц)" );
                    FillTablesWithTest( db );
                    db.SaveChanges();
                    db.Database.CommitTransaction();
                }
            }
            catch (Exception e)
            {
                throw new OperationFailedException( e.Message );
            }
        }

        static void FillTablesWithTest(DBC db)
        {
            db.Users.Add(new User_Item(){
               ID = new Guid( "46FD37B3-9DC5-4B2F-A203-479C54949E93" ),
               Initials = "Пушкин А.С.",
               LastName = "Пушкин",
               FirstName = "Александр",
               SecondName = "Сергеевич",
               IsAdministrator = true,
               IsActiveUser = true
            });
            db.Users.Add(new User_Item()
            {
               ID = new Guid( "3210BC98-43CC-4206-99B2-1185EA0D172A" ),
               Initials = "Лермонтов М.Ю.",
               LastName = "Лермонтов",
               FirstName = "Михаил",
               SecondName = "Юрьевич",
               IsAdministrator = true,
               IsActiveUser = true
            });
        }
    }
}
