using System;
using System.Text;

namespace CommonHelpers {

//**************************************************************************************************
public class HException: Exception {
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

public HException()
{}
public HException(string message)
   :base( HString.ConvertAsText( message ) )
{}
public HException(string message,Exception innerException)
   :base( HString.ConvertAsText( message ), innerException )
{}

//==================================================================================================

/// <exception cref="ArgumentNullException" />
public static string ComposeMessage(Exception e)
{
   return ComposeMessage( e, false );
}
/// <exception cref="ArgumentNullException" />
public static string ComposeMessage(Exception e,bool withStackTrace)
{
   HArgChecking.VerifyNotNull( e );
   string baseMessage = e.Message != "" ?
      string.Format( "{0}: {1}", e.GetType(), e.Message ) :
      e.GetType().ToString();
   if ( !withStackTrace )
      return baseMessage;
   return baseMessage +
      (e.StackTrace!=null ? Environment.NewLine+e.StackTrace : "");
}

//--------------------------------------------------------------------------------------------------

/// <exception cref="ArgumentNullException" />
public static string ComposeChainedMessage(Exception e)
{
   return ComposeChainedMessage( e, false );
}
/// <exception cref="ArgumentNullException" />
public static string ComposeChainedMessage(Exception e,bool withStackTrace)
{
   return ComposeChainedMessage( e, withStackTrace, false );
}
/// <exception cref="ArgumentNullException" />
public static string ComposeChainedMessage(Exception e,bool withStackTrace,bool separateItems)
{
   HArgChecking.VerifyNotNull( e );
   var sb = new StringBuilder();
   for (var x=e; x!=null; x=x.InnerException)
   {
      if ( x != e )
      {
         sb.AppendLine();
         if ( separateItems )
            sb.AppendLine();
      }
      if ( e.InnerException != null )
         sb.Append( "* " );
      sb.Append( ComposeMessage( x, withStackTrace ) );
   }
   return sb.ToString();
}

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
} // HException
//**************************************************************************************************

} // CommonHelpers
