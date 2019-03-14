@echo off
setlocal

if "%~0"=="REBUILD_N_START_R.cmd" goto BEGIN
cls
echo REBUILD-N-START (RELEASE):
echo ��������������������������

:BEGIN
call :RESET_ERROR

call REBUILD_R.cmd
if %errorlevel% neq 0 goto FAILURE

echo.
echo �������������������������������������������������������������������������������

call START_R.cmd %*
if %errorlevel% neq 0 goto FAILURE

::==================================================================================

:SUCCESS
if "%~0"=="REBUILD_N_START_R.cmd" goto NORMAL_EXIT
echo ��� REBUILD-N-START (SUCCESS) �� OK ���
goto NORMAL_EXIT

:FAILURE
if "%~0"=="REBUILD_N_START_R.cmd" goto ERROR
echo.
echo ��� ## REBUILD-N-START (RELEASE) �� FAILURE ���
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
