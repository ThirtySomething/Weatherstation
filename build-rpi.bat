@ECHO OFF
REM Memorize startup dir
SET "STARTDIR=%~p0"

REM Variable settings
SET "DESTPATH=RPi-Build"
SET "DESTARCH=linux-arm"
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
		dotnet publish -c %DESTTYPE% -f %DESTFRAM% --force -o "%STARTDIR%\\%DESTPATH%" -r %DESTARCH%
	)
EXIT /B

REM For all projects in solution
:START
FOR /f "delims=" %%f IN ('DIR /b /s /a-d *.csproj') DO (
	REM Publish project
	CALL :dnp "%%f"
)

REM Switch back to startup directory
cd %STARTDIR%
