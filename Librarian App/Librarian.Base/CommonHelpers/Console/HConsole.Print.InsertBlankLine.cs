using System;

namespace CommonHelpers {

//**************************************************************************************************
partial class HConsole {
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

public static void Print(bool insertBlankLine)
{
   Print( insertBlankLine, (ConsoleColor?)null );
}
public static void Print(bool insertBlankLine,string text)
{
   Print( insertBlankLine, (ConsoleColor?)null, text );
}
public static void Print(bool insertBlankLine,string textFmt,params object[] parameters)
{
   Print( insertBlankLine, (ConsoleColor?)null, textFmt, parameters );
}

public static void Print(bool insertBlankLine,ConsoleColor? foregroundColor)
{
   Print( insertBlankLine, foregroundColor, "" );
}
public static void Print(bool insertBlankLine,ConsoleColor? foregroundColor,string text)
{
   lock ( SyncRoot )
      using ( new HConsoleColorRetention( foregroundColor ) )
      {
         if ( insertBlankLine )
            Console.WriteLine();
         Console.WriteLine( text );
      }
}
public static void Print( bool insertBlankLine, ConsoleColor? foregroundColor,
                          string textFmt, params object[] parameters )
{
   Print( insertBlankLine, foregroundColor, string.Format( textFmt, parameters ) );
}

//--------------------------------------------------------------------------------------------------

public static void PrintNormal(bool insertBlankLine,string text)
{
   Print( insertBlankLine, NormalColor, text );
}
public static void PrintNormal(bool insertBlankLine,string textFmt,params object[] parameters)
{
   Print( insertBlankLine, NormalColor, textFmt, parameters );
}

public static void PrintIntensive(bool insertBlankLine,string text)
{
   Print( insertBlankLine, IntensiveColor, text );
}
public static void PrintIntensive(bool insertBlankLine,string textFmt,params object[] parameters)
{
   Print( insertBlankLine, IntensiveColor, textFmt, parameters );
}

public static void PrintExpressive(bool insertBlankLine,string text)
{
   Print( insertBlankLine, ExpressiveColor, text );
}
public static void PrintExpressive(bool insertBlankLine,string textFmt,params object[] parameters)
{
   Print( insertBlankLine, ExpressiveColor, textFmt, parameters );
}

public static void PrintExclamation(bool insertBlankLine,string text)
{
   Print( insertBlankLine, ExclamationColor, text );
}
public static void PrintExclamation(bool insertBlankLine,string textFmt,params object[] parameters)
{
   Print( insertBlankLine, ExclamationColor, textFmt, parameters );
}

public static void PrintFaint(bool insertBlankLine,string text)
{
   Print( insertBlankLine, FaintColor, text );
}
public static void PrintFaint(bool insertBlankLine,string textFmt,params object[] parameters)
{
   Print( insertBlankLine, FaintColor, textFmt, parameters );
}

//--------------------------------------------------------------------------------------------------

public static void PrintOperation(bool insertBlankLine,string operationText)
{
   Print( insertBlankLine, OperationColor, "* {0} ... ", operationText );
}
public static void PrintOperation( bool insertBlankLine,
                                   string operationTextFmt,
                                   params object[] parameters )
{
   PrintOperation( insertBlankLine, string.Format( operationTextFmt, parameters ) );
}

//--------------------------------------------------------------------------------------------------

public static void PrintError(bool insertBlankLine,string message)
{
   Print( insertBlankLine, ErrorColor, message );
}
public static void PrintError(bool insertBlankLine,string messageFmt,params object[] parameters)
{
   Print( insertBlankLine, ErrorColor, messageFmt, parameters );
}

public static void PrintError(bool insertBlankLine,Exception e)
{
   PrintError( insertBlankLine, e.Message, false );
}
/// <exception cref="ArgumentNullException" />
public static void PrintError(bool insertBlankLine,Exception e,bool printInnerExceptons)
{
   PrintError( insertBlankLine, e.Message, printInnerExceptons, false );
}
/// <exception cref="ArgumentNullException" />
public static void PrintError( bool insertBlankLine, Exception e,
                               bool printInnerExceptons, bool withStackTrace )
{
   if ( e == null )
      throw new ArgumentNullException();
   if ( printInnerExceptons )
      PrintError( printInnerExceptons, HException.ComposeChainedMessage( e, withStackTrace ) );
   else
      PrintError( printInnerExceptons, HException.ComposeMessage( e, withStackTrace ) );
}

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
} // HConsole
//**************************************************************************************************

} // CommonHelpers
