# Tests combinations of build configurations and language versions.

param(
    [Alias('c')]
    [string[]]$Configuration = @('Debug', 'Release'),
    [Alias('l')]
    [int[]]$LanguageVersion = 10..14
)

$ErrorActionPreference = 'Stop'

$testsProject = Join-Path $PSScriptRoot '../InlineIL.Tests/InlineIL.Tests.csproj'
$artifactRoot = Join-Path $testsProject "../bin/TestAll"

if (Test-Path $artifactRoot) {
    Remove-Item -Recurse -Force -Path $artifactRoot
}

New-Item -ItemType Directory -Force -Path $artifactRoot | Out-Null

$runs = foreach ($config in $Configuration) {
    foreach ($langVersion in $LanguageVersion) {
        [pscustomobject]@{ Configuration = $config; LanguageVersion = $langVersion }
    }
}

if ($runs.Count -eq 0) {
    Write-Host ""
    Write-Host -ForegroundColor Red "❌ No tests to run"
    exit 1
}

Write-Host ""
Write-Host "Running tests for $($runs.Count) combination$($runs.Count -eq 1 ? '' : 's')..."
Write-Host ""

$results = $runs | ForEach-Object -Parallel {
    $run = $_
    $artifactsDir = Join-Path $using:artifactRoot $run.Configuration $run.LanguageVersion
    $name = "C# $($run.LanguageVersion) - $($run.Configuration)"

    $commonArgs = @(
        "-c", $run.Configuration,
        "-p:TestAssembliesLangVersion=$($run.LanguageVersion)",
        "--artifacts-path", $artifactsDir,
        $using:testsProject
    )

    Write-Host -ForegroundColor DarkGray "  🔨 Building: $name"
    $output = dotnet build $commonArgs

    if ($LASTEXITCODE -ne 0) {
        Write-Host ""
        Write-Host -ForegroundColor Red "  ❌ Build failed: $name"
        Write-Host -ForegroundColor DarkRed ($output -join [Environment]::NewLine)
        Write-Host ""
        return $false
    }

    Write-Host -ForegroundColor DarkGray "  🧪 Testing:  $name"
    dotnet test --no-build $commonArgs | Out-Null

    if ($LASTEXITCODE -ne 0) {
        Write-Host -ForegroundColor Red "  ❌ Failed:   $name"
        return $false
    }

    Write-Host -ForegroundColor Green "  ✅ Passed:   $name"
    return $true
}

Write-Host ""

if ($results -notcontains $true) {
    Write-Host -ForegroundColor Red "❌ All tests failed"
    exit 1
}
elseif ($results -contains $false) {
    Write-Host -ForegroundColor Red "❌ Some tests failed"
    exit 1
}
else {
    Write-Host -ForegroundColor Green "✅ All tests passed"
    exit 0
}
