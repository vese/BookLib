using System;

using Librarian;
using Librarian.DB;

using CommonHelpers;

namespace DBCorrector
{
    partial class Program
    {
        /// <exception cref="HAppFailureException" />
        static void ThrowNotImplementedCommand()
        {
            HConsole.PrintError( $"Команда \"{CommandName}\" не реализована." );
            PrintSyntax();
            throw new HAppFailureException(""); // ошибка уже распечатана
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
        static void ThrowCommandParamsNotSupported()
        {
            ThrowCommandParametersError(
                $"Команда \"{CommandName}\" не поддерживает параметров." );
        }
        /// <exception cref="InvalidOperationException" />
        /// <exception cref="HAppFailureException" />
        static void ThrowIncorrectCommandParameter()
        {
            if ( CommandParameter == null )
                throw new InvalidOperationException();
            ThrowCommandParametersError(
                $"Некорректный параметр для команды \"{CommandName}\": "+
                $"\"{CommandParameter}\" (значение)." );
        }

        /// <exception cref="HAppFailureException" />
        static bool HasNotInTransactionParameter()
        {
            return CommandParameter.Equals( "NotInTransaction",
                                            StringComparison.OrdinalIgnoreCase );
        }
    }
}
