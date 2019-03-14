@echo off
setlocal

if "%~0"=="START_R.cmd" goto BEGIN
cls
echo STARTING THE APPLICATION (RELEASE):
echo ßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßß

:BEGIN
call :RESET_ERROR

call #SETTINGS.cmd
if %errorlevel% neq 0 goto ERROR

if not exist "%#TargetModulePath_R%" goto TARGET_MODULE_NOT_FOUND

call :EXEC_CMD start "DBCorrector" /D "%CD%" dotnet "%#TargetModulePath_R%" %*
if %errorlevel% neq 0 goto ERROR

::==================================================================================

if "%~0"=="START_R.cmd" goto NORMAL_EXIT
echo ÜÜÜ DONE ÜÜÜ
goto NORMAL_EXIT

::==================================================================================

:TARGET_MODULE_NOT_FOUND
echo.
echo ## Target module not found: "%#TargetModulePath_R%".
goto ERROR

::==================================================================================

:RESET_ERROR
set errorlevel=0
set errorlevel=
goto :EOF

:EXEC_CMD
echo.
echo ^>^> %* ...
echo.
%*
goto :EOF

::==================================================================================

:ERROR
if "%~0"=="START_R.cmd" goto ERROR_EXIT
echo 
pause

:ERROR_EXIT
endlocal
verify # 2>nul
goto :EOF

:NORMAL_EXIT
endlocal
call :RESET_ERROR
goto :EOF

::==================================================================================
