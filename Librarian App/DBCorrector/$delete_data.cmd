@echo off
cls
run_d "(localdb)\MSSQLLocalDB" "Librarian" DeleteData
echo 
pause
EXIT 255
