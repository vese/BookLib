@echo off
setlocal

if "%~0"=="CLEAN_REBUILD_R.cmd" goto BEGIN
cls
echo RE-BUILDING PROJECTS WITH TOTAL CLEANUP ÄÄ "RELEASE":
echo ßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßß

:BEGIN
call CLEAN_ALL.cmd
if %errorlevel% neq 0 goto FAILURE

echo.
echo ÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍ

call REBUILD_R.cmd
if %errorlevel% neq 0 goto FAILURE

::==================================================================================

:SUCCESS
if "%~0"=="CLEAN_REBUILD_R.cmd" goto FINISH
echo.
echo ÜÜÜ REBUILD PROJECTS WITH CLEANUP (RELEASE) ÄÄ SUCCESS ÜÜÜ
goto FINISH

:FAILURE
if "%~0"=="CLEAN_REBUILD_R.cmd" goto ERROR
echo.
echo ÜÜÜ ## REBUILD PROJECTS WITH CLEANUP (RELEASE) ÄÄ FAILURE ÜÜÜ
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
if "%~0"=="REBUILD_R.cmd" goto ERROR_EXIT
echo 
pause

:ERROR_EXIT
endlocal
verify # 2>nul
goto :EOF

:FINISH
endlocal
call :RESET_ERROR
if "%~0"=="REBUILD_R.cmd" goto :EOF
echo.
pause
goto :EOF

::==================================================================================
