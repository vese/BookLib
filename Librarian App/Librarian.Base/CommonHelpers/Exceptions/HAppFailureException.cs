using System;

namespace CommonHelpers {

//**************************************************************************************************
public class HAppFailureException: HAppExitException {
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

public HAppFailureException()
{}
public HAppFailureException(string message): base( message )
{}
public HAppFailureException(string message,Exception innerException)
   :base( message, innerException )
{}

public HAppFailureException(int exitCode): base( exitCode )
{}
public HAppFailureException(string message,int exitCode): base( message, exitCode )
{}
public HAppFailureException(string message,Exception innerException,int? exitCode)
   :base( message, innerException, exitCode )
{}

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
} // HAppFailureException
//**************************************************************************************************

} // CommonHelpers
