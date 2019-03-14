using System;

using Librarian;
using Librarian.DB;

using CommonHelpers;

namespace DBCorrector
{
    partial class Program
    {
        /// <summary>
        /// Основной блок программы (вызывается из "Program.Main")
        /// </summary>
        /// <exception cref="HAppFailureException" />
        static void Run()
        {
            DBC.ConnectionStringAccessor = () =>
               $"Server={ServerName};Database={DatabaseName};Trusted_Connection=True;"+
               $"Application Name={AssemblyName} (Console App);;Pooling=False";
            PrintCommand();
            try
            {
                switch ( Command )
                {
                case UtilityCommand.Make:
                    Make_Command();
                    break;
                default:
                    throw new HDeadCodeBranchException();
                }
            }
            catch (Exception e)
            {
                if (e is HDeadCodeBranchException) throw;
                throw new HAppFailureException( e.Message );
            }
            PrintDone();
        }

        /// <exception cref="HAppFailureException" />
        static void Make_Command()
        {
            PrintOperation( "Создание базы данных (\"Database.EnsureCreated\")" );
            using (var db = new DBC())
            {}
        }

        static void PrintCommand()
        {
            HConsole.PrintExpressive( $"=== Команда \"{Command}\": ===" );
            HConsole.Print( $"База данных: [{ServerName}].[{DatabaseName}]" );
        }
        static void PrintOperation([HOptional] string caption)
        {
            HConsole.PrintOperation( caption ?? "Выполнение команды \"{Command}\"" );
        }
        static void PrintDone()
        {
            HConsole.PrintExpressive( "Выполнено." );
        }
    }
}
