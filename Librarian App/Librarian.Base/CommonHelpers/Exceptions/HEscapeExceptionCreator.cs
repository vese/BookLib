using System;

namespace CommonHelpers {

//**************************************************************************************************
public static class HEscapeExceptionCreator {
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

public static HEscapeException Create(string messageFmt,params object[] parameters)
{
   return new HEscapeException( string.Format( messageFmt, parameters ) );
}
public static HEscapeException Create(Exception e)
{
   return Create( e.Message );
}

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
} // HEscapeExceptionCreator
//**************************************************************************************************

} // CommonHelpers
