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
        static void RecreateStructure_Command()
        {
            if ( CommandParameter != null )
                ThrowCommandParamsNotSupported();
            DoDestroyStructure( true );
            DoCreateStructure();
        }
    }
}
