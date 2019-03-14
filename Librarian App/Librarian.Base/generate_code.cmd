@echo off
setlocal

if "%~0"=="GENERATE_CODE.cmd" goto BEGIN
cls
echo GENERATING CODE FILE(S):
echo ßßßßßßßßßßßßßßßßßßßßßßßß

:BEGIN
call :RESET_ERROR

call #SETTINGS.cmd
if %errorlevel% neq 0 goto ERROR

call :EXEC_CMD MsBuild /nologo "%#ProjectFileName%" /t:"_GenerateCode_" /p:Configuration="Debug"
if %errorlevel% neq 0 goto FAILURE

::==================================================================================

:SUCCESS
if "%~0"=="GENERATE_CODE.cmd" goto FINISH
echo.
echo ÜÜÜ GENERATE CODE ÄÄ SUCCESS ÜÜÜ
goto FINISH

:FAILURE
if "%~0"=="GENERATE_CODE.cmd" goto ERROR
echo.
echo ÜÜÜ ## GENERATE CODE ÄÄ FAILURE ÜÜÜ
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
if "%~0"=="GENERATE_CODE.cmd" goto ERROR_EXIT
echo 
pause

:ERROR_EXIT
endlocal
verify # 2>nul
goto :EOF

:FINISH
endlocal
call :RESET_ERROR
if "%~0"=="GENERATE_CODE.cmd" goto :EOF
echo.
pause
goto :EOF

::==================================================================================
