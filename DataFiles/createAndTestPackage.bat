REM save current directory
set startingDirectory=%CD%

echo "Build release version of DataFiles project..."
REM CALL compileReleaseProjects.bat

echo "Build Nuget package.."
REM nuget pack WithoutHaste.DataFiles.nuspec

echo "Copy package to local source folder..."
REM xcopy *.nupkg ..\..\..\NuGetTestSource

echo "Run InstallationTestsSetup..."
cd ..\InstallationTests
START /WAIT Setup\InstallationTestsSetup\bin\Release\InstallationTestsSetup.exe

echo "Install NuGet package in each auto-generatedsolution..."
set installationTestsDirectory=%CD%
set frameworks=20 30 35 40 45 451 452 46 461 462 47 471 472
(for %%f in (%frameworks%) do ( 
	cd %installationTestsDirectory%
	cd AutoGenerated\net%%f
	REM nuget install 
))

echo "Return to starting directory..."
cd %startingDirectory%