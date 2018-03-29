@ECHO OFF
REM Memorize startup dir
SET "STARTDIR=%~p0"

REM Variable settings
SET "DESTPATH=RPi-Build"
SET "DESTARCH=linux-arm"
REM SET "DESTARCH=linux-x64"
SET "DESTFRAM=netcoreapp2.0"
SET "DESTTYPE=Release"

REM Skip functions
GOTO :START

REM Function DotNet Publish
:DNP
	REM If project file exists, publish it
	IF EXIST "%1" (
		ECHO.Publishing [%1]
		CD %~dp1
		dotnet publish -c %DESTTYPE% -f %DESTFRAM% --self-contained --force -o "%STARTDIR%\\%DESTPATH%" -r %DESTARCH%
	)
EXIT /B

REM Cleanup old crap
:CLEANUP
	IF EXIST "%STARTDIR%\\%DESTPATH%" (
		RMDIR /s /q "%STARTDIR%\\%DESTPATH%"
	)
EXIT /B

REM For all projects in solution
:START
CALL :CLEANUP
FOR /f "delims=" %%f IN ('DIR /b /s /a-d *.csproj') DO (
	REM Publish project
	CALL :DNP "%%f"
)

REM Switch back to startup directory
CD %STARTDIR%
