@SETLOCAL ENABLEEXTENSIONS ENABLEDELAYEDEXPANSION
@ECHO OFF
REM Memorize master directory
SET "STARTDIR=%~dp0"
ECHO.Root directory [%STARTDIR%]

REM Variable settings
SET "DESTPATH=build"
SET "DESTFRAM=netcoreapp2.0"
SET "DESTTYPE=Release"
SET "PLUGINPATH=Plugins"
SET "DELFILEEXT=json pdb"
SET "ARCHTYPES=win-x64 linux-x64 linux-arm"

REM Check commandline parameter for architecture
IF /i [%1] == [] (
	REM Default architecture will be for raspbian => Linux on AR, 32-bit
	SET "DESTARCH=linux-arm"
	GOTO :START
)

REM Only defined types are supported
FOR %%i IN (%ARCHTYPES%) DO (
	IF /i [%1] == [%%i] (
		SET "DESTARCH=%%i"
		GOTO :START
	)
)
ECHO.Selected architecture [%1] not supported.
ECHO.Supported architectures are [%ARCHTYPES%].
GOTO :END

REM Append directory to variable
:APA
	SET "TMPDIR=%~dpn1\\%~n2"
	FOR /f %%F IN ("%TMPDIR%") DO SET "%3=%%~dpnF"
EXIT /B

REM Function DotNet Publish
:DNP
	REM Check for plugin path
	SET PATHINPUT=%~dpn1
	CALL :APA %~dpn2 %DESTARCH% DEPLOYPATH
	IF /i "%PATHINPUT:Plugins=%"=="%PATHINPUT%" (
		SET "SELFCONTAINED=--self-contained"
	) ELSE (
		CALL :APA %DEPLOYPATH% %PLUGINPATH% DEPLOYPATH
		SET "SELFCONTAINED="
	)
	REM If project file exists, publish it
	IF EXIST "%1" (
		ECHO.Publishing [%1] to [%DEPLOYPATH%]
		CD %~dp1
		dotnet publish -c %DESTTYPE% -f %DESTFRAM% %SELFCONTAINED% --force -o "%DEPLOYPATH%" -r %DESTARCH%
	)
EXIT /B

REM Cleanup old version
:CLEANUP
	CALL :APA %1 %DESTARCH% CLEANUPDIR
	IF EXIST "%CLEANUPDIR%" (
		ECHO.Cleaning up [%CLEANUPDIR%]
		RMDIR /s /q "%CLEANUPDIR%"
	)
EXIT /B

REM Remove obsolete files
:ROF
FOR %%i IN (%DELFILEEXT%) DO (
	REM Search for file with given extension
	FOR /r "%1" %%a IN (*.%%i) DO (
		REM Delete file
		DEL /f /q "%%a"
	)
)
EXIT /B

REM For all projects in solution
:START
ECHO.Selected architecture [%DESTARCH%]
CD %STARTDIR%
CALL :APA %STARTDIR% %DESTPATH% DEPLOYBASE
CALL :CLEANUP %DEPLOYBASE%
FOR /f "delims=" %%f IN ('DIR /b /s /a-d *.csproj') DO (
	REM Publish project
	CALL :DNP "%%f" "%DEPLOYBASE%"
)
CD %STARTDIR%
CALL :ROF "%DEPLOYBASE%"

REM Switch back to startup directory
:END
CD %STARTDIR%
