using System;
using System.Text;

namespace CommonHelpers {

//**************************************************************************************************
public static class HTextBuilder {
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

/// <exception cref="ArgumentNullException" />
public static StringBuilder ConvertAsText(string multilineStr)
{
   HArgChecking.VerifyNotNull( multilineStr );
   return ConvertAsText( new StringBuilder(multilineStr) );
}
/// <exception cref="ArgumentNullException" />
public static StringBuilder ConvertAsText(StringBuilder multilineBld)
{
   HArgChecking.VerifyNotNull( multilineBld );
   var sb = new StringBuilder();
   for (int i=0; i<multilineBld.Length; i++)
   {
      char ch = multilineBld[ i ];
      if ( ch == '\n' ) // LF
         sb.AppendLine();
      else
         if ( ch != '\r' ) // CR
            sb.Append(ch);
   }
   return sb;
}

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
} // HTextBuilder
//**************************************************************************************************

} // CommonHelpers
