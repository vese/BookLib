using System;

namespace CommonHelpers {

//**************************************************************************************************
public class HConsoleColorRetention: IDisposable {
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

public HConsoleColorRetention(ConsoleColor? foregroundColor)
   :this( foregroundColor, null )
{}
public HConsoleColorRetention( ConsoleColor? foregroundColor,
                               ConsoleColor? backgroundColor )
{
   if ( foregroundColor.HasValue )
   {
      _ForegroundColor_ = Console.ForegroundColor;
      Console.ForegroundColor = foregroundColor.Value;
   }
   if ( backgroundColor.HasValue )
   {
      _BackgroundColor_ = Console.BackgroundColor;
      Console.BackgroundColor = backgroundColor.Value;
   }
   _IsValidDisposable_ = true;
}

public void Dispose()
{
   _IsValidDisposable_ = false;
   if ( _ForegroundColor_.HasValue ) Console.ForegroundColor = _ForegroundColor_.Value;
   if ( _BackgroundColor_.HasValue ) Console.BackgroundColor = _BackgroundColor_.Value;
}

~HConsoleColorRetention()
{
   if ( _IsValidDisposable_ )
      throw new Exception(
         "Объект [HConsoleColorRetention] должен уничтожаться синхронно." );
}

//==================================================================================================

ConsoleColor?
   _ForegroundColor_,
   _BackgroundColor_;
bool
   _IsValidDisposable_;

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
} // HConsoleColorRetention
//**************************************************************************************************

} // CommonHelpers
