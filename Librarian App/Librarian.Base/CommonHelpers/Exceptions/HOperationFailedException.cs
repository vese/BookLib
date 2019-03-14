using System;

namespace CommonHelpers {

//**************************************************************************************************
public class HOperationFailedException: HException {
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

public HOperationFailedException()
{}
public HOperationFailedException(string message)
   :base( message )
{}
public HOperationFailedException(string message,Exception innerException)
   :base( message, innerException )
{}

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
} // HOperationFailedException
//**************************************************************************************************

} // CommonHelpers
