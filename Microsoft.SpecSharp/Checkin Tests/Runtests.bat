@echo off
xcopy /Q /Y ..\..\bin\Microsoft.SpecSharp.Runtime.* .
xcopy /Q /Y ..\..\bin\System.Compiler.* .
xcopy /Q /Y ..\..\bin\Microsoft.SpecSharp.* .
xcopy /Q /Y ..\..\bin\Microsoft.VisualStudio.* .
xcopy /Q /Y ..\..\bin\ssc.exe .

set errors=0
set test=%1
if %1x == x set test=*
REM This is necessary because the tests currently fail in 64-bit mode.
corflags /Force /nologo /32BIT+ ssc.exe
ssc.exe %test%.suite
if %ERRORLEVEL% NEQ 0 set errors=1
if %errors% NEQ 0 goto End

..\IncrementalCompilationTests\bin\IncrementalCompilationTests.exe IncrementalCompilationSuite.txt
if %ERRORLEVEL% NEQ 0 set errors=1

REM These tests aren't in a test suite since they appear do not work unless they get loaded from disk.
csc /nologo /warn:0 /r:System.Compiler.dll /debug GetRuntimeAttributeTest1.cs
GetRuntimeAttributeTest1.exe
if %ERRORLEVEL% NEQ 0 set errors=1
csc /nologo /warn:0 /r:System.Compiler.dll /debug GetRuntimeAttributeTest2.cs
GetRuntimeAttributeTest2.exe
if %ERRORLEVEL% NEQ 0 set errors=1


:End
exit /b %errors%
