Import-Module $PSScriptRoot\..\Tools\BuildScripts.ps1 -Force

$project=Last $PSScriptRoot.Split("\") -1    # project name from directory
$start = Script-Banner $project

$Verbose = $args | Get-Flag -Name:"-Verbose"
$Trace = $args | Get-Flag -Name:"-Trace"
$solutionDir =[System.IO.Path]::Combine($PSScriptRoot,"MSBuild")

msbuild.exe $PSScriptRoot\MSBuild\$project.proj /t:Clean /p:Configuration=Debug | Error-Coloring -Verbose:$Verbose -Trace:$Trace
msbuild.exe $PSScriptRoot\MSBuild\$project.proj /t:Clean /p:Configuration=Release | Error-Coloring -Verbose:$Verbose -Trace:$Trace

Script-Footer $start $project

