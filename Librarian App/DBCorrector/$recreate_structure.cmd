@echo off
cls
run_d "(localdb)\MSSQLLocalDB" "Librarian" RecreateStructure
echo 
pause
EXIT 255
