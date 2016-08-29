namespace DotLedger.Interledger.CryptoConditions

open System

type ConditionType =
    | PREIMAGE_SHA256   = 0
    | RSA_SHA256        = 1
    | PREFIX_SHA256     = 2
    | THRESHOLD_SHA256  = 3
    | ED25519           = 4



[<Flags>]
type FeatureSuite =
    | SHA_256   = 1uy
    | PREIMAGE  = 2uy
    | PREFIX    = 4uy
    | THRESHOLD = 8uy
    | RSA_PSS   = 16uy
    | ED25519   = 32uy

type ICondition =
    abstract Type : ConditionType
    abstract Features : seq<FeatureSuite>
    abstract Fingerprint : byte[]
    abstract MaxFulfillmentLength : int
    abstract ToUri : unit -> Uri
    abstract ToBinary : unit -> byte[]



type IFulfillment =
    abstract TypeId : ConditionType
    abstract Features : seq<ConditionType>
    abstract Hash : byte[]
    abstract ToUri : unit -> Uri
    abstract ToBinary : unit -> byte[]
    abstract GenerateCondition : unit -> ICondition
    abstract CalculateMaxFulfillmentSize : unit -> int



//Why are some serial version UIDs negative and some positive? Should these exception classes be abstract in .NET?

[<AbstractClass>]
type UnsupportedConditionException(message) =
    inherit Exception(message)
    static let serialVersionUID = -4173529087643312558L

type UnsupportedFeaturesException(message) =
    inherit UnsupportedConditionException(message)
    static let serialVersionUID = 4065337161784477150L

type UnsupportedLengthException(message) =
    inherit UnsupportedConditionException(message)
    static let serialVersionUID = 6368777371981462844L


type UnsupportedMaxFullfilmentValueException(message) =
    inherit UnsupportedConditionException(message)
    static let serialVersionUID = 6076981317066854350L


namespace Interledger.CryptoConditions.Encoding

open System.IO
open System

[<AbstractClass>]
type DecodingException(message) =
    inherit Exception(message)
    static let serialVersionUID = 7363031559695469596L

type IllegalLengthIndicatorException(message) =
    inherit Exception(message)
    static let serialVersionUID = 2076963320466312387L


module OerEncoding =
    let WriteOerEncodedBoundedUnsignedInteger(stream: Stream, value: int, upperBound) =
        if isNull stream then
            nullArg "stream"

        if upperBound < 1 then
            raise (new ArgumentOutOfRangeException("upperBound", "The upper bound must be greater than zero."))

        if upperBound <= (int)Byte.MaxValue then stream.Write(BitConverter.GetBytes(value), 0, sizeof<int>)
        elif upperBound <= (int)UInt16.MaxValue then
            stream.Write(BitConverter.GetBytes(value >>> 8), 0, sizeof<int>)
            stream.Write(BitConverter.GetBytes(value), 0, sizeof<int>)
        else
            stream.Write(BitConverter.GetBytes(value >>> 24), 0, sizeof<int>)
            stream.Write(BitConverter.GetBytes(value >>> 16), 0, sizeof<int>)
            stream.Write(BitConverter.GetBytes(value >>> 8), 0, sizeof<int>)
            stream.Write(BitConverter.GetBytes(value), 0, sizeof<int>)
        ()