SonarScanner.MSBuild.exe begin /k:"SmartIT.Library" /d:sonar.host.url="http://localhost:9000" /d:sonar.login="squ_e2b19a256771220d4600a4cf75e6a33d9b594a11" /d:sonar.cs.vscoveragexml.reportsPaths=coverage.xml
MSBuild.exe SmartIT.Library.sln /t:Rebuild
dotnet-coverage collect "dotnet test" -f xml -o "coverage.xml"
SonarScanner.MSBuild.exe end /d:sonar.login="squ_e2b19a256771220d4600a4cf75e6a33d9b594a11"