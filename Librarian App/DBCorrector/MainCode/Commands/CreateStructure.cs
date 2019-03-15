﻿using System;

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
                ThrowCommandParamsNotSupported();
            DoCreateStructure();
        }

        /// <exception cref="OperationFailedException" />
        static void DoCreateStructure()
        {
            HConsole.PrintOperation( "Создание структуры данных (\"Database.EnsureCreated\")" );
            try
            {
                using (var db = new DBC(true))
                {}
            }
            catch (Exception e)
            {
                throw new OperationFailedException( e.Message );
            }
        }
    }
}
