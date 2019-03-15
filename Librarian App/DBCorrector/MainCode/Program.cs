using System;
using System.Text;
using System.Reflection;

using CommonHelpers;

namespace DBCorrector
{
    partial class Program
    {
        static readonly string
            AssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
        static string
            ServerName, DatabaseName, CommandName, CommandParameter;
        static UtilityCommand
            Command;
        static readonly StringBuilder
            QueryTextBuilder = new StringBuilder();

        static void PrintSyntax()
        {
            HConsole.PrintIntensive(
            "=== Синтаксис: ===" );
            HConsole.PrintExpressive(
               "{0} \"<сервер>\" \"<БД>\" \"<команда>\" \"[параметр команды]\"",
               AssemblyName );

            HConsole.PrintIntensive(
            "=== Список команд (не чувствительно к регистру): ===" );
            HConsole.PrintExpressive(
               "{0} {1} создание структуры БД (\"Database.EnsureCreated\")",
               UtilityCommand.CreateStructure, HConsole.LongDash );
            HConsole.PrintExpressive(
               "{0} {1} уничтожение структуры БД; параметр: [NotInTransaction]",
               UtilityCommand.DestroyStructure, HConsole.LongDash );
            HConsole.PrintExpressive(
               "{0} {1} уничтожение+создание структуры БД",
               UtilityCommand.RecreateStructure, HConsole.LongDash );
            HConsole.PrintExpressive(
               "{0} {1} зачистка содержимого таблиц; параметр: [NotInTransaction]",
               UtilityCommand.DeleteData, HConsole.LongDash );
            HConsole.PrintExpressive(
               "{0} {1} наполнение тестовыми данными; параметр: [WithCleanup]",
               UtilityCommand.FillTest, HConsole.LongDash );

            string
               sampleSvr = "(localdb)\\MSSQLLocalDB",
               sampleDB = "TEST";

            HConsole.PrintIntensive(
            "=== Примеры команд: ===" );
            HConsole.Print(
               "DBCorrector \"{1}\" \"{2}\" {0}",
               UtilityCommand.CreateStructure, sampleSvr, sampleDB );
            HConsole.Print(
               "DBCorrector \"{1}\" \"{2}\" {0}",
               UtilityCommand.DestroyStructure, sampleSvr, sampleDB);
            HConsole.Print(
               "DBCorrector \"{1}\" \"{2}\" {0}",
               UtilityCommand.RecreateStructure, sampleSvr, sampleDB );
            HConsole.Print(
               "DBCorrector \"{1}\" \"{2}\" {0} NotInTransaction",
               UtilityCommand.DeleteData, sampleSvr, sampleDB );
            HConsole.Print(
               "DBCorrector \"{1}\" \"{2}\" {0} WithCleanup",
               UtilityCommand.FillTest, sampleSvr, sampleDB );
        }

        static int Main(string[] args)
        {
            HProgram.SpecifyMainThread(); // указать главный поток для библиотеки CommonHelpers
            Console.Title = AssemblyName;

            AppDomain.CurrentDomain.UnhandledException += AppDomainUnhandledException_Handler;
            Console.CancelKeyPress += Console_CancelKeyPress;

            HConsole.PrintIntensive( $"*** УТИЛИТА [{AssemblyName.ToUpper()}]: ***" );

            if ( args.Length == 0 )
            {
                PrintSyntax();
                return 0; // нет ошибки
            }
            if ( !HOps.IsOneOf( args.Length, 3, 4 ) )
            {
                HConsole.PrintError(
                    "Ожидалось 3 либо 4 параметра (в аргументах командной строки)." );
                goto INCORRECT_CMD;
            }
            ServerName  = args[ 0 ].Trim();
            DatabaseName= args[ 1 ].Trim();
            CommandName = args[ 2 ].Trim();
            CommandParameter = args.Length>=4 ? args[ 3 ].Trim() : null;
            if ( ServerName=="" || DatabaseName=="" || CommandName=="" || CommandParameter=="" )
            {
                HConsole.PrintError( "Пустые параметры (в аргументах командной строки)." );
                goto INCORRECT_CMD;
            }

            if (!(
                MatchCommad( UtilityCommand.CreateStructure ) ||
                MatchCommad( UtilityCommand.DestroyStructure ) ||
                MatchCommad( UtilityCommand.RecreateStructure ) ||
                MatchCommad( UtilityCommand.DeleteData ) ||
                MatchCommad( UtilityCommand.FillTest )
            ))
            {
                HConsole.PrintError( $"Неопознанная команда: \"{CommandName}\"." );
                goto INCORRECT_CMD;
            }

            int
                status = 0; // нет ошибки
            try
            {
                Run();
            }
            catch (HAppFailureException e)
            {
                if ( e.Message != "" )
                    HConsole.PrintError( e );
                status = 1; // ошибка
            }
            catch (Exception e)
            {
            // Непредвиденная ошибка в главном программном потоке:
                if ( Console.CursorLeft > 0 )
                    Console.WriteLine();
                HConsole.PrintError( e, true, true );
                return -1; // сбой в программе
            }
            return status;

INCORRECT_CMD:
            PrintSyntax();
            return 1; // ошибка
        }

        static bool MatchCommad(UtilityCommand command)
        {
            bool rslt = CommandName.Equals( command.ToString(),
                StringComparison.OrdinalIgnoreCase );
            if ( rslt ) Command = command;
            return rslt;
        }
    }
}
