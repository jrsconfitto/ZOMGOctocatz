$BuildDir = "ZOMGOctocatz\bin\Release\"

# Build
& MSBuild "ZOMGOctocatz.sln" /t:Rebuild /p:Configuration=Release /v:m

Copy-Item readme.md $BuildDir\readme.md

# Zip it
cd ZOMGOctocatz\bin\Release
..\..\..\tools\7zip\7z a -tzip ZOMGOctocatz.zip ZOMGOctocatz.exe RestSharp.dll readme.md

# Go back home yo
cd ..\..\..

Remove-Item -recurse $BuildDir\* -exclude *.exe,*.zip,*.vshost.exe
