SonarScanner.MSBuild.exe begin /k:"SmartIT.Library" /d:sonar.host.url="http://lenovo-m910x:9000" /d:sonar.token="squ_a4b1f1e520f16eba7af73be3ce04271290bfa44c" /d:sonar.cs.vscoveragexml.reportsPaths=coverage.xml
MSBuild.exe SmartIT.Library.sln /t:Rebuild
dotnet-coverage collect "dotnet test" -f xml -o "coverage.xml"
SonarScanner.MSBuild.exe end /d:sonar.token="squ_a4b1f1e520f16eba7af73be3ce04271290bfa44c"