using System;

namespace CommonHelpers {

//**************************************************************************************************
public static class HOpFailedExceptionCreator {
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

public static HOperationFailedException Create(string messageFmt,params object[] parameters)
{
   return new HOperationFailedException( string.Format( messageFmt, parameters ) );
}
public static HOperationFailedException Create(Exception e)
{
   return Create( e.Message );
}

//==================================================================================================

/// <exception cref="ArgumentNullException" />
/// <exception cref="HOperationFailedException" />
public static void Do(Action action)
{
   HArgChecking.VerifyNotNull( action );
   try { action(); }
   catch (Exception e)
      { throw HOpFailedExceptionCreator.Create( e ); }
}

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
} // HOpFailedExceptionCreator
//**************************************************************************************************

} // CommonHelpers
