@echo off
cls
run_d "(localdb)\MSSQLLocalDB" "Librarian" FillTest WithCleanup
echo 
pause
EXIT 255
