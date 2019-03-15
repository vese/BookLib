//\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
// This code file is generated automatically (by scripts of Librarian.Base project).
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
using System;
using System.Reflection;

namespace Librarian.Base {

//**************************************************************************************************
/// <summary>
/// Assembly information and related
/// </summary>
public static partial class Librarian_Base_Info {
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

public const string
   AssemblyName         = "Librarian.Base",
   ModuleName           = "Librarian.Base.dll",
   ProductTitle         = "Модуль Librarian.Base",
   VersionString        = "2019.03.15.0",
   TargetFramework      = "netcoreapp2.1",
   CompanyName          = "Релэкс",
   ProductName          = "Librarian.Base DLL",
   ProductDescription   = "Базовый модуль приложения Librarian",
// CopyrightString      = "",
   Authors              = "Веселов, Китаев, Перов, Чиркин";

public const string
#if DEBUG
   ConfigurationName = "Debug";
#else
   ConfigurationName = "Release";
#endif

public static readonly Version
   Version = Assembly.GetExecutingAssembly().GetName().Version;

//==================================================================================================

#if DEBUG
public const bool
   IsDebugConfiguration   = true,
   IsReleaseConfiguration = false;
#endif // DEBUG
#if RELEASE
public const bool
   IsDebugConfiguration   = false,
   IsReleaseConfiguration = true;
#endif // RELEASE

//--------------------------------------------------------------------------------------------------

public static bool GetIsDebugConfiguration()
{
   return IsDebugConfiguration;
}
public static bool GetIsReleaseConfiguration()
{
   return IsReleaseConfiguration;
}

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
} // Librarian_Base_Info
//**************************************************************************************************

} // Librarian.Base
