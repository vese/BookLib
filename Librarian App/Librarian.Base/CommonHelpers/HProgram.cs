using System;
using System.Threading;

namespace CommonHelpers {

//**************************************************************************************************
public static class HProgram {
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

public static bool IsInMainThread
{
   get { return Thread.CurrentThread == _MainThread_; }
}

/// <exception cref="InvalidOperationException" />
public static Thread MainThread
{
   get
   {
      var rslt = _MainThread_;
      if ( rslt != null ) return rslt;
      throw new InvalidOperationException(
         "Main program thread was not specified." );
   }
}
public static Thread MainThreadSpecification
{
   get { return _MainThread_; }
}
public static bool IsMainThreadSpecified
{
   get { return _MainThread_ != null; }
}

public static void SpecifyMainThread()
{
   SpecifyMainThread( Thread.CurrentThread );
}
public static void SpecifyMainThread(Thread thread)
{
   _MainThread_ = thread;
}

//==================================================================================================

static volatile Thread
   _MainThread_;

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
} // HProgram
//**************************************************************************************************

} // CommonHelpers
