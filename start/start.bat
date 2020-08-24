@echo off
cd "smi.exe"
cls
smi.exe inject -p "Risk of Rain 2" -a "..\bin\Release\UmbraRoR.dll" -n UmbraRoR -c Loader -m Load
pause