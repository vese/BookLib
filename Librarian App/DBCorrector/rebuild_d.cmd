@echo off
setlocal

if "%~0"=="REBUILD_D.cmd" goto BEGIN
cls
echo RE-BUILDING "DEBUG" CONFIGURATION:
echo ßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßß

:BEGIN
call :RESET_ERROR

call #SETTINGS.cmd
if %errorlevel% neq 0 goto ERROR

call :EXEC_CMD MsBuild /nologo "%#ProjectFileName%" /t:"Rebuild" /p:Configuration="Debug" /restore
if %errorlevel% neq 0 goto FAILURE

::==================================================================================

:SUCCESS
if "%~0"=="REBUILD_D.cmd" goto FINISH
echo.
echo ÜÜÜ REBUILD (DEBUG) ÄÄ SUCCESS ÜÜÜ
goto FINISH

:FAILURE
if "%~0"=="REBUILD_D.cmd" goto ERROR
echo.
echo ÜÜÜ ## REBUILD (DEBUG) ÄÄ FAILURE ÜÜÜ
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
if "%~0"=="REBUILD_D.cmd" goto ERROR_EXIT
echo 
pause

:ERROR_EXIT
endlocal
verify # 2>nul
goto :EOF

:FINISH
endlocal
call :RESET_ERROR
if "%~0"=="REBUILD_D.cmd" goto :EOF
echo.
pause
goto :EOF

::==================================================================================
