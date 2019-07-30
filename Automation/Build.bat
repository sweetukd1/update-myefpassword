setlocal EnableDelayedExpansion

@rem setting the two most common paths for msbuild installation, one test coming from BuildTools2017 and one coming from VisualStudio 2017
SET DEFAULTMSBUILDFROMBUILDTOOLS_PATH="C:\Program Files (x86)\Microsoft Visual Studio\2017\BuildTools\MSBuild\15.0\Bin"
ECHO.%JOB_NAME% | FIND /I "-DEV">Nul && ( SET FOLDER_NAME=DEV)
ECHO.%JOB_NAME% | FIND /I "-QA">Nul && ( SET FOLDER_NAME=QA)
ECHO.%JOB_NAME% | FIND /I "-LIVE">Nul && ( SET FOLDER_NAME=LIVE)

if EXIST %DEFAULTMSBUILDFROMBUILDTOOLS_PATH% SET DEFAULTMSBUILD_PATH=%DEFAULTMSBUILDFROMBUILDTOOLS_PATH%

cd..
::echo The current directory is %CD%
for %%x in (*.sln) do (
    set SOLN_NAME=%%x)
	
::echo %SOLN_NAME%

C:\Tools\nuget.exe  restore "%SOLN_NAME%" -ConfigFile "C:\Tools\Nuget.config"


SET SOLUTION_NAME=%SOLN_NAME%
 FOR /f "tokens=1 delims=." %%a IN ("%SOLUTION_NAME%") DO (
 SET SOLUTION_FOLDER=%%a)

ECHO Attempting to build using the path %DEFAULTMSBUILD_PATH%.... 
%DEFAULTMSBUILD_PATH%\msbuild "%SOLN_NAME%" /p:Configuration=Release /p:DeployOnBuild=true /p:OutputPath=D:\%SOLUTION_FOLDER%\%FOLDER_NAME%\%BUILD_NUMBER%

:: Storing Solution folder  name into text file
echo %SOLUTION_FOLDER%>Automation\%FOLDER_NAME%\SOLUTION_FOLDER.txt