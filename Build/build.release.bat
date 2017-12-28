msbuild.exe MSBuild\Visyn.Public.proj /t:Clean /p:Configuration=Release
msbuild.exe MSBuild\Visyn.Public.proj /T:SharedBuild_Validate
msbuild.exe MSBuild\Visyn.Public.proj /t:Build /p:AllowUnsafeBlocks=true;Configuration=Release