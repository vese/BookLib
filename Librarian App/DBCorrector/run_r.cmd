@echo off
setlocal

if "%~0"=="RUN_R.cmd" goto BEGIN
cls
echo RUNNING THE APPLICATION (RELEASE):
echo ßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßß

:BEGIN
call :RESET_ERROR

call #SETTINGS.cmd
if %errorlevel% neq 0 goto ERROR

if not exist "%#TargetModulePath_R%" goto TARGET_MODULE_NOT_FOUND

cls
dotnet "%#TargetModulePath_R%" %*
if %errorlevel% neq 0 goto ERROR

goto FINISH

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

::==================================================================================

:ERROR
if "%~0"=="RUN_R.cmd" goto ERROR_EXIT
echo 
pause

:ERROR_EXIT
endlocal
verify # 2>nul
goto :EOF

:FINISH
endlocal
call :RESET_ERROR
if "%~0"=="RUN_R.cmd" goto :EOF
echo.
pause
goto :EOF

::==================================================================================
