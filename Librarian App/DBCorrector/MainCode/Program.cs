using System;
using System.Threading;
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
            HConsole.PrintIntensive(
               "=== Примеры команд: ===" );
            HConsole.Print(
               "DBCorrector \"(localdb)\\MSSQLLocalDB\" \"Librarian\" \"{0}\"",
               UtilityCommand.CreateStructure );
            HConsole.Print(
               "DBCorrector \"(localdb)\\MSSQLLocalDB\" \"Librarian\" \"{0}\"",
               UtilityCommand.DestroyStructure );
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
                MatchCommad( UtilityCommand.DestroyStructure ) ))
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

        static void AppDomainUnhandledException_Handler( object sender,
                                                         UnhandledExceptionEventArgs e )
        {
            bool lockCapture = Monitor.TryEnter( HConsole.SyncRoot, 500 );
            try
            {
                HConsole.Print();
                HConsole.PrintIntensive( "=== {0}: ===", HProgram.IsInMainThread ?
                    "Необработанное исключение в основном программном потоке" :
                    "Необработанное исключение по второстепенном программном потоке" );
                var exception = e.ExceptionObject as Exception;
                using ( new HConsoleColorRetention( HConsole.ErrorColor ) )
                    if ( exception != null )
                    // Вывод цепи исключений (без стека вызовов):
                        Console.WriteLine( HException.ComposeChainedMessage( exception ) );
                    else
                        Console.WriteLine(
                            "Объект необработанного исключения не является экземпляром "+
                            "\"System.Exception\"." );
            }
            finally
            {
               if ( lockCapture )
                  Monitor.Exit( HConsole.SyncRoot );
            }
        // Стек вызовов будет выведен на консоль автоматически (серым цветом).
        }

        private static void Console_CancelKeyPress(object sender,ConsoleCancelEventArgs e)
        {
            bool lockCapture = Monitor.TryEnter( HConsole.SyncRoot, 500 );
            try
            {
                if ( Console.CursorLeft > 0 )
                    Console.WriteLine();
                using ( new HConsoleColorRetention( HConsole.IntensiveColor ) )
                    Console.WriteLine( "* Прерывание по <Ctrl+C> ... " );
            }
            finally
            {
                if ( lockCapture )
                    Monitor.Exit( HConsole.SyncRoot );
            }
        }
    }
}
