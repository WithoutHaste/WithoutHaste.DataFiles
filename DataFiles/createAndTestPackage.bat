echo "Build release version of DataFiles project..."
CALL compileReleaseProjects.bat

echo "Build Nuget package.."
nuget pack WithoutHaste.DataFiles.nuspec

echo "Copy package to local source folder..."
xcopy *.nupkg ..\..\..\NuGetTestSource

