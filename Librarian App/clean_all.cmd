@echo off
setlocal
set initial_cd=%CD%

if "%~0"=="CLEAN_ALL.cmd" goto BEGIN
cls
echo 様様様様様様様様様様様様様様様様様様様様様様様様様様様様様様様様様様様様様様様�
echo 様 REMOVING ALL OUTPUT AND INTERMEDIATE FILES/FOLDERS: 様様様様様様様様様様様様

:BEGIN
rem call :CLEAN "Librarian"
rem if %errorlevel% neq 0 goto ERROR

call :CLEAN "Librarian.Base"
if %errorlevel% neq 0 goto ERROR

rem call :CLEAN "Librarian.TS"
rem if %errorlevel% neq 0 goto ERROR

call :CLEAN "DBCorrector"
if %errorlevel% neq 0 goto ERROR

rem call :CLEAN ""
rem if %errorlevel% neq 0 goto ERROR

call :RESTORE_DIR

:DONE
if "%~0"=="CLEAN_ALL.cmd" goto FINISH
echo.
echo 様様様様様様様様様様様様様様様様様様様様様様様様様様様様様様様様様様様様様様様�
goto FINISH

::==================================================================================

:CLEAN
call :CHANGE_DIR "%initial_cd%\%~1"
if %errorlevel% neq 0 goto :EOF

call CLEAN_ALL.cmd
if %errorlevel% neq 0 goto :EOF

echo.
echo 様様様様様様様様様様様様様様様様様様様様様様様様様様様様様様様様様様様様様様様�
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
if "%~0"=="CLEAN_ALL.cmd" goto SKIP_ERROR_PAUSE
echo 
pause

:SKIP_ERROR_PAUSE
call :RESTORE_DIR

:ERROR_EXIT
endlocal
verify # 2>nul
goto :EOF

:FINISH
if "%~0"=="CLEAN_ALL.cmd" goto SKIP_FINISH_PAUSE
echo.
pause

:SKIP_FINISH_PAUSE
call :RESTORE_DIR

:NORMAL_EXIT
endlocal
call :RESET_ERROR
goto :EOF

::==================================================================================
