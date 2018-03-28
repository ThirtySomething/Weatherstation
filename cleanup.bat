@echo off
REM Memorize startup path
set "STARTDIR=%~p0"

REM For all entries in list, here "RPi-build", "obj\", "bin\" and ".vs"
for %%i in (RPi-build\ obj\ bin\ .vs\) do (
	REM Search in %STARTDIR% recursively for directories matchig pattern
	for /d /r "%STARTDIR%" %%a in (%%i) do (
		REM Directory found, delete it
		if exist "%%a" (
			echo "Deleting [%%a]"
			rmdir /s /q "%%a"
		)
	)
)

REM Switch back to startup directory
cd %STARTDIR%
