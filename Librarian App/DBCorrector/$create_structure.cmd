@echo off
cls
run_d "(localdb)\MSSQLLocalDB" "Librarian" CreateStructure
echo 
pause
EXIT 255
