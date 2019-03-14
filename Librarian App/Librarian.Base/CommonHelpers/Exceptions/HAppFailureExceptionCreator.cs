using System;

namespace CommonHelpers {

//**************************************************************************************************
public static class HAppFailureExceptionCreator {
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

public static HAppFailureException Create(string messageFmt,params object[] parameters)
{
   return new HAppFailureException( string.Format( messageFmt, parameters ) );
}
public static HAppFailureException Create(Exception e)
{
   return Create( e.Message );
}

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
} // HAppFailureExceptionCreator
//**************************************************************************************************

} // CommonHelpers
