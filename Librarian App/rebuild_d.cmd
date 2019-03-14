@echo off
setlocal
set initial_cd=%CD%

if "%~0"=="REBUILD_D.cmd" goto BEGIN
cls
echo RE-BUILDING PROJECTS ÄÄ "DEBUG":
echo ßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßßß

:BEGIN
rem call :REBUILD_PROJECT ""
rem if %errorlevel% neq 0 goto FAILURE

call :REBUILD_PROJECT "Librarian.Base"
if %errorlevel% neq 0 goto FAILURE

call :REBUILD_PROJECT "DBCorrector"
if %errorlevel% neq 0 goto FAILURE

rem call :REBUILD_PROJECT "Librarian"
rem if %errorlevel% neq 0 goto FAILURE

rem call :REBUILD_PROJECT "Librarian.TS"
rem if %errorlevel% neq 0 goto FAILURE

call :RESTORE_DIR
::==================================================================================

:SUCCESS
if "%~0"=="REBUILD_D.cmd" goto FINISH
echo.
echo ÜÜÜ REBUILD PROJECTS (DEBUG) ÄÄ SUCCESS ÜÜÜ
goto FINISH

:FAILURE
if "%~0"=="REBUILD_D.cmd" goto ERROR
echo.
echo ÜÜÜ ## REBUILD PROJECTS (DEBUG) ÄÄ FAILURE ÜÜÜ
goto ERROR

::==================================================================================

:REBUILD_PROJECT
call :CHANGE_DIR "%initial_cd%\%~1"
if %errorlevel% neq 0 goto :EOF

call REBUILD_D.cmd
if %errorlevel% neq 0 goto :EOF

echo.
echo ÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍ
goto :EOF

::==================================================================================

:RESET_ERROR
set errorlevel=0
set errorlevel=
goto :EOF

:CHANGE_DIR
echo.
echo * Changing directory to: "%~1" ...
cd /d "%~1"
goto :EOF

:RESTORE_DIR
if /i "%CD%"=="%initial_cd%" goto :EOF
echo.
echo * Restoring directory to: "%initial_cd%" ...
cd /d "%initial_cd%"
if /i "%CD%"=="%initial_cd%" goto :EOF
echo.
echo ## === FATAL ERROR: ===
echo ## Can not restore directory to: "%initial_cd%".
echo ## Batch processing terminated!
echo 
pause
EXIT 255

::==================================================================================

:ERROR
if "%~0"=="REBUILD_D.cmd" goto SKIP_ERROR_PAUSE
echo 
pause

:SKIP_ERROR_PAUSE
call :RESTORE_DIR

:ERROR_EXIT
endlocal
verify # 2>nul
goto :EOF

:FINISH
if "%~0"=="REBUILD_D.cmd" goto SKIP_FINISH_PAUSE
echo.
pause

:SKIP_FINISH_PAUSE
call :RESTORE_DIR

:NORMAL_EXIT
endlocal
call :RESET_ERROR
goto :EOF

::==================================================================================
