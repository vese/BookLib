using System;
using System.Threading;

namespace CommonHelpers {

//**************************************************************************************************
public static partial class HConsole {
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

public const ConsoleColor
   NormalColor     = ConsoleColor.Gray,
   IntensiveColor  = ConsoleColor.White,
   ExpressiveColor = ConsoleColor.Yellow,
   ExclamationColor= ConsoleColor.Magenta,
   FaintColor      = ConsoleColor.DarkGray,
   ErrorColor      = ConsoleColor.Red,
   OperationColor  = IntensiveColor;

public const char DashChar = '\u2500';
public const string LongDash = "\u2500\u2500";

public static object SyncRoot { get; } = new object();

//==================================================================================================

public static void ResetInput()
{
   while ( Console.KeyAvailable )
      Console.ReadKey( true );
}

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
} // HConsole
//**************************************************************************************************

} // CommonHelpers
