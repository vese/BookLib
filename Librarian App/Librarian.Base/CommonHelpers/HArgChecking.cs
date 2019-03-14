using System;

namespace CommonHelpers {

//**************************************************************************************************
public static class HArgChecking {
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

public static ArgumentException ArgumentException_IncorrectArguments =>
   new ArgumentException( "Incorrect arguments were passed to function." );

/// <exception cref="ArgumentNullException" />
public static void VerifyNotNull(object value)
{
   if ( value == null )
      throw new ArgumentNullException();
}
/// <exception cref="ArgumentNullException" />
public static void VerifyNotNulls(params object[] values)
{
   VerifyNotNull( values );
   foreach (var value in values)
      if ( value == null )
         throw new ArgumentNullException();
}

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
} // HArgChecking
//**************************************************************************************************

} // CommonHelpers
