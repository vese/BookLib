using System;
using System.Globalization;

namespace CommonHelpers {

//**************************************************************************************************
/// <summary>
/// Инвариантные хелперы для 32-битного целого числа
/// </summary>
public static class HInt32 {
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

public static string ToNormalString(Int32 value)
{
   return value.ToString( "G", NumberFormatInfo.InvariantInfo );
}

/// <exception cref="ArgumentNullException" />
/// <exception cref="FormatException" />
/// <exception cref="OverflowException" />
public static Int32 FromString(string str)
{
   return Int32.Parse( str, NumberFormatInfo.InvariantInfo );
}
/// <exception cref="ArgumentNullException" />
public static bool TryFromString(string str,out Int32 value)
{
   Exception err; return TryFromString( str, out value, out err );
}
/// <exception cref="ArgumentNullException" />
public static bool TryFromString(string str,out Int32 value,out Exception err)
{
   try {
      value = FromString( str ); }
   catch (Exception e) {
      if ( e is ArgumentException ) throw;
      value = 0; err = e; return false; }
   err = null; return true;
}

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
} // HInt32
//**************************************************************************************************

} // CommonHelpers
