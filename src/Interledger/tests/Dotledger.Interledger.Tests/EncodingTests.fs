namespace DotLedger.InterLedger.UnitTests

module EncodingTests =
    open System
    open System.IO
    open Xunit
    open Interledger.CryptoConditions.Encoding


    [<Fact>]
    let ``OER encoder throws when Stream parameter is null``() =
        let justSomeTestValue = 100
        let justSomeUpperBound = 100

        let exp = Record.Exception(fun _ -> OerEncoding.WriteOerEncodedBoundedUnsignedInteger (null, justSomeTestValue, justSomeUpperBound))

        Assert.NotNull exp;
        Assert.IsType<ArgumentNullException> exp |> ignore
        Assert.Contains ("stream", exp.Message)


    [<Theory>]
    [<ClassData(typeof<OerValueUpperBoundPairs>)>]
    let ``OER encoder encodes OerValueUpperBoundPairs dataset correctly`` (value: int, upperBound: int) =
        let stream = new MemoryStream()
        OerEncoding.WriteOerEncodedBoundedUnsignedInteger (stream, value, upperBound)

        let encodedData = BitConverter.ToInt32 (stream.ToArray(), 0)
        Assert.Equal(value, encodedData)