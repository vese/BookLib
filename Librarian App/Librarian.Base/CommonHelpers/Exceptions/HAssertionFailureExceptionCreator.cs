using System;

namespace CommonHelpers {

//**************************************************************************************************
public static class HAssertionFailureExceptionCreator {
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

public static HAssertionFailureException Create(string messageFmt,params object[] parameters)
{
   return new HAssertionFailureException( string.Format( messageFmt, parameters ) );
}
public static HAssertionFailureException Create(Exception e)
{
   return Create( e.Message );
}

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
} // HAssertionFailureExceptionCreator
//**************************************************************************************************

} // CommonHelpers
