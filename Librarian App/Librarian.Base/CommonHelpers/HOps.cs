using System;

namespace CommonHelpers {

//**************************************************************************************************
public static class HOps {
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

public static T ReplaceNull<T>(ref T variable,T subst) where T: class
{
   if ( variable==null ) variable = subst; return variable;
}

public static T NullExchange<T>(ref T variable) where T: class
{
   T prev = variable; variable = null; return prev;
}
public static T DefaultExchange<T>(ref T variable)
{
   T prev = variable; variable = default(T); return prev;
}

public static T Exchange<T>(ref T variable,T newValue) where T: class
{
   T prev = variable; variable = newValue; return prev;
}

public static bool IsOneOf([HOptional] object obj,params object[] objects)
{
   HArgChecking.VerifyNotNull( objects );
   foreach (object o in objects)
      if ( object.Equals( o, obj ) )
         return true;
   return false;
}

/// <exception cref="ArgumentNullException" />
public static bool Try(Action action)
{
   Exception err; return Try( action, out err );
}
/// <exception cref="ArgumentNullException" />
public static bool Try(Action action,out Exception err)
{
   HArgChecking.VerifyNotNull( action );
   try { action(); }
   catch (Exception e) {
      err = e; return false; }
   err = null; return true;
}
/// <exception cref="ArgumentNullException" />
public static bool Try(ref Exception err,Action action)
{
   HArgChecking.VerifyNotNull( action );
   try { action(); }
   catch (Exception e) {
      if ( err!=null ) err = e;
      return false; }
   return true;
}

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
} // HOps
//**************************************************************************************************

} // CommonHelpers
