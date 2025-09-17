@ECHO OFF
SETLOCAL

ECHO === Restoring packages ===
dotnet restore
IF %ERRORLEVEL% NEQ 0 EXIT /B %ERRORLEVEL%

ECHO === Building solution ===
dotnet build --configuration Release
IF %ERRORLEVEL% NEQ 0 EXIT /B %ERRORLEVEL%

ECHO === Running tests ===
dotnet test --configuration Release --no-build
IF %ERRORLEVEL% NEQ 0 EXIT /B %ERRORLEVEL%

ECHO === Build succeeded ===
ENDLOCAL
