$BuildDir = "ZOMGOctocatz\bin\Release\"

# Build
& "C:\Program Files (x86)\Microsoft Visual Studio 10.0\VC\vcvarsall.bat"
& MSBuild "ZOMGOctocatz.sln" /t:Rebuild /p:Configuration=Release /v:m

Copy-Item readme.md $BuildDir\readme.md

# Zip it
cd ZOMGOctocatz\bin\Release
7z a -tzip ZOMGOctocatz.zip ZOMGOctocatz.exe RestSharp.dll readme.md

# Go back home yo
cd ..\..\..

Remove-Item -recurse $BuildDir\* -exclude *.exe,*.zip,*.vshost.exe
