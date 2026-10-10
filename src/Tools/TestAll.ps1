# Tests combinations of build configurations and language versions.

param(
    [Alias('c')]
    [string[]]$Configuration = @('Debug', 'Release'),
    [Alias('l')]
    [int[]]$LanguageVersion = 10..14,
    [switch]$ContinueOnFailure
)

$failed = $false

foreach ($config in $Configuration) {
    Write-Host -ForegroundColor Yellow "Testing configuration: $config"

    foreach ($langVersion in $LanguageVersion) {
        Write-Host -ForegroundColor Yellow " - Language version: $langVersion"
        $output = dotnet test -c $config -p:TestAssembliesLangVersion=$langVersion "$PSScriptRoot/../InlineIL.Tests/InlineIL.Tests.csproj"
        if ($LASTEXITCODE -eq 0) {
            Write-Host -ForegroundColor Green "   ✅ Passed"
        }
        else {
            $failed = $true
            Write-Host -ForegroundColor Red "   ❌ Failed"
            Write-Host ''
            Write-Host $output
            if (-not $ContinueOnFailure) { exit 1 }
        }
        Write-Host ''
    }
}

if ($failed) {
    Write-Host -ForegroundColor Red "❌ Some tests failed"
    exit 1
}
else {
    Write-Host -ForegroundColor Green "✅ All tests passed"
    exit 0
}
