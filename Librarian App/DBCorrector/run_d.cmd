@echo off
setlocal

if "%~0"=="RUN_D.cmd" goto BEGIN
cls
echo RUNNING THE APPLICATION (DEBUG):
echo ßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßß

:BEGIN
call :RESET_ERROR

call #SETTINGS.cmd
if %errorlevel% neq 0 goto ERROR

if not exist "%#TargetModulePath_D%" goto TARGET_MODULE_NOT_FOUND

cls
dotnet "%#TargetModulePath_D%" %*
if %errorlevel% neq 0 goto ERROR

goto FINISH

::==================================================================================

:TARGET_MODULE_NOT_FOUND
echo.
echo ## Target module not found: "%#TargetModulePath_D%".
goto ERROR

::==================================================================================

:RESET_ERROR
set errorlevel=0
set errorlevel=
goto :EOF

::==================================================================================

:ERROR
if "%~0"=="RUN_D.cmd" goto ERROR_EXIT
echo 
pause

:ERROR_EXIT
endlocal
verify # 2>nul
goto :EOF

:FINISH
endlocal
call :RESET_ERROR
if "%~0"=="RUN_D.cmd" goto :EOF
echo.
pause
goto :EOF

::==================================================================================
