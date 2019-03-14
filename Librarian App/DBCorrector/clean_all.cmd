@echo off
setlocal

if "%~0"=="CLEAN_ALL.cmd" goto BEGIN
cls
echo �������������������������������������������������������������������������������
echo �� REMOVING ALL OUTPUT AND INTERMEDIATE FILES/FOLDERS: ������������������������

:BEGIN
call :RESET_ERROR

call #SETTINGS.cmd
if %errorlevel% neq 0 goto ERROR

call :EXEC_CMD MsBuild /nologo "%#ProjectFileName%" /t:"_CleanAll_"
if %errorlevel% neq 0 goto ERROR

if "%~0"=="CLEAN_ALL.cmd" goto FINISH
echo.
echo �������������������������������������������������������������������������������
goto FINISH

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
if "%~0"=="CLEAN_ALL.cmd" goto ERROR_EXIT
echo 
pause

:ERROR_EXIT
endlocal
verify # 2>nul
goto :EOF

:FINISH
endlocal
call :RESET_ERROR
if "%~0"=="CLEAN_ALL.cmd" goto :EOF
echo.
pause
goto :EOF

::==================================================================================
