msbuild.exe MSBuild\Visyn.Public.proj /t:Clean /p:Configuration=Debug
msbuild.exe MSBuild\Visyn.Public.proj /t:Build /p:AllowUnsafeBlocks=true;Configuration=Debug