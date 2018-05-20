@ECHO OFF
REM Memorize startup path
SET "STARTDIR=%~p0"
SET "KILLDIRS=bin\ build\ html\ obj\ bin\ .vs\"
SET "KILLFILES=log"

REM For all entries in list %KILLDIRS%
FOR %%i IN (%KILLDIRS%) DO (
	REM Search in %STARTDIR% recursively for directories matchig pattern
	FOR /d /r "%STARTDIR%" %%a IN (%%i) DO (
		REM Directory found, delete it
		IF EXIST "%%a" (
			ECHO.Deleting [%%a]
			RMDIR /s /q "%%a"
		)
	)
)

REM For all entries in list %KILLFILES%
FOR %%i IN (%KILLFILES%) DO (
	REM Search for file with given extension
	FOR /r "%STARTDIR%" %%a IN (*.%%i) DO (
		REM Delete file
		DEL /f /q "%%a"
	)
)

REM Switch back to startup directory
CD %STARTDIR%
