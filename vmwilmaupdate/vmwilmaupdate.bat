@ECHO off

SET %VMRUN%=C:\Program Files (x86)\VMware\VMware VIX\vmrun.exe
SET %PSEXEC%=PsExec.exe

rem for each vm
rem %VMRUN% runProgramInGuest 