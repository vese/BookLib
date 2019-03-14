@echo off
setlocal

if "%~0"=="REBUILD_N_START_D.cmd" goto BEGIN
cls
echo REBUILD-N-START (DEBUG):
echo ßßßßßßßßßßßßßßßßßßßßßßßß

:BEGIN
call :RESET_ERROR

call REBUILD_D.cmd
if %errorlevel% neq 0 goto FAILURE

echo.
echo ÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ

call START_D.cmd %*
if %errorlevel% neq 0 goto FAILURE

::==================================================================================

:SUCCESS
if "%~0"=="REBUILD_N_START_D.cmd" goto NORMAL_EXIT
echo ÜÜÜ REBUILD-N-START (SUCCESS) ÄÄ OK ÜÜÜ
goto NORMAL_EXIT

:FAILURE
if "%~0"=="REBUILD_N_START_D.cmd" goto ERROR
echo.
echo ÜÜÜ ## REBUILD-N-START (DEBUG) ÄÄ FAILURE ÜÜÜ
goto ERROR

::==================================================================================

:RESET_ERROR
set errorlevel=0
set errorlevel=
goto :EOF

::==================================================================================

:ERROR
if "%~0"=="REBUILD_N_START_D.cmd" goto ERROR_EXIT
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
