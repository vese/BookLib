using System;

using Librarian;
using Librarian.DB;

using CommonHelpers;

namespace DBCorrector
{
    partial class Program
    {
        /// <exception cref="HAppFailureException" />
        /// <exception cref="OperationFailedException" />
        static void CreateStructure_Command()
        {
            if ( CommandParameter != null )
               ThrowCommandParamsNotSupportedError();
            PrintOperation( "Создание структуры данных (\"Database.EnsureCreated\")" );
            try
            {
               using (var db = new DBC())
               {}
            }
            catch (Exception e)
            {
               throw new OperationFailedException( e.Message );
            }
        }
    }
}
