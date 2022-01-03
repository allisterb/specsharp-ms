@echo off

..\..\bin\ssc.exe /p:v4 /debug /t:library /nostdlib /r:mscorlib.dll /keyfile:..\..\Common\InterimKey.snk /r:..\..\System.Compiler.Runtime\bin\System.Compiler.Runtime.dll /out:bin\Microsoft.SpecSharp.Runtime.dll ..\..\Common\Include\Version.cs Classes.ssc Collections.ssc