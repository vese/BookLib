@echo off
setlocal

if "%~0"=="REBUILD_N_START_R.cmd" goto BEGIN
cls
echo REBUILD-N-START (RELEASE):
echo ßßßßßßßßßßßßßßßßßßßßßßßßßß

:BEGIN
call :RESET_ERROR

call REBUILD_R.cmd
if %errorlevel% neq 0 goto FAILURE

echo.
echo ÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ

call START_R.cmd %*
if %errorlevel% neq 0 goto FAILURE

::==================================================================================

:SUCCESS
if "%~0"=="REBUILD_N_START_R.cmd" goto NORMAL_EXIT
echo ÜÜÜ REBUILD-N-START (SUCCESS) ÄÄ OK ÜÜÜ
goto NORMAL_EXIT

:FAILURE
if "%~0"=="REBUILD_N_START_R.cmd" goto ERROR
echo.
echo ÜÜÜ ## REBUILD-N-START (RELEASE) ÄÄ FAILURE ÜÜÜ
goto ERROR

::==================================================================================

:RESET_ERROR
set errorlevel=0
set errorlevel=
goto :EOF

::==================================================================================

:ERROR
if "%~0"=="REBUILD_N_START_R.cmd" goto ERROR_EXIT
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
