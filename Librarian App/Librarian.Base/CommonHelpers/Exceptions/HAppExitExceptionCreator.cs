using System;

namespace CommonHelpers {

//**************************************************************************************************
public static class HAppExitExceptionCreator {
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

public static HAppExitException Create(string messageFmt,params object[] parameters)
{
   return new HAppExitException( string.Format( messageFmt, parameters ) );
}
public static HAppExitException Create(Exception e)
{
   return Create( e.Message );
}

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
} // HAppExitExceptionCreator
//**************************************************************************************************

} // CommonHelpers
