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
            HConsole.PrintIntensive( $"=== Команда \"{Command}\": ===" );
            HConsole.Print( $"База данных: [{ServerName}].[{DatabaseName}]" );
            try
            {
                switch ( Command )
                {
                case UtilityCommand.CreateStructure:
                    CreateStructure_Command();
                    break;
                case UtilityCommand.DestroyStructure:
                    DestroyStructure_Command();
                    break;
                case UtilityCommand.RecreateStructure:
                    RecreateStructure_Command();
                    break;
                case UtilityCommand.DeleteData:
                    DeleteData_Command();
                    break;
                case UtilityCommand.FillTest:
                    FillTest_Command();
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
            HConsole.PrintExpressive( "Выполнено." );
        }
    }
}
