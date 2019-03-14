@echo off
setlocal

if "%~0"=="BUILD_N_START_D.cmd" goto BEGIN
cls
echo BUILD-N-START (DEBUG):
echo ����������������������

:BEGIN
call :RESET_ERROR

call BUILD_D.cmd
if %errorlevel% neq 0 goto FAILURE

echo.
echo �������������������������������������������������������������������������������

call START_D.cmd %*
if %errorlevel% neq 0 goto FAILURE

::==================================================================================

:SUCCESS
if "%~0"=="BUILD_N_START_D.cmd" goto NORMAL_EXIT
echo ��� BUILD-N-START (SUCCESS) �� OK ���
goto NORMAL_EXIT

:FAILURE
if "%~0"=="BUILD_N_START_D.cmd" goto ERROR
echo.
echo ��� ## BUILD-N-START (DEBUG) �� FAILURE ���
goto ERROR

::==================================================================================

:RESET_ERROR
set errorlevel=0
set errorlevel=
goto :EOF

::==================================================================================

:ERROR
if "%~0"=="BUILD_N_START_D.cmd" goto ERROR_EXIT
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
