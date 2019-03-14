using System;

namespace CommonHelpers {

//**************************************************************************************************
partial class HConsole {
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

public static void Print()
{
   Print( false );
}
public static void Print(string text)
{
   Print( false, (ConsoleColor?)null, text );
}
public static void Print(string textFmt,params object[] parameters)
{
   Print( false, (ConsoleColor?)null, textFmt, parameters );
}

public static void Print(ConsoleColor? foregroundColor)
{
   Print( false, foregroundColor, "" );
}
public static void Print(ConsoleColor? foregroundColor,string text)
{
   Print( false, foregroundColor, text );
}
public static void Print(ConsoleColor? foregroundColor,string textFmt,params object[] parameters)
{
   Print( false, foregroundColor, string.Format( textFmt, parameters ) );
}

//--------------------------------------------------------------------------------------------------

public static void PrintNormal(string text)
{
   PrintNormal( false, text );
}
public static void PrintNormal(string textFmt,params object[] parameters)
{
   PrintNormal( false, textFmt, parameters );
}

public static void PrintIntensive(string text)
{
   PrintIntensive( false, text );
}
public static void PrintIntensive(string textFmt,params object[] parameters)
{
   PrintIntensive( false, textFmt, parameters );
}

public static void PrintExpressive(string text)
{
   PrintExpressive( false, text );
}
public static void PrintExpressive(string textFmt,params object[] parameters)
{
   PrintExpressive( false, textFmt, parameters );
}

public static void PrintExclamation(string text)
{
   PrintExclamation( false, text );
}
public static void PrintExclamation(string textFmt,params object[] parameters)
{
   PrintExclamation( false, textFmt, parameters );
}

public static void PrintFaint(string text)
{
   PrintFaint( false, text );
}
public static void PrintFaint(string textFmt,params object[] parameters)
{
   PrintFaint( false, textFmt, parameters );
}

//--------------------------------------------------------------------------------------------------

public static void PrintOperation(string operationText)
{
   PrintOperation( false, operationText );
}
public static void PrintOperation(string operationTextFmt,params object[] parameters)
{
   PrintOperation( false, operationTextFmt, parameters );
}

//--------------------------------------------------------------------------------------------------

public static void PrintError(string message)
{
   PrintError( false, message );
}
public static void PrintError(string messageFmt,params object[] parameters)
{
   PrintError( false, messageFmt, parameters );
}

public static void PrintError(Exception e)
{
   PrintError( false, e.Message, false );
}
/// <exception cref="ArgumentNullException" />
public static void PrintError(Exception e,bool printInnerExceptons)
{
   PrintError( false, e.Message, printInnerExceptons, false );
}
/// <exception cref="ArgumentNullException" />
public static void PrintError(Exception e,bool printInnerExceptons,bool withStackTrace)
{
   PrintError( false, e, printInnerExceptons, withStackTrace );
}

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
} // HConsole
//**************************************************************************************************

} // CommonHelpers
