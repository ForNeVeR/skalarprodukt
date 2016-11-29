# skalarprodukt [![Status Umbra][status-umbra]][andivionian-status-classifier] [![Build status][badge-appveyor]][build-appveyor]
Experimental multidimensional arrays for F#

## Benchmarks

See current [benchmark results][benchmark-results].

## Example

```fsharp
open skalarprodukt
open skalarprodukt.Providers

open NDArray
open NDims

type ``N = 1`` = NDims<1>
type Vector<'t> = NDArray<'t, ``N = 1``>
let v1 : Vector<_> = NDArray.init 3 id
v1 |> NDArray.mapi (+)
v1 |> NDArray.iteri (fun i v -> printf "(%d) = %d\n" i v)

type ``N = 2`` = NDims<2>
type Matrix<'t> = NDArray<'t, ``N = 2``>
let m1 : Matrix<_> = NDArray.init (2, 2) (fun (i, j) -> if i = j then 1 else 0)
m1 |> NDArray.mapi (fun (i, j) v -> if i = j then -v else v)
m1 |> NDArray.iteri (fun (i, j) v -> printf "(%d, %d) = %d\n" i j v)
```

## License

skalarprodukt is licensed under the terms of MIT License. See License.md file for
details.

[benchmark-results]: docs/benchmark-results.md

[build-appveyor]: https://ci.appveyor.com/project/gsomix/skalarprodukt/branch/master

[badge-appveyor]: https://ci.appveyor.com/api/projects/status/41vvocbhhb1hx1hq/branch/master?svg=true

[andivionian-status-classifier]: https://github.com/ForNeVeR/andivionian-status-classifier#status-umbra-

[status-umbra]: https://img.shields.io/badge/status-umbra-red.svg
