using System;

namespace CommonHelpers {

//**************************************************************************************************
public class HEscapeException: HException {
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

public HEscapeException()
{}
public HEscapeException(string message): base( message )
{}
public HEscapeException(string message,Exception innerException): base( message, innerException )
{}

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
} // HEscapeException
//**************************************************************************************************

} // CommonHelpers
