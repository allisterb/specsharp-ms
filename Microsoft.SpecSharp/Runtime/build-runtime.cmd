@echo off

..\..\bin\ssc.exe /platform:v4 /debug /t:library /nostdlib /r:mscorlib.dll /r:..\..\bin\System.Compiler.Runtime.dll /out:bin\Microsoft.SpecSharp.Runtime.dll ..\..\Common\Include\Version.cs Classes.ssc Collections.ssc