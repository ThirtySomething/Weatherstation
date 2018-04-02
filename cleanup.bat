@ECHO OFF
REM Memorize startup path
SET "STARTDIR=%~p0"

REM For all entries in list, here "build", "obj\", "bin\" and ".vs"
FOR %%i IN (build\ obj\ bin\ .vs\) DO (
	REM Search in %STARTDIR% recursively for directories matchig pattern
	FOR /d /r "%STARTDIR%" %%a IN (%%i) DO (
		REM Directory found, delete it
		IF EXIST "%%a" (
			ECHO.Deleting [%%a]
			RMDIR /s /q "%%a"
		)
	)
)

REM Switch back to startup directory
CD %STARTDIR%
