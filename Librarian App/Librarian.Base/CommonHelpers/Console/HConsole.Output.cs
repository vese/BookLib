using System;

namespace CommonHelpers {

//**************************************************************************************************
partial class HConsole {
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

public static void Output(string text)
{
   Output( (ConsoleColor?)null, text );
}
public static void Output(string textFmt,params object[] parameters)
{
   Output( (ConsoleColor?)null, textFmt, parameters );
}

public static void Output(ConsoleColor? foregroundColor,string text)
{
   lock ( SyncRoot )
      using ( new HConsoleColorRetention( foregroundColor ) )
         Console.Write( text );
}
public static void Output(ConsoleColor? foregroundColor,string textFmt,params object[] parameters)
{
   Output( foregroundColor, string.Format( textFmt, parameters ) );
}

//--------------------------------------------------------------------------------------------------

public static void OutputNormal(string text)
{
   Output( NormalColor, text );
}
public static void OutputNormal(string textFmt,params object[] parameters)
{
   Output( NormalColor, textFmt, parameters );
}

public static void OutputIntensive(string text)
{
   Output( IntensiveColor, text );
}
public static void OutputIntensive(string textFmt,params object[] parameters)
{
   Output( IntensiveColor, textFmt, parameters );
}

public static void OutputExpressive(string text)
{
   Output( ExpressiveColor, text );
}
public static void OutputExpressive(string textFmt,params object[] parameters)
{
   Output( ExpressiveColor, textFmt, parameters );
}

public static void OutputExclamation(string text)
{
   Output( ExclamationColor, text );
}
public static void OutputExclamation(string textFmt,params object[] parameters)
{
   Output( ExclamationColor, textFmt, parameters );
}

public static void OutputFaint(string text)
{
   Output( FaintColor, text );
}
public static void OutputFaint(string textFmt,params object[] parameters)
{
   Output( FaintColor, textFmt, parameters );
}

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
} // HConsole
//**************************************************************************************************

} // CommonHelpers
