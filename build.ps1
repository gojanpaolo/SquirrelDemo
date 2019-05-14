#requires -version 4

function Get-AssemblyVersion($assemblyInfoPath) {
  $pattern = '^\[assembly: AssemblyVersion\("(.*)"\)\]'
  (Get-Content $assemblyInfoPath) | ForEach-Object {
    if ($_ -match $pattern) {
      $assemblyVersionAttribute = [version]$matches[1]
      return "{0}.{1}.{2}" -f $assemblyVersionAttribute.Major, $assemblyVersionAttribute.Minor, $assemblyVersionAttribute.Build
    }
  }
}

function Start-Build {
  $nuget = ".\nuget.exe"
  $vswhere = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"
  $msbuild = & $vswhere -latest -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe | select-object -first 1
  $squirrel = ".\packages\squirrel.windows.*\tools\Squirrel.exe"
  $myAppProject = "MyApp\MyApp.csproj"
  $assemblyVersion = Get-AssemblyVersion "MyApp\Properties\AssemblyInfo.cs"

  & $nuget restore $myAppProject -SolutionDirectory ".\"
  & $msbuild $myAppProject /p:Configuration=Release /p:AllowedReferenceRelatedFileExtensions=.pdb
  & $nuget pack MyApp.nuspec -version $assemblyVersion
  & $squirrel --releasify "MyApp.$assemblyVersion.nupkg"
}

Start-Build
pause
