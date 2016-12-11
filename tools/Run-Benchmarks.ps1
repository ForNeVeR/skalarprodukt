param (
    $Binary = "$PSScriptRoot/../skalarprodukt.Benchmark/bin/Release/skalarprodukt.Benchmark.exe"
)

$ErrorActionPreference = 'Stop'

function Extract-BenchmarkList {
    Write-Host 'Determining available benchmarks'
    $benchmarks = Write-Output 'x' | & $Binary | ? { $_.Trim().StartsWith('#') } | % {
        $parts = $_.Trim().Substring(1).Split(' ')
        $number = $parts[0]
        $name = $parts[1]
        [PSCustomObject]@{ Number = $number; Name = $name }
    }
    Write-Host 'Benchmarks detected:'
    Write-Host ($benchmarks -join "`n")
    $benchmarks
}

Extract-BenchmarkList | % {
    $benchmark = $_
    & $Binary $benchmark.Number --exporters markdown
}
