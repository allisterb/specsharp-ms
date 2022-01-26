@echo off
@setlocal
pushd
set ERROR_CODE=0
dotnet build Microsoft.SpecSharp\CommandLineCompiler\ssc.csproj %*

:end
popd
exit /B %ERROR_CODE%