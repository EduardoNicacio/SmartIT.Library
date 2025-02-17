SonarScanner.MSBuild.exe begin /k:"SmartIT.Library" /d:sonar.login="squ_78371717947f879b6ff131a2e9102becf80fc5a3" /d:sonar.cs.vscoveragexml.reportsPaths=coverage.xml
MSBuild.exe SmartIT.Library.sln /t:Rebuild
dotnet-coverage collect "dotnet test" -f xml -o "coverage.xml"
SonarScanner.MSBuild.exe end /d:sonar.login="squ_78371717947f879b6ff131a2e9102becf80fc5a3"