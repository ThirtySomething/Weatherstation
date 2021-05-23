@ECHO OFF
TITLE Buildscript for Weatherstation
REM Memorize master directory
SET "STARTDIR=%~dp0"
SET "GENERATOR=buildscript.bat"
SET "ARCHTYPES=win-x64 linux-x64 linux-arm"

GOTO :START

REM Append directory to variable
:APA
	SET "TMPDIR=%~dpn1\\%~n2"
	FOR /f %%F IN ("%TMPDIR%") DO SET "%3=%%~dpnF"
EXIT /B

:START
CALL :APA %STARTDIR% %GENERATOR% RUNME
FOR %%i IN (%ARCHTYPES%) DO (
	ECHO.Compiling for [%%i]
	CALL %RUNME% %%i > _build_%%i.log 2>&1
)
