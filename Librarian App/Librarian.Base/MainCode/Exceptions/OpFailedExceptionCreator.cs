using System;

using CommonHelpers;

namespace Librarian
{
    public static class OpFailedExceptionCreator
    {
        public static OperationFailedException Create(string messageFmt,params object[] parameters)
        {
            return new OperationFailedException( HString.FormatAsText( messageFmt, parameters ) );
        }
        public static OperationFailedException Create(Exception e)
        {
            return Create( e.Message );
        }
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="OperationFailedException" />
        public static void Do(Action action)
        {
            HArgChecking.VerifyNotNull( action );
            try { action(); }
            catch (Exception e)
            { throw Create( e ); }
       }
    }
}
