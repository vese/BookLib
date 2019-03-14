@echo off
if "%~0"=="#SETTINGS.cmd" goto SET_ENVIRONMENT
echo.
echo ## Incorrect call of "%~nx0" (auxiliary batch).
echo 
pause
EXIT 255
::==================================================================================
:SET_ENVIRONMENT

set #ProjectName=DBCorrector
set #FrameworkFolderName=netcoreapp2.1

set #ProjectFileName=%#ProjectName%.csproj
set #TargetModuleName=%#ProjectName%.dll

set #OutputDir_D=bin\Debug\%#FrameworkFolderName%\
set #OutputDir_R=bin\Release\%#FrameworkFolderName%\

set #TargetModulePath_D=%#OutputDir_D%%#TargetModuleName%
set #TargetModulePath_R=%#OutputDir_R%%#TargetModuleName%

::==================================================================================
goto :EOF
::==================================================================================
:ERROR
verify # 2>nul
goto :EOF
::==================================================================================
