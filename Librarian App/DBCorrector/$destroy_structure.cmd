@echo off
cls
run_d "(localdb)\MSSQLLocalDB" "Librarian" "DestroyStructure"
echo 
pause
EXIT 255
