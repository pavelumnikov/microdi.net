set VSTOOLS="%VS140COMNTOOLS%"
for /F "usebackq" %%i in (`echo "%PATH%" ^| find /I /C "%WINDIR%\Microsoft.NET\Framework\v4.0.30319"`) do set FOUNDMS40=%%i
if %VSTOOLS%=="" (
  if "%FOUNDMS40%"=="0" (set "PATH=%PATH%;%WINDIR%\Microsoft.NET\Framework\v4.0.30319")
  goto skipvsinit
)

call "%VSTOOLS:~1,-1%vsvars32.bat"
if errorlevel 1 goto end

:skipvsinit

msbuild.exe microDI.sln /p:Configuration=Debug

if errorlevel 1 goto end

set NUNIT_TOOL="C:\Program Files (x86)\NUnit.org\nunit-console"
%NUNIT_TOOL%\nunit3-console.exe .\Tests\microDI.UnitTests\bin\Debug\microDI.UnitTests.dll

:end
exit /b %ERRORLEVEL%