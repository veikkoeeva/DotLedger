﻿namespace DotLedger.InterLedger.UnitTests

type DataSetBase(generator: obj[] seq) =
    interface seq<obj[]> with
        member this.GetEnumerator() = generator.GetEnumerator()
        member this.GetEnumerator() = generator.GetEnumerator() :> System.Collections.IEnumerator