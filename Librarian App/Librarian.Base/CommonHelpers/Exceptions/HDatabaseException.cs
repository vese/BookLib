using System;

namespace CommonHelpers {

//**************************************************************************************************
public class HDatabaseException: HException {
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

public HDatabaseException()
{}
public HDatabaseException(string message)
   :base( message )
{}
public HDatabaseException(string message,Exception innerException)
   :base( message, innerException )
{}

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
} // HDatabaseException
//**************************************************************************************************

} // CommonHelpers
