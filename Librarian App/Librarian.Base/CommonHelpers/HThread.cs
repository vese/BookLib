using System;
using System.Threading;

namespace CommonHelpers {

//**************************************************************************************************
public static class HThread {
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

/// <exception cref="ArgumentOutOfRangeException" />
public static void Sleep(int millisecondsTimeout)
{
   Thread.Sleep( millisecondsTimeout );
}
/// <exception cref="ArgumentOutOfRangeException" />
public static void Sleep(TimeSpan timeout)
{
   Thread.Sleep( timeout );
}

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
} // HThread
//**************************************************************************************************

} // CommonHelpers
