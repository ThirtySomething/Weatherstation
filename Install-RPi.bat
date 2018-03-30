@setlocal enableextensions enabledelayedexpansion
@ECHO OFF
REM Memorize master directory
SET "STARTDIR=%~dp0"
ECHO.Root directory [%STARTDIR%]

REM Variable settings
SET "DESTPATH=RPi-Build"
SET "DESTARCH=linux-arm"
SET "DESTFRAM=netcoreapp2.0"
SET "DESTTYPE=Release"
SET "PLUGINPATH=Plugins"
SET "STARTUPFILE=_run.sh"

REM Skip functions
GOTO :START

REM Function DotNet Publish
:DNP
	REM Check for plugin path
	SET PATHINPUT=%1
	IF /i "%PATHINPUT:Plugins=%"=="%PATHINPUT%" (
		SET "DEPLOYPATH=%STARTDIR%\%DESTPATH%"
		SET "SELFCONTAINED=--self-contained"
	) ELSE (
		SET "DEPLOYPATH=%STARTDIR%\%DESTPATH%\%PLUGINPATH%"
		SET "SELFCONTAINED="
	)
	REM If project file exists, publish it
	IF EXIST "%1" (
		ECHO.Publishing [%1] to [%DEPLOYPATH%]
		CD %~dp1
		REM dotnet restore
		dotnet publish -c %DESTTYPE% -f %DESTFRAM% %SELFCONTAINED% --force -o "%DEPLOYPATH%" -r %DESTARCH%
	)
EXIT /B

REM Cleanup old version
:CLEANUP
	IF EXIST "%STARTDIR%\%DESTPATH%" (
		RMDIR /s /q "%STARTDIR%\%DESTPATH%"
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
