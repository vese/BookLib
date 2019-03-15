using System;
using System.Threading;

using CommonHelpers;

namespace DBCorrector
{
    partial class Program
    {
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
