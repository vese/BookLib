@echo off
setlocal

if "%~0"=="REBUILD_N_RUN_R.cmd" goto BEGIN
cls
echo REBUILD-N-RUN (RELEASE):
echo ßßßßßßßßßßßßßßßßßßßßßßß

:BEGIN
call REBUILD_R.cmd
if %errorlevel% neq 0 goto FAILURE

cls
call RUN_R.cmd %*
if %errorlevel% neq 0 goto ERROR

goto FINISH

::==================================================================================

:FAILURE
if "%~0"=="REBUILD_N_RUN_R.cmd" goto ERROR
echo.
echo ÜÜÜ ## REBUILD-N-RUN (RELEASE) ÄÄ FAILURE ÜÜÜ
goto ERROR

::==================================================================================

:RESET_ERROR
set errorlevel=0
set errorlevel=
goto :EOF

::==================================================================================

:ERROR
if "%~0"=="REBUILD_N_RUN_R.cmd" goto ERROR_EXIT
echo 
pause

:ERROR_EXIT
endlocal
verify # 2>nul
goto :EOF

:FINISH
endlocal
call :RESET_ERROR
if "%~0"=="REBUILD_N_RUN_R.cmd" goto :EOF
echo.
pause
goto :EOF

::==================================================================================
