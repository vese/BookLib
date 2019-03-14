using System;

namespace CommonHelpers {

//**************************************************************************************************
public static class HString {
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

/// <exception cref="ArgumentNullException" />
public static string ConvertAsText(string multilineStr)
{
   HArgChecking.VerifyNotNull( multilineStr );
   return HTextBuilder.ConvertAsText( multilineStr ).ToString();
}
/// <exception cref="ArgumentNullException" />
//[CLSCompliant(false)]
public static string ConvertAsText(ref string multilineStr)
{
   return multilineStr = ConvertAsText( multilineStr );
}

//--------------------------------------------------------------------------------------------------

/// <exception cref="ArgumentNullException" />
public static string FormatAsText(string multilineStrFmt,params object[] parameters)
{
   HArgChecking.VerifyNotNull( multilineStrFmt );
   HArgChecking.VerifyNotNulls( parameters );
   return ConvertAsText( string.Format( multilineStrFmt, parameters ) );
}

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
} // HString
//**************************************************************************************************

} // CommonHelpers
