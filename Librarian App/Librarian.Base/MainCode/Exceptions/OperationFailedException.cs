using System;

using CommonHelpers;

namespace Librarian
{
    public class OperationFailedException: Exception
    {
        public OperationFailedException()
        {}
        public OperationFailedException(string message)
        : base( HString.FormatAsText( message ) )
        {}
        public OperationFailedException(string message,Exception innerException)
        : base( HString.FormatAsText( message ), innerException )
        {}
    }
}
