using System;

namespace CommonHelpers {

//**************************************************************************************************
public class HAppExitException: HEscapeException {
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

public int? ExitCode { get; private set; }

public HAppExitException()
{}
public HAppExitException(string message): base( message )
{}
public HAppExitException(string message,Exception innerException)
   :base( message, innerException )
{}

public HAppExitException(int exitCode)
{
   ExitCode = exitCode;
}
public HAppExitException(string message,int exitCode): base( message )
{
   ExitCode = exitCode;
}
public HAppExitException(string message,Exception innerException,int? exitCode)
   :base( message, innerException )
{
   ExitCode = exitCode;
}

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
} // HAppExitException
//**************************************************************************************************

} // CommonHelpers
