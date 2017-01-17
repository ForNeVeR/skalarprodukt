# skalarprodukt [![Status Umbra][status-umbra]][andivionian-status-classifier] [![Build status][badge-appveyor]][build-appveyor]
Experimental multidimensional arrays for F#

## Build

To build the project, execute the following commands (assuming you have `nuget`
and `msbuild` in your `PATH`):

```console
$ nuget restore skalarprodukt.sln
$ msbuild /m /p:Platform="Any CPU" /p:Configuration=Release skalarprodukt.sln
```

## Benchmarks

See the current [benchmark results][benchmark-results].

There's a script to run all the benchmarks and generate the reports:
[`scripts/Run-Benchmarks.ps1`][run-benchmarks].

## Example

```fsharp
// Learn more about F# at http://fsharp.org. See the 'F# Tutorial' project
// for more guidance on F# programming.

#r @"..\..\skalarprodukt\skalarprodukt\bin\Release\skalarprodukt.dll"

open skalarprodukt
open NDArray

let v1 : Vector<_> = NDArray.init 3 id
v1 |> NDArray.mapi (+)
v1 |> NDArray.iteri (fun i v -> printf "(%d) = %d\n" i v)

let m1 : Matrix<_> = NDArray.init (2, 2) (fun (i, j) -> if i = j then 1 else 0)
m1 |> NDArray.mapi (fun (i, j) v -> if i = j then -v else v)
m1 |> NDArray.iteri (fun (i, j) v -> printf "(%d, %d) = %d\n" i j v)
```

## License

skalarprodukt is licensed under the terms of MIT License. See License.md file for
details.

[benchmark-results]: BenchmarkDotNet.Artifacts/results/
[run-benchmarks]: script/Run-Benchmarks.ps1

[andivionian-status-classifier]: https://github.com/ForNeVeR/andivionian-status-classifier#status-umbra-
[build-appveyor]: https://ci.appveyor.com/project/gsomix/skalarprodukt/branch/master

[badge-appveyor]: https://ci.appveyor.com/api/projects/status/41vvocbhhb1hx1hq/branch/master?svg=true
[status-umbra]: https://img.shields.io/badge/status-umbra-red.svg
