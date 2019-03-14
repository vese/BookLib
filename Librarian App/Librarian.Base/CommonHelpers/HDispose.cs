using System;

namespace CommonHelpers {

//**************************************************************************************************
public static class HDispose {
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

/// <exception cref="Exception" />
public static bool Free(IDisposable disposable)
{
   if ( disposable != null )
   {
      disposable.Dispose();
      return true;
   }
   return false;
}

public static bool TryFree(IDisposable disposable)
{
   Exception err; return TryFree( disposable, out err );
}
public static bool TryFree(IDisposable disposable,out Exception err)
{
   if ( disposable != null )
      try { disposable.Dispose(); }
      catch (Exception e) {
         err = e; return false; }
   err = null; return true;
}

//==================================================================================================

/// <exception cref="Exception" />
public static bool Free<T>(ref T disposableVar) where T: class, IDisposable
{
   if ( disposableVar != null )
   {
      HOps.NullExchange( ref disposableVar ).Dispose();
      return true;
   }
   return false;
}

public static bool TryFree<T>(ref T disposableVar) where T: class, IDisposable
{
   Exception err; return TryFree<T>( ref disposableVar, out err );
}
public static bool TryFree<T>(ref T disposableVar,out Exception err) where T: class, IDisposable
{
   if ( disposableVar != null )
      try {
         HOps.NullExchange( ref disposableVar ).Dispose(); }
      catch (Exception e) {
         err = e; return false; }
   err = null; return true;
}

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
} // HDispose
//**************************************************************************************************

} // CommonHelpers
