@echo off
if "%~0"=="#SETTINGS.cmd" goto SET_ENVIRONMENT
echo.
echo ## Incorrect call of "%~nx0" (auxiliary batch).
echo 
pause
EXIT 255
::==================================================================================
:SET_ENVIRONMENT

set #ProjectName=Librarian.Base

set #ProjectFileName=%#ProjectName%.csproj
set #TargetModuleName=%#ProjectName%.dll

::==================================================================================
goto :EOF
::==================================================================================
:ERROR
verify # 2>nul
goto :EOF
::==================================================================================
