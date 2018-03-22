@echo off
REM Memorize startup path
set "STARTDIR=%~p0"

REM For all entries in list, here "obj\", "bin\" and ".vs"
for %%i in (obj\ bin\ .vs\) do (
	REM Search in %STARTDIR% recursively for directories matchig pattern
	for /d /r "%STARTDIR%" %%a in (%%i) do (
		REM Directory found, delete it
		if exist "%%a" (
			echo "Deleting [%%a]"
			rmdir /s /q "%%a"
		)
	)
)
