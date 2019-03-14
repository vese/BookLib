using System;

namespace CommonHelpers {

//**************************************************************************************************
public class HAssertionFailureException: HException {
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

public HAssertionFailureException()
   :base( "Source code assertion has failed." )
{}
public HAssertionFailureException(string message)
   :base( message )
{}
public HAssertionFailureException(string message,Exception innerException)
   :base( message, innerException )
{}

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
} // HAssertionFailureException
//**************************************************************************************************

} // CommonHelpers
