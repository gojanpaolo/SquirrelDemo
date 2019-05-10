$assemblyInfoCs = "MyApp\Properties\AssemblyInfo.cs"

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

  $myAppProject = "MyApp\MyApp.csproj"

  & $nuget restore $myAppProject -SolutionDirectory ".\"

  & $msbuild $myAppProject /p:Configuration=Release /p:AllowedReferenceRelatedFileExtensions=.pdb

  $assemblyVersion = Get-AssemblyVersion $assemblyInfoCs

  & $nuget pack MyApp.nuspec -version $assemblyVersion

  $squirrel = ".\packages\squirrel.windows.*\tools\Squirrel.exe"

  & $squirrel --releasify "MyApp.$assemblyVersion.nupkg" --no-msi
}
