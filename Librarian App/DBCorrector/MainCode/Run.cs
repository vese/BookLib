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
                case UtilityCommand.CreateStructure:
                    CreateStructure_Command();
                    break;
                default:
                    throw new HDeadCodeBranchException();
                }
            }
            catch (Exception e)
            {
                if (e is HDeadCodeBranchException) throw;
                if (e is OperationFailedException || e is HAppFailureException)
                    throw new HAppFailureException( e.Message );
            }
            PrintDone();
        }

        /// <exception cref="ArgumentNullException" />
        /// <exception cref="HAppFailureException" />
        static void ThrowCommandParametersError(string message)
        {
            HConsole.PrintError( message );
            PrintSyntax();
            throw new HAppFailureException(""); // ошибка уже распечатана
        }
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="HAppFailureException" />
        static void ThrowCommandParametersError(string messageFmt,params object[] parameters)
        {
            ThrowCommandParametersError( HString.FormatAsText( messageFmt, parameters ) );
        }
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="HAppFailureException" />
        static void ThrowCommandParamsNotSupportedError()
        {
            ThrowCommandParametersError(
                $"Команда \"{CommandName}\" не поддерживает параметров." );
        }
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="HAppFailureException" />
        static void ThrowIncorrectCommandParameter(string parameter)
        {
            HArgChecking.VerifyNotNull( parameter );
            ThrowCommandParametersError(
                $"Некорректный параметр для команды \"{CommandName}\": "+
                $"\"{parameter}\" (значение)." );
        }

        static void PrintCommand()
        {
            HConsole.PrintIntensive( $"=== Команда \"{Command}\": ===" );
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
