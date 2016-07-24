﻿#if TYPESHAPE_HIDE
module internal TypeShape
#else
module TypeShape
#endif

#nowarn "4224"

open System
open System.Collections.Generic
open System.Reflection
open Microsoft.FSharp.Reflection

//////////////////////////////////////////////////
///////////// Section: TypeShape core definitions

type ITypeShapeVisitor<'R> =
    abstract Visit<'T> : unit -> 'R

[<AbstractClass>]
type TypeShape internal () =
    abstract Type : Type
    abstract Accept : ITypeShapeVisitor<'R> -> 'R
    override s.ToString() = sprintf "Shape [%O]" (s.GetType())

type TypeShape<'T> =
    inherit TypeShape
    [<CompilerMessage("TypeShape<'T> constructor should only be used when inheriting shape implementations.", 4224)>]
    new () = { inherit TypeShape() }
    override __.Type = typeof<'T>
    override __.Accept v = v.Visit<'T> ()




//////////////////////////////////////
///////////// Section: Core BCL types

///////////// Enum types

type IEnumVisitor<'R> =
    abstract Visit<'Enum, 'Underlying when 'Enum : enum<'Underlying>> : unit -> 'R

type IShapeEnum =
    abstract Accept : IEnumVisitor<'R> -> 'R

type private ShapeEnum<'Enum, 'Underlying when 'Enum : enum<'Underlying>>() =
    inherit TypeShape<'Enum> ()
    interface IShapeEnum with
        member __.Accept v = v.Visit<'Enum, 'Underlying> ()

///////////// Nullable types

type INullableVisitor<'R> =
    abstract Visit<'T when 'T : (new : unit -> 'T) and 'T :> ValueType and 'T : struct> : unit -> 'R

type IShapeNullable =
    abstract Accept : INullableVisitor<'R> -> 'R

type private ShapeNullable<'T when 'T : (new : unit -> 'T) and 'T :> ValueType and 'T : struct>() =
    inherit TypeShape<Nullable<'T>>()
    interface IShapeNullable with
        member __.Accept v = v.Visit<'T> ()

///////////// System.Tuple

type IShapeTuple = interface end

type ITuple1Visitor<'R> =
    abstract Visit<'T> : unit -> 'R

type IShapeTuple1 =
    inherit IShapeTuple
    abstract Accept : ITuple1Visitor<'R> -> 'R

type private ShapeTuple<'T> () =
    inherit TypeShape<System.Tuple<'T>> ()
    interface IShapeTuple1 with
        member __.Accept v = v.Visit<'T> ()

type ITuple2Visitor<'R> =
    abstract Visit<'T1, 'T2> : unit -> 'R

type IShapeTuple2 =
    inherit IShapeTuple
    abstract Accept : ITuple2Visitor<'R> -> 'R

type private ShapeTuple<'T1, 'T2> () =
    inherit TypeShape<'T1 * 'T2> ()
    interface IShapeTuple2 with
        member __.Accept v = v.Visit<'T1,'T2> ()

type ITuple3Visitor<'R> =
    abstract Visit<'T1, 'T2, 'T3> : unit -> 'R

type IShapeTuple3 =
    inherit IShapeTuple
    abstract Accept : ITuple3Visitor<'R> -> 'R

type private ShapeTuple<'T1, 'T2, 'T3> () =
    inherit TypeShape<'T1 * 'T2 * 'T3> ()
    interface IShapeTuple3 with
        member __.Accept v = v.Visit<'T1, 'T2, 'T3> ()

type ITuple4Visitor<'R> =
    abstract Visit<'T1, 'T2, 'T3, 'T4> : unit -> 'R

type IShapeTuple4 =
    inherit IShapeTuple
    abstract Accept : ITuple4Visitor<'R> -> 'R

type private ShapeTuple<'T1, 'T2, 'T3, 'T4> () =
    inherit TypeShape<'T1 * 'T2 * 'T3 * 'T4> ()
    interface IShapeTuple4 with
        member __.Accept v = v.Visit<'T1, 'T2, 'T3, 'T4> ()

type ITuple5Visitor<'R> =
    abstract Visit<'T1, 'T2, 'T3, 'T4, 'T5> : unit -> 'R

type IShapeTuple5 =
    inherit IShapeTuple
    abstract Accept : ITuple5Visitor<'R> -> 'R

type private ShapeTuple<'T1, 'T2, 'T3, 'T4, 'T5> () =
    inherit TypeShape<'T1 * 'T2 * 'T3 * 'T4 * 'T5> ()
    interface IShapeTuple5 with
        member __.Accept v = v.Visit<'T1, 'T2, 'T3, 'T4, 'T5> ()

type ITuple6Visitor<'R> =
    abstract Visit<'T1, 'T2, 'T3, 'T4, 'T5, 'T6> : unit -> 'R

type IShapeTuple6 =
    inherit IShapeTuple
    abstract Accept : ITuple6Visitor<'R> -> 'R

type private ShapeTuple<'T1, 'T2, 'T3, 'T4, 'T5, 'T6> () =
    inherit TypeShape<'T1 * 'T2 * 'T3 * 'T4 * 'T5 * 'T6> ()
    interface IShapeTuple6 with
        member __.Accept v = v.Visit<'T1, 'T2, 'T3, 'T4, 'T5, 'T6> ()

type ITuple7Visitor<'R> =
    abstract Visit<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7> : unit -> 'R

type IShapeTuple7 =
    inherit IShapeTuple
    abstract Accept : ITuple7Visitor<'R> -> 'R

type private ShapeTuple<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7> () =
    inherit TypeShape<'T1 * 'T2 * 'T3 * 'T4 * 'T5 * 'T6 * 'T7> ()
    interface IShapeTuple7 with
        member __.Accept v = v.Visit<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7> ()

type ITuple8Visitor<'R> =
    abstract Visit<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'TRest> : unit -> 'R

type IShapeTuple8 =
    inherit IShapeTuple
    abstract Accept : ITuple8Visitor<'R> -> 'R

type private ShapeTuple<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'TRest> () =
    inherit TypeShape<Tuple<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'TRest>> ()
    interface IShapeTuple8 with
        member __.Accept v = v.Visit<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'TRest> ()

/////////// F# functions

type IFSharpFuncVisitor<'R> =
    abstract Visit<'Domain, 'CoDomain> : unit -> 'R

type IShapeFSharpFunc =
    abstract Accept : IFSharpFuncVisitor<'R> -> 'R

type private ShapeFSharpFunc<'T, 'Union> () =
    inherit TypeShape<'T -> 'Union> ()
    interface IShapeFSharpFunc with
        member __.Accept v = v.Visit<'T, 'Union> ()

/////////// System.Exception

type IExceptionVisitor<'R> =
    abstract Visit<'exn when 'exn :> exn> : unit -> 'R

type IShapeException =
    abstract Accept : IExceptionVisitor<'R> -> 'R

type private ShapeException<'exn when 'exn :> exn> () =
    inherit TypeShape<'exn> ()
    interface IShapeException with
        member __.Accept v = v.Visit<'exn> ()


/////////////////////////////////////////////////
///////////// Section: Collections & IEnumerable

///////////// IEnumerable

type IEnumerableVisitor<'R> =
    abstract Visit<'T> : unit -> 'R

type IShapeEnumerable =
    abstract Accept : IEnumerableVisitor<'R> -> 'R

type private ShapeEnumerable<'T>() =
    inherit TypeShape<seq<'T>> ()
    interface IShapeEnumerable with
        member __.Accept v = v.Visit<'T> ()

///////////// Collection

type ICollectionVisitor<'R> =
    abstract Visit<'T> : unit -> 'R

type IShapeCollection =
    abstract Accept : ICollectionVisitor<'R> -> 'R

type private ShapeCollection<'T>() =
    inherit TypeShape<ICollection<'T>>()
    interface IShapeCollection with
        member __.Accept v = v.Visit<'T> ()

    interface IShapeEnumerable with
        member __.Accept v = v.Visit<'T> ()

///////////// KeyValuePair

type IKeyValuePairVisitor<'R> =
    abstract Visit<'K, 'V> : unit -> 'R

type IShapeKeyValuePair =
    abstract Accept : IKeyValuePairVisitor<'R> -> 'R

type private ShapeKeyValuePair<'K,'V> () =
    inherit TypeShape<KeyValuePair<'K,'V>> ()
    interface IShapeKeyValuePair with
        member __.Accept v = v.Visit<'K, 'V> ()

///////////// System.Array

///////////// Array 1D

type IArrayVisitor<'R> =
    abstract Visit<'T> : unit -> 'R

type IShapeArray =
    abstract Accept : IArrayVisitor<'R> -> 'R

type private ShapeArray<'T>() =
    inherit TypeShape<'T []> ()
    interface IShapeArray with
        member __.Accept v = v.Visit<'T> ()

    interface IShapeCollection with
        member __.Accept v = v.Visit<'T> ()

    interface IShapeEnumerable with
        member __.Accept v = v.Visit<'T> ()

///////////// Array 2D

type IArray2DVisitor<'R> =
    abstract Visit<'T> : unit -> 'R

type IShapeArray2D =
    abstract Accept : IArray2DVisitor<'R> -> 'R

type private ShapeArray2D<'T>() =
    inherit TypeShape<'T [,]> ()
    interface IShapeArray2D with
        member __.Accept v = v.Visit<'T> ()

///////////// Array 3D

type IArray3DVisitor<'R> =
    abstract Visit<'T> : unit -> 'R

type IShapeArray3D =
    abstract Accept : IArray3DVisitor<'R> -> 'R

type private ShapeArray3D<'T>() =
    inherit TypeShape<'T [,,]> ()
    interface IShapeArray3D with
        member __.Accept v = v.Visit<'T> ()

///////////// Array 4D

type IArray4DVisitor<'R> =
    abstract Visit<'T> : unit -> 'R

type IShapeArray4D =
    abstract Accept : IArray4DVisitor<'R> -> 'R

type private ShapeArray4D<'T>() =
    inherit TypeShape<'T [,,,]> ()
    interface IShapeArray4D with
        member __.Accept v = v.Visit<'T> ()

///////////// System.Collections.List

type IResizeArrayVisitor<'R> =
    abstract Visit<'T> : unit -> 'R

type IShapeResizeArray =
    abstract Accept : IResizeArrayVisitor<'R> -> 'R

type private ShapeResizeArray<'T> () =
    inherit TypeShape<ResizeArray<'T>> ()
    interface IShapeEnumerable with
        member __.Accept v = v.Visit<'T> ()
    interface IShapeCollection with
        member __.Accept v = v.Visit<'T> ()
    interface IShapeResizeArray with
        member __.Accept v = v.Visit<'T> ()


///////////// System.Collections.Dictionary

type IDictionaryVisitor<'R> =
    abstract Visit<'K, 'V when 'K : equality> : unit -> 'R

type IShapeDictionary =
    abstract Accept : IDictionaryVisitor<'R> -> 'R

type private ShapeDictionary<'K, 'V when 'K : equality> () =
    inherit TypeShape<Dictionary<'K, 'V>> ()
    interface IShapeEnumerable with
        member __.Accept v = v.Visit<KeyValuePair<'K, 'V>> ()
    interface IShapeCollection with
        member __.Accept v = v.Visit<KeyValuePair<'K, 'V>> ()
    interface IShapeDictionary with
        member __.Accept v = v.Visit<'K, 'V> ()

///////////// System.Collections.HashSet

type IHashSetVisitor<'R> =
    abstract Visit<'T when 'T : equality> : unit -> 'R

type IShapeHashSet =
    abstract Accept : IHashSetVisitor<'R> -> 'R

type private ShapeHashSet<'T when 'T : equality> () =
    inherit TypeShape<HashSet<'T>> ()
    interface IShapeEnumerable with
        member __.Accept v = v.Visit<'T> ()
    interface IShapeCollection with
        member __.Accept v = v.Visit<'T> ()
    interface IShapeHashSet with
        member __.Accept v = v.Visit<'T> ()

///////////// F# Set

type IFSharpSetVisitor<'R> =
    abstract Visit<'T when 'T : comparison> : unit -> 'R

type IShapeFSharpSet =
    abstract Accept : IFSharpSetVisitor<'R> -> 'R

type private ShapeFSharpSet<'T when 'T : comparison> () =
    inherit TypeShape<Set<'T>> ()
    interface IShapeEnumerable with
        member __.Accept v = v.Visit<'T> ()
    interface IShapeCollection with
        member __.Accept v = v.Visit<'T> ()
    interface IShapeFSharpSet with
        member __.Accept v = v.Visit<'T> ()

///////////// F# Map

type IFSharpMapVisitor<'R> =
    abstract Visit<'K, 'V when 'K : comparison> : unit -> 'R

type IShapeFSharpMap =
    abstract Accept : IFSharpMapVisitor<'R> -> 'R

type private ShapeFSharpMap<'K, 'V when 'K : comparison> () =
    inherit TypeShape<Map<'K,'V>> ()
    interface IShapeEnumerable with
        member __.Accept v = v.Visit<KeyValuePair<'K, 'V>> ()
    interface IShapeCollection with
        member __.Accept v = v.Visit<KeyValuePair<'K, 'V>> ()
    interface IShapeFSharpMap with
        member __.Accept v = v.Visit<'K, 'V>()

//////////////////////////////////
///////////// Section: F# Records

type private RecordInfo =
    {
        RProperties : PropertyInfo []
        RCtor : ConstructorInfo
    }

type IShapeFSharpRecord =
    abstract IsFSharpRef : bool
    abstract ConstructorInfo : ConstructorInfo
    abstract Properties : PropertyInfo list

///////////// Record arity 1

type IShapeFSharpRecord1 =
    inherit IShapeFSharpRecord
    abstract Accept<'R> : IFSharpRecord1Visitor<'R> -> 'R

and IShapeFSharpRecord<'Record, 'Field1> =
    inherit IShapeFSharpRecord1
    abstract Construct : 'Field1 -> 'Record
    abstract Project1 : 'Record -> 'Field1

and IFSharpRecord1Visitor<'R> =
    abstract Visit<'Record, 'Field1> : IShapeFSharpRecord<'Record, 'Field1> -> 'R

type private ShapeFSharpRecord<'Record, 'Field1> (info : RecordInfo) =
    inherit TypeShape<'Record>()
    interface IShapeFSharpRecord<'Record, 'Field1> with
        member __.IsFSharpRef = false
        member __.ConstructorInfo = info.RCtor
        member __.Properties = info.RProperties |> Array.toList
        member __.Construct(f1 : 'Field1) = info.RCtor.Invoke [|f1|] :?> 'Record
        member __.Project1(record : 'Record) = info.RProperties.[0].GetValue(record, null) :?> 'Field1
        member s.Accept v = v.Visit<'Record, 'Field1> s

///////////// Record arity 2

type IShapeFSharpRecord2 =
    inherit IShapeFSharpRecord
    abstract Accept<'R> : IFSharpRecord2Visitor<'R> -> 'R

and IShapeFSharpRecord<'Record, 'Field1, 'Field2> =
    inherit IShapeFSharpRecord2
    abstract Construct : 'Field1 * 'Field2 -> 'Record
    abstract Project1 : 'Record -> 'Field1
    abstract Project2 : 'Record -> 'Field2

and IFSharpRecord2Visitor<'R> =
    abstract Visit<'Record, 'Field1, 'Field2> : IShapeFSharpRecord<'Record, 'Field1, 'Field2> -> 'R

type private ShapeFSharpRecord<'Record, 'Field1, 'Field2> (info : RecordInfo) =
    inherit TypeShape<'Record>()
    interface IShapeFSharpRecord<'Record, 'Field1, 'Field2> with
        member __.IsFSharpRef = false
        member __.Properties = info.RProperties |> Array.toList
        member __.ConstructorInfo = info.RCtor
        member __.Construct(f1 : 'Field1, f2 : 'Field2) = info.RCtor.Invoke [|f1; f2|] :?> 'Record
        member __.Project1(record : 'Record) = info.RProperties.[0].GetValue(record, null) :?> 'Field1
        member __.Project2(record : 'Record) = info.RProperties.[1].GetValue(record, null) :?> 'Field2
        member s.Accept v = v.Visit<'Record, 'Field1, 'Field2> s

///////////// Record arity 3

type IShapeFSharpRecord3 =
    inherit IShapeFSharpRecord
    abstract Accept<'R> : IFSharpRecord3Visitor<'R> -> 'R

and IShapeFSharpRecord<'Record, 'Field1, 'Field2, 'Field3> =
    inherit IShapeFSharpRecord3
    abstract Construct : 'Field1 * 'Field2 * 'Field3 -> 'Record
    abstract Project1 : 'Record -> 'Field1
    abstract Project2 : 'Record -> 'Field2
    abstract Project3 : 'Record -> 'Field3

and IFSharpRecord3Visitor<'R> =
    abstract Visit<'Record, 'Field1, 'Field2, 'Field3> : IShapeFSharpRecord<'Record, 'Field1, 'Field2, 'Field3> -> 'R

type private ShapeFSharpRecord<'Record, 'Field1, 'Field2, 'Field3> (info : RecordInfo) =
    inherit TypeShape<'Record>()
    interface IShapeFSharpRecord<'Record, 'Field1, 'Field2, 'Field3> with
        member __.IsFSharpRef = false
        member __.ConstructorInfo = info.RCtor
        member __.Properties = info.RProperties |> Array.toList
        member __.Construct(f1 : 'Field1, f2 : 'Field2, f3 : 'Field3) = info.RCtor.Invoke [|f1; f2; f3|] :?> 'Record
        member __.Project1(record : 'Record) = info.RProperties.[0].GetValue(record, null) :?> 'Field1
        member __.Project2(record : 'Record) = info.RProperties.[1].GetValue(record, null) :?> 'Field2
        member __.Project3(record : 'Record) = info.RProperties.[2].GetValue(record, null) :?> 'Field3
        member s.Accept v = v.Visit<'Record, 'Field1, 'Field2, 'Field3> s

///////////// Record arity 4

type IShapeFSharpRecord4 =
    inherit IShapeFSharpRecord
    abstract Accept<'R> : IFSharpRecord4Visitor<'R> -> 'R

and IShapeFSharpRecord<'Record, 'Field1, 'Field2, 'Field3, 'Field4> =
    inherit IShapeFSharpRecord4
    abstract Construct : 'Field1 * 'Field2 * 'Field3 * 'Field4 -> 'Record
    abstract Project1 : 'Record -> 'Field1
    abstract Project2 : 'Record -> 'Field2
    abstract Project3 : 'Record -> 'Field3
    abstract Project4 : 'Record -> 'Field4

and IFSharpRecord4Visitor<'R> =
    abstract Visit<'Record, 'Field1, 'Field2, 'Field3, 'Field4> : IShapeFSharpRecord<'Record, 'Field1, 'Field2, 'Field3, 'Field4> -> 'R

type private ShapeFSharpRecord<'Record, 'Field1, 'Field2, 'Field3, 'Field4> (info : RecordInfo) =
    inherit TypeShape<'Record>()
    interface IShapeFSharpRecord<'Record, 'Field1, 'Field2, 'Field3, 'Field4> with
        member __.IsFSharpRef = false
        member __.ConstructorInfo = info.RCtor
        member __.Properties = info.RProperties |> Array.toList
        member __.Construct(f1 : 'Field1, f2 : 'Field2, f3 : 'Field3, f4 : 'Field4) = info.RCtor.Invoke [|f1; f2; f3; f4|] :?> 'Record
        member __.Project1(record : 'Record) = info.RProperties.[0].GetValue(record, null) :?> 'Field1
        member __.Project2(record : 'Record) = info.RProperties.[1].GetValue(record, null) :?> 'Field2
        member __.Project3(record : 'Record) = info.RProperties.[2].GetValue(record, null) :?> 'Field3
        member __.Project4(record : 'Record) = info.RProperties.[3].GetValue(record, null) :?> 'Field4
        member s.Accept v = v.Visit<'Record, 'Field1, 'Field2, 'Field3, 'Field4> s

///////////// Record arity 5

type IShapeFSharpRecord5 =
    inherit IShapeFSharpRecord
    abstract Accept<'R> : IFSharpRecord5Visitor<'R> -> 'R

and IFSharpRecord5Visitor<'R> =
    abstract Visit<'Record, 'Field1, 'Field2, 'Field3, 'Field4, 'Field5> : IShapeFSharpRecord<'Record, 'Field1, 'Field2, 'Field3, 'Field4, 'Field5> -> 'R

and IShapeFSharpRecord<'Record, 'Field1, 'Field2, 'Field3, 'Field4, 'Field5> =
    inherit IShapeFSharpRecord5
    abstract Construct : 'Field1 * 'Field2 * 'Field3 * 'Field4 * 'Field5 -> 'Record
    abstract Project1 : 'Record -> 'Field1
    abstract Project2 : 'Record -> 'Field2
    abstract Project3 : 'Record -> 'Field3
    abstract Project4 : 'Record -> 'Field4
    abstract Project5 : 'Record -> 'Field5

type private ShapeFSharpRecord<'Record, 'Field1, 'Field2, 'Field3, 'Field4, 'Field5> (info : RecordInfo) =
    inherit TypeShape<'Record>()
    interface IShapeFSharpRecord<'Record, 'Field1, 'Field2, 'Field3, 'Field4, 'Field5> with
        member __.IsFSharpRef = false
        member __.ConstructorInfo = info.RCtor
        member __.Properties = info.RProperties |> Array.toList
        member __.Construct(f1 : 'Field1, f2 : 'Field2, f3 : 'Field3, f4 : 'Field4, f5 : 'Field5) = info.RCtor.Invoke [|f1; f2; f3; f4; f5|] :?> 'Record
        member __.Project1(record : 'Record) = info.RProperties.[0].GetValue(record, null) :?> 'Field1
        member __.Project2(record : 'Record) = info.RProperties.[1].GetValue(record, null) :?> 'Field2
        member __.Project3(record : 'Record) = info.RProperties.[2].GetValue(record, null) :?> 'Field3
        member __.Project4(record : 'Record) = info.RProperties.[3].GetValue(record, null) :?> 'Field4
        member __.Project5(record : 'Record) = info.RProperties.[4].GetValue(record, null) :?> 'Field5
        member s.Accept v = v.Visit<'Record, 'Field1, 'Field2, 'Field3, 'Field4, 'Field5> s

///////////// Record arity 6

type IShapeFSharpRecord6 =
    inherit IShapeFSharpRecord
    abstract Accept<'R> : IFSharpRecord6Visitor<'R> -> 'R

and IShapeFSharpRecord<'Record, 'Field1, 'Field2, 'Field3, 'Field4, 'Field5, 'Field6> =
    inherit IShapeFSharpRecord6
    abstract Construct : 'Field1 * 'Field2 * 'Field3 * 'Field4 * 'Field5 * 'Field6 -> 'Record
    abstract Project1 : 'Record -> 'Field1
    abstract Project2 : 'Record -> 'Field2
    abstract Project3 : 'Record -> 'Field3
    abstract Project4 : 'Record -> 'Field4
    abstract Project5 : 'Record -> 'Field5
    abstract Project6 : 'Record -> 'Field6

and IFSharpRecord6Visitor<'R> =
    abstract Visit<'Record, 'Field1, 'Field2, 'Field3, 'Field4, 'Field5, 'Field6> : IShapeFSharpRecord<'Record, 'Field1, 'Field2, 'Field3, 'Field4, 'Field5, 'Field6> -> 'R

and private ShapeFSharpRecord<'Record, 'Field1, 'Field2, 'Field3, 'Field4, 'Field5, 'Field6> (info : RecordInfo) =
    inherit TypeShape<'Record>()
    interface IShapeFSharpRecord<'Record, 'Field1, 'Field2, 'Field3, 'Field4, 'Field5, 'Field6> with
        member __.IsFSharpRef = false
        member __.ConstructorInfo = info.RCtor
        member __.Properties = info.RProperties |> Array.toList
        member __.Construct(f1 : 'Field1, f2 : 'Field2, f3 : 'Field3, f4 : 'Field4, f5 : 'Field5, f6 : 'Field6) = info.RCtor.Invoke [|f1; f2; f3; f4; f5 ; f6|] :?> 'Record
        member __.Project1(record : 'Record) = info.RProperties.[0].GetValue(record, null) :?> 'Field1
        member __.Project2(record : 'Record) = info.RProperties.[1].GetValue(record, null) :?> 'Field2
        member __.Project3(record : 'Record) = info.RProperties.[2].GetValue(record, null) :?> 'Field3
        member __.Project4(record : 'Record) = info.RProperties.[3].GetValue(record, null) :?> 'Field4
        member __.Project5(record : 'Record) = info.RProperties.[4].GetValue(record, null) :?> 'Field5
        member __.Project6(record : 'Record) = info.RProperties.[5].GetValue(record, null) :?> 'Field6
        member s.Accept v = v.Visit<'Record, 'Field1, 'Field2, 'Field3, 'Field4, 'Field5, 'Field6> s

///////////// Record arity 7

type IShapeFSharpRecord7 =
    inherit IShapeFSharpRecord
    abstract Accept<'R> : IFSharpRecord7Visitor<'R> -> 'R

and IFSharpRecord7Visitor<'R> =
    abstract Visit<'Record, 'Field1, 'Field2, 'Field3, 'Field4, 'Field5, 'Field6, 'Field67> : IShapeFSharpRecord<'Record, 'Field1, 'Field2, 'Field3, 'Field4, 'Field5, 'Field6, 'Field67> -> 'R

and IShapeFSharpRecord<'Record, 'Field1, 'Field2, 'Field3, 'Field4, 'Field5, 'Field6, 'Field7> =
    inherit IShapeFSharpRecord7
    abstract Construct : 'Field1 * 'Field2 * 'Field3 * 'Field4 * 'Field5 * 'Field6 * 'Field7 -> 'Record
    abstract Project1 : 'Record -> 'Field1
    abstract Project2 : 'Record -> 'Field2
    abstract Project3 : 'Record -> 'Field3
    abstract Project4 : 'Record -> 'Field4
    abstract Project5 : 'Record -> 'Field5
    abstract Project6 : 'Record -> 'Field6
    abstract Project7 : 'Record -> 'Field7

type private ShapeFSharpRecord<'Record, 'Field1, 'Field2, 'Field3, 'Field4, 'Field5, 'Field6, 'Field7> private (info : RecordInfo) =
    inherit TypeShape<'Record>()
    interface IShapeFSharpRecord<'Record, 'Field1, 'Field2, 'Field3, 'Field4, 'Field5, 'Field6, 'Field7> with
        member __.IsFSharpRef = false
        member __.ConstructorInfo = info.RCtor
        member __.Properties = info.RProperties |> Array.toList
        member __.Construct(f1 : 'Field1, f2 : 'Field2, f3 : 'Field3, f4 : 'Field4, f5 : 'Field5, f6 : 'Field6, f7 : 'Field7) = 
            info.RCtor.Invoke [|f1; f2; f3; f4; f5; f6; f7|] :?> 'Record

        member __.Project1(record : 'Record) = info.RProperties.[0].GetValue(record, null) :?> 'Field1
        member __.Project2(record : 'Record) = info.RProperties.[1].GetValue(record, null) :?> 'Field2
        member __.Project3(record : 'Record) = info.RProperties.[2].GetValue(record, null) :?> 'Field3
        member __.Project4(record : 'Record) = info.RProperties.[3].GetValue(record, null) :?> 'Field4
        member __.Project5(record : 'Record) = info.RProperties.[4].GetValue(record, null) :?> 'Field5
        member __.Project6(record : 'Record) = info.RProperties.[5].GetValue(record, null) :?> 'Field6
        member __.Project7(record : 'Record) = info.RProperties.[6].GetValue(record, null) :?> 'Field7
        member s.Accept v = v.Visit<'Record, 'Field1, 'Field2, 'Field3, 'Field4, 'Field5, 'Field6, 'Field7> s

///////////// F# ref

type IShapeFSharpRef =
    abstract Accept : IFSharpRefVisitor<'R> -> 'R

and IFSharpRefVisitor<'R> =
    abstract Visit<'T> : unit -> 'R

type private ShapeFSharpRef<'T> (info : RecordInfo) =
    inherit TypeShape<'T ref>()
    interface IShapeFSharpRecord<'T ref, 'T> with
        member x.IsFSharpRef = true
        member x.Construct t = ref t
        member x.Project1 tr = tr.Value
        member x.ConstructorInfo = info.RCtor
        member x.Properties = info.RProperties |> List.ofArray
        member x.Accept v = v.Visit<'T ref, 'T> x

    interface IShapeFSharpRef with
        member __.Accept v = v.Visit<'T> ()

/////////////////////////////////
///////////// Section: F# Unions

type private CaseInfo = 
    {
        UnionCaseInfo : UnionCaseInfo
        PayloadType : Type
        UCtor : obj -> obj
        UProj : obj -> obj
    }

type private UnionInfo =
    {
        IsChoiceType : bool
        TagReader : obj -> int
        Cases : CaseInfo []
    }
with
    member __.UnionCaseInfo =
        __.Cases |> Seq.map (fun u -> u.UnionCaseInfo) |> Seq.toList

type IShapeFSharpUnion =
    abstract IsFSharpOption : bool
    abstract IsFSharpChoice : bool
    abstract IsFSharpList : bool
    abstract GetTagUntyped : obj -> int
    abstract UnionCaseInfo : UnionCaseInfo list

///////////// 1-case union

type IFSharpUnion1Visitor<'R> =
    abstract Visit<'Union, 'Case1> : IShapeFSharpUnion<'Union, 'Case1> -> 'R

and IShapeFSharpUnion1 =
    inherit IShapeFSharpUnion
    abstract Accept : IFSharpUnion1Visitor<'R> -> 'R

and IShapeFSharpUnion<'Union, 'Case1> =
    inherit IShapeFSharpUnion1
    abstract GetTag : 'Union -> int
    abstract Project : 'Union -> 'Case1
    abstract Construct1 : 'Case1 -> 'Union

type private ShapeFSharpUnion<'Union, 'Case1> (info : UnionInfo) =
    inherit TypeShape<'Union>()
    interface IShapeFSharpUnion<'Union, 'Case1> with
        member __.IsFSharpOption = false
        member __.IsFSharpList = false
        member __.IsFSharpChoice = info.IsChoiceType
        member __.UnionCaseInfo = info.UnionCaseInfo
        member __.GetTag (u : 'Union) = info.TagReader (u :> _)
        member __.GetTagUntyped o = info.TagReader o

        member __.Project (u : 'Union) =
            let { UProj = proj } = info.Cases.[0]
            proj u :?> 'Case1

        member __.Construct1 (c : 'Case1) = 
            let { UCtor = ctor } = info.Cases.[0]
            ctor c :?> 'Union

        member self.Accept v = v.Visit<'Union, 'Case1> self

///////////// 2-case union

type IFSharpUnion2Visitor<'R> =
    abstract Visit<'Union, 'Case1, 'Case2> : IShapeFSharpUnion<'Union, 'Case1, 'Case2> -> 'R

and IShapeFSharpUnion2 =
    inherit IShapeFSharpUnion
    abstract Accept : IFSharpUnion2Visitor<'R> -> 'R

and IShapeFSharpUnion<'Union, 'Case1, 'Case2> =
    inherit IShapeFSharpUnion2
    abstract GetTag : 'Union -> int
    abstract Project : 'Union -> Choice<'Case1, 'Case2>
    abstract Construct1 : 'Case1 -> 'Union
    abstract Construct2 : 'Case2 -> 'Union

type private ShapeFSharpUnion<'Union, 'Case1, 'Case2> (info : UnionInfo) =
    inherit TypeShape<'Union>()
    interface IShapeFSharpUnion<'Union, 'Case1, 'Case2> with
        member __.IsFSharpOption = false
        member __.IsFSharpList = false
        member __.IsFSharpChoice = info.IsChoiceType
        member __.UnionCaseInfo = info.UnionCaseInfo
        member __.GetTag (u : 'Union) = info.TagReader (u :> _)
        member __.GetTagUntyped o = info.TagReader o

        member __.Project (u : 'Union) =
            if info.IsChoiceType then u :> obj :?> _ else 
            let tag = info.TagReader (u :> _)
            let { UProj = proj } = info.Cases.[tag]
            let value = proj u
            match tag with
            | 0 -> Choice1Of2(value :?> 'Case1)
            | _ -> Choice2Of2(value :?> 'Case2)

        member __.Construct1 (c : 'Case1) =
            let { UCtor = ctor } = info.Cases.[0]
            ctor c :?> 'Union

        member __.Construct2 (c : 'Case2) =
            let { UCtor = ctor } = info.Cases.[1]
            ctor c :?> 'Union

        member self.Accept(v : IFSharpUnion2Visitor<'R>) = v.Visit<'Union, 'Case1, 'Case2> self

///////////// 3-case union

type IFSharpUnion3Visitor<'R> =
    abstract Visit<'Union, 'Case1, 'Case2, 'Case3> : IShapeFSharpUnion<'Union, 'Case1, 'Case2, 'Case3> -> 'R

and IShapeFSharpUnion3 =
    inherit IShapeFSharpUnion
    abstract Accept : IFSharpUnion3Visitor<'R> -> 'R

and IShapeFSharpUnion<'Union, 'Case1, 'Case2, 'Case3> =
    inherit IShapeFSharpUnion3
    abstract GetTag : 'Union -> int
    abstract Project : 'Union -> Choice<'Case1, 'Case2, 'Case3>
    abstract Construct1 : 'Case1 -> 'Union
    abstract Construct2 : 'Case2 -> 'Union
    abstract Construct3 : 'Case3 -> 'Union

type private ShapeFSharpUnion<'Union, 'Case1, 'Case2, 'Case3> (info : UnionInfo) =
    inherit TypeShape<'Union>()
    interface IShapeFSharpUnion<'Union, 'Case1, 'Case2, 'Case3> with
        member __.UnionCaseInfo = info.UnionCaseInfo
        member __.IsFSharpOption = false
        member __.IsFSharpList = false
        member __.IsFSharpChoice = info.IsChoiceType
        member __.GetTag (u : 'Union) = info.TagReader (u :> _)
        member __.GetTagUntyped o = info.TagReader o

        member __.Project (u : 'Union) =
            if info.IsChoiceType then u :> obj :?> _ else 
            let tag = info.TagReader (u :> _)
            let { UProj = proj } = info.Cases.[tag]
            let value = proj u
            match tag with
            | 0 -> Choice1Of3(value :?> 'Case1)
            | 1 -> Choice2Of3(value :?> 'Case2)
            | _ -> Choice3Of3(value :?> 'Case3)

        member __.Construct1 (c : 'Case1) =
            let { UCtor = ctor } = info.Cases.[0]
            ctor c :?> 'Union

        member __.Construct2 (c : 'Case2) =
            let { UCtor = ctor } = info.Cases.[1]
            ctor c :?> 'Union

        member __.Construct3 (c : 'Case3) =
            let { UCtor = ctor } = info.Cases.[2]
            ctor c :?> 'Union

        member self.Accept(v : IFSharpUnion3Visitor<'R>) = 
            v.Visit<'Union, 'Case1, 'Case2, 'Case3> self

///////////// 4-case union

type IFSharpUnion4Visitor<'R> =
    abstract Visit<'Union, 'Case1, 'Case2, 'Case3, 'Case4> : IShapeFSharpUnion<'Union, 'Case1, 'Case2, 'Case3, 'Case4> -> 'R

and IShapeFSharpUnion4 =
    inherit IShapeFSharpUnion
    abstract Accept : IFSharpUnion4Visitor<'R> -> 'R

and IShapeFSharpUnion<'Union, 'Case1, 'Case2, 'Case3, 'Case4> =
    inherit IShapeFSharpUnion4
    abstract GetTag : 'Union -> int
    abstract Project : 'Union -> Choice<'Case1, 'Case2, 'Case3, 'Case4>
    abstract Construct1 : 'Case1 -> 'Union
    abstract Construct2 : 'Case2 -> 'Union
    abstract Construct3 : 'Case3 -> 'Union
    abstract Construct4 : 'Case4 -> 'Union

type private ShapeFSharpUnion<'Union, 'Case1, 'Case2, 'Case3, 'Case4> (info : UnionInfo) =
    inherit TypeShape<'Union>()
    interface IShapeFSharpUnion<'Union, 'Case1, 'Case2, 'Case3, 'Case4> with
        member __.IsFSharpOption = false
        member __.IsFSharpList = false
        member __.IsFSharpChoice = info.IsChoiceType
        member __.UnionCaseInfo = info.UnionCaseInfo
        member __.GetTag (u : 'Union) = info.TagReader (u :> _)
        member __.GetTagUntyped o = info.TagReader o

        member __.Project (u : 'Union) =
            if info.IsChoiceType then u :> obj :?> _ else 
            let tag = info.TagReader (u :> _)
            let { UProj = proj } = info.Cases.[tag]
            let value = proj u
            match tag with
            | 0 -> Choice1Of4(value :?> 'Case1)
            | 1 -> Choice2Of4(value :?> 'Case2)
            | 2 -> Choice3Of4(value :?> 'Case3)
            | _ -> Choice4Of4(value :?> 'Case4)

        member __.Construct1 (c : 'Case1) =
            let { UCtor = ctor } = info.Cases.[0]
            ctor c :?> 'Union

        member __.Construct2 (c : 'Case2) =
            let { UCtor = ctor } = info.Cases.[1]
            ctor c :?> 'Union

        member __.Construct3 (c : 'Case3) =
            let { UCtor = ctor } = info.Cases.[2]
            ctor c :?> 'Union

        member __.Construct4 (c : 'Case4) =
            let { UCtor = ctor } = info.Cases.[3]
            ctor c :?> 'Union

        member self.Accept(v : IFSharpUnion4Visitor<'R>) = 
            v.Visit<'Union, 'Case1, 'Case2, 'Case3, 'Case4> self

///////////// 5-case union

type IFSharpUnion5Visitor<'R> =
    abstract Visit<'Union, 'Case1, 'Case2, 'Case3, 'Case4, 'Case5> : IShapeFSharpUnion<'Union, 'Case1, 'Case2, 'Case3, 'Case4, 'Case5> -> 'R

and IShapeFSharpUnion5 =
    inherit IShapeFSharpUnion
    abstract Accept : IFSharpUnion5Visitor<'R> -> 'R

and IShapeFSharpUnion<'Union, 'Case1, 'Case2, 'Case3, 'Case4, 'Case5> =
    inherit IShapeFSharpUnion5
    abstract GetTag : 'Union -> int
    abstract Project : 'Union -> Choice<'Case1, 'Case2, 'Case3, 'Case4, 'Case5>
    abstract Construct1 : 'Case1 -> 'Union
    abstract Construct2 : 'Case2 -> 'Union
    abstract Construct3 : 'Case3 -> 'Union
    abstract Construct4 : 'Case4 -> 'Union
    abstract Construct5 : 'Case5 -> 'Union

type private ShapeFSharpUnion<'Union, 'Case1, 'Case2, 'Case3, 'Case4, 'Case5> (info : UnionInfo) =
    inherit TypeShape<'Union>()
    interface IShapeFSharpUnion<'Union, 'Case1, 'Case2, 'Case3, 'Case4, 'Case5> with
        member __.IsFSharpOption = false
        member __.IsFSharpList = false
        member __.IsFSharpChoice = info.IsChoiceType
        member __.UnionCaseInfo = info.UnionCaseInfo
        member __.GetTag (u : 'Union) = info.TagReader (u :> _)
        member __.GetTagUntyped o = info.TagReader o

        member __.Project (u : 'Union) =
            if info.IsChoiceType then u :> obj :?> _ else 
            let tag = info.TagReader (u :> _)
            let { UProj = proj } = info.Cases.[tag]
            let value = proj u
            match tag with
            | 0 -> Choice1Of5(value :?> 'Case1)
            | 1 -> Choice2Of5(value :?> 'Case2)
            | 2 -> Choice3Of5(value :?> 'Case3)
            | 3 -> Choice4Of5(value :?> 'Case4)
            | _ -> Choice5Of5(value :?> 'Case5)

        member __.Construct1 (c : 'Case1) =
            let { UCtor = ctor } = info.Cases.[0]
            ctor c :?> 'Union

        member __.Construct2 (c : 'Case2) =
            let { UCtor = ctor } = info.Cases.[1]
            ctor c :?> 'Union

        member __.Construct3 (c : 'Case3) =
            let { UCtor = ctor } = info.Cases.[2]
            ctor c :?> 'Union

        member __.Construct4 (c : 'Case4) =
            let { UCtor = ctor } = info.Cases.[3]
            ctor c :?> 'Union

        member __.Construct5 (c : 'Case5) =
            let { UCtor = ctor } = info.Cases.[4]
            ctor c :?> 'Union

        member self.Accept(v : IFSharpUnion5Visitor<'R>) = 
            v.Visit<'Union, 'Case1, 'Case2, 'Case3, 'Case4, 'Case5> self

///////////// 6-case union

type IFSharpUnion6Visitor<'R> =
    abstract Visit<'Union, 'Case1, 'Case2, 'Case3, 'Case4, 'Case5, 'Case6> : IShapeFSharpUnion<'Union, 'Case1, 'Case2, 'Case3, 'Case4, 'Case5, 'Case6> -> 'R

and IShapeFSharpUnion6 =
    inherit IShapeFSharpUnion
    abstract Accept : IFSharpUnion6Visitor<'R> -> 'R

and IShapeFSharpUnion<'Union, 'Case1, 'Case2, 'Case3, 'Case4, 'Case5, 'Case6> =
    inherit IShapeFSharpUnion6
    abstract GetTag : 'Union -> int
    abstract Project : 'Union -> Choice<'Case1, 'Case2, 'Case3, 'Case4, 'Case5, 'Case6>
    abstract Construct1 : 'Case1 -> 'Union
    abstract Construct2 : 'Case2 -> 'Union
    abstract Construct3 : 'Case3 -> 'Union
    abstract Construct4 : 'Case4 -> 'Union
    abstract Construct5 : 'Case5 -> 'Union
    abstract Construct6 : 'Case6 -> 'Union

type private ShapeFSharpUnion<'Union, 'Case1, 'Case2, 'Case3, 'Case4, 'Case5, 'Case6> (info : UnionInfo) =
    inherit TypeShape<'Union>()
    interface IShapeFSharpUnion<'Union, 'Case1, 'Case2, 'Case3, 'Case4, 'Case5, 'Case6> with
        member __.IsFSharpOption = false
        member __.IsFSharpList = false
        member __.IsFSharpChoice = info.IsChoiceType
        member __.UnionCaseInfo = info.UnionCaseInfo
        member __.GetTag (u : 'Union) = info.TagReader (u :> _)
        member __.GetTagUntyped o = info.TagReader o

        member __.Project (u : 'Union) =
            if info.IsChoiceType then u :> obj :?> _ else 
            let tag = info.TagReader (u :> _)
            let { UProj = proj } = info.Cases.[tag]
            let value = proj u
            match tag with
            | 0 -> Choice1Of6(value :?> 'Case1)
            | 1 -> Choice2Of6(value :?> 'Case2)
            | 2 -> Choice3Of6(value :?> 'Case3)
            | 3 -> Choice4Of6(value :?> 'Case4)
            | 4 -> Choice5Of6(value :?> 'Case5)
            | _ -> Choice6Of6(value :?> 'Case6)

        member __.Construct1 (c : 'Case1) =
            let { UCtor = ctor } = info.Cases.[0]
            ctor c :?> 'Union

        member __.Construct2 (c : 'Case2) =
            let { UCtor = ctor } = info.Cases.[1]
            ctor c :?> 'Union

        member __.Construct3 (c : 'Case3) =
            let { UCtor = ctor } = info.Cases.[2]
            ctor c :?> 'Union

        member __.Construct4 (c : 'Case4) =
            let { UCtor = ctor } = info.Cases.[3]
            ctor c :?> 'Union

        member __.Construct5 (c : 'Case5) =
            let { UCtor = ctor } = info.Cases.[4]
            ctor c :?> 'Union

        member __.Construct6 (c : 'Case6) =
            let { UCtor = ctor } = info.Cases.[5]
            ctor c :?> 'Union

        member self.Accept(v : IFSharpUnion6Visitor<'R>) = 
            v.Visit<'Union, 'Case1, 'Case2, 'Case3, 'Case4, 'Case5, 'Case6> self

///////////// 7-case union

type IFSharpUnion7Visitor<'R> =
    abstract Visit<'Union, 'Case1, 'Case2, 'Case3, 'Case4, 'Case5, 'Case6, 'Case7> : ShapeFSharpUnion<'Union, 'Case1, 'Case2, 'Case3, 'Case4, 'Case5, 'Case6, 'Case7> -> 'R

and IShapeFSharpUnion7 =
    inherit IShapeFSharpUnion
    abstract Accept : IFSharpUnion7Visitor<'R> -> 'R

and IShapeFSharpUnion<'Union, 'Case1, 'Case2, 'Case3, 'Case4, 'Case5, 'Case6, 'Case7> =
    inherit IShapeFSharpUnion7
    abstract GetTag : 'Union -> int
    abstract Project : 'Union -> Choice<'Case1, 'Case2, 'Case3, 'Case4, 'Case5, 'Case6, 'Case7>
    abstract Construct1 : 'Case1 -> 'Union
    abstract Construct2 : 'Case2 -> 'Union
    abstract Construct3 : 'Case3 -> 'Union
    abstract Construct4 : 'Case4 -> 'Union
    abstract Construct5 : 'Case5 -> 'Union
    abstract Construct6 : 'Case6 -> 'Union
    abstract Construct7 : 'Case7 -> 'Union


and ShapeFSharpUnion<'Union, 'Case1, 'Case2, 'Case3, 'Case4, 'Case5, 'Case6, 'Case7> private (info : UnionInfo) =
    inherit TypeShape<'Union>()
    interface IShapeFSharpUnion<'Union, 'Case1, 'Case2, 'Case3, 'Case4, 'Case5, 'Case6, 'Case7> with
        member __.IsFSharpOption = false
        member __.IsFSharpList = false
        member __.IsFSharpChoice = info.IsChoiceType
        member __.UnionCaseInfo = info.UnionCaseInfo
        member __.GetTag (u : 'Union) = info.TagReader (u :> _)
        member __.GetTagUntyped o = info.TagReader o

        member __.Project (u : 'Union) =
            if info.IsChoiceType then u :> obj :?> _ else 
            let tag = info.TagReader (u :> _)
            let { UProj = proj } = info.Cases.[tag]
            let value = proj u
            match tag with
            | 0 -> Choice1Of7(value :?> 'Case1)
            | 1 -> Choice2Of7(value :?> 'Case2)
            | 2 -> Choice3Of7(value :?> 'Case3)
            | 3 -> Choice4Of7(value :?> 'Case4)
            | 4 -> Choice5Of7(value :?> 'Case5)
            | 5 -> Choice6Of7(value :?> 'Case6)
            | _ -> Choice7Of7(value :?> 'Case7)

        member __.Construct1 (c : 'Case1) =
            let { UCtor = ctor } = info.Cases.[0]
            ctor c :?> 'Union

        member __.Construct2 (c : 'Case2) =
            let { UCtor = ctor } = info.Cases.[1]
            ctor c :?> 'Union

        member __.Construct3 (c : 'Case3) =
            let { UCtor = ctor } = info.Cases.[2]
            ctor c :?> 'Union

        member __.Construct4 (c : 'Case4) =
            let { UCtor = ctor } = info.Cases.[3]
            ctor c :?> 'Union

        member __.Construct5 (c : 'Case5) =
            let { UCtor = ctor } = info.Cases.[4]
            ctor c :?> 'Union

        member __.Construct6 (c : 'Case6) =
            let { UCtor = ctor } = info.Cases.[5]
            ctor c :?> 'Union

        member __.Construct7 (c : 'Case7) =
            let { UCtor = ctor } = info.Cases.[6]
            ctor c :?> 'Union

        member self.Accept(v : IFSharpUnion7Visitor<'R>) = 
            v.Visit<'Union, 'Case1, 'Case2, 'Case3, 'Case4, 'Case5, 'Case6, 'Case7> self

///////////// F# option

type IFSharpOptionVisitor<'R> =
    abstract Visit<'T> : unit -> 'R

type IShapeFSharpOption =
    abstract Accept : IFSharpOptionVisitor<'R> -> 'R

type ShapeFSharpOption<'T> () =
    inherit TypeShape<'T option> ()
    let ucis = FSharpType.GetUnionCases typeof<'T option> |> Array.toList
    interface IShapeFSharpOption with
        member __.Accept v = v.Visit<'T> ()

    interface IShapeFSharpUnion<'T option, unit, 'T> with
        member __.GetTag t = match t with None -> 0 | _ -> 1
        member __.GetTagUntyped o = match o :?> 'T option with None -> 0 | _ -> 1
        member __.IsFSharpChoice = false
        member __.IsFSharpList = false
        member __.IsFSharpOption = true
        member __.UnionCaseInfo = ucis
        member __.Project t = match t with None -> Choice1Of2 () | Some t -> Choice2Of2 t
        member __.Construct1 (()) = None
        member __.Construct2 t = Some t
        member self.Accept v = v.Visit<'T option, unit, 'T> self

///////////// F# List

type IFSharpListVisitor<'R> =
    abstract Visit<'T> : unit -> 'R

type IShapeFSharpList =
    abstract Accept : IFSharpListVisitor<'R> -> 'R

type ShapeFSharpList<'T> () =
    inherit TypeShape<'T list> ()
    let ucis = FSharpType.GetUnionCases typeof<'T list> |> Array.toList
    interface IShapeFSharpList with
        member __.Accept v = v.Visit<'T> ()
    interface IShapeEnumerable with
        member __.Accept v = v.Visit<'T> ()
    interface IShapeCollection with
        member __.Accept v = v.Visit<'T> ()
    interface IShapeFSharpUnion<'T list, unit, 'T * 'T list> with
        member __.GetTag a = match a with [] -> 0 | _ -> 1
        member __.GetTagUntyped o = match o :?> 'T list with [] -> 0 | _ -> 1
        member __.IsFSharpChoice = false
        member __.IsFSharpList = true
        member __.IsFSharpOption = false
        member __.UnionCaseInfo = ucis
        member __.Project ts = match ts with [] -> Choice1Of2() | t :: tl -> Choice2Of2(t,tl)
        member __.Construct1 (()) = []
        member __.Construct2 ((t, tl)) = t :: tl
        member self.Accept v = v.Visit<'T list, unit, 'T * 'T list> self

////////////////////////////////////////////
///////////// Section: TypeShape resolution

exception UnsupportedShape of Type:Type
 with
    override __.Message = sprintf "Unsupported TypeShape '%O'" __.Type

module private TypeShapeImpl =

    open System.Reflection
    open Microsoft.FSharp.Reflection

    let allMembers =
        BindingFlags.NonPublic ||| BindingFlags.Public |||
            BindingFlags.Instance ||| BindingFlags.Static |||
                BindingFlags.FlattenHierarchy

    // typedefof does not work properly with 'enum' constraints
    let getGenericEnumType () = 
        typeof<ShapeEnum<BindingFlags,int>>.GetGenericTypeDefinition()

    let activateArgs (gt : Type) (tp : Type []) (args : obj[]) =
        let ti = gt.MakeGenericType tp
        let ctypes = args |> Array.map (fun o -> o.GetType())
        let ctor = ti.GetConstructor(allMembers, null, CallingConventions.Standard, ctypes, [||])
        ctor.Invoke args :?> TypeShape

    let activate (gt : Type) (tp : Type []) = activateArgs gt tp [||]
    let activate1 (gt : Type) (tp : Type) = activate gt [|tp|]
    let activate2 (gt : Type) (p1 : Type) (p2 : Type) = activate gt [|p1 ; p2|]

    let canon = Type.GetType("System.__Canon")

    /// correctly resolves if type is assignable to interface
    let rec private isAssignableFrom (interfaceTy : Type) (ty : Type) =
        let proj (t : Type) = t.Assembly, t.Namespace, t.Name, t.MetadataToken
        if interfaceTy = ty then true
        elif ty.GetInterfaces() |> Array.exists(fun if0 -> proj if0 = proj interfaceTy) then true
        else
            match ty.BaseType with
            | null -> false
            | bt -> isAssignableFrom interfaceTy bt
        
    /// use reflection to bootstrap a shape instance
    let resolveTypeShape (t : Type) : TypeShape =
        if t.IsGenericTypeDefinition then raise <| UnsupportedShape t
        elif t.IsGenericParameter then raise <| UnsupportedShape t
        elif t = canon then raise <| UnsupportedShape t
        elif t.IsEnum then 
            activate2 (getGenericEnumType()) t <| Enum.GetUnderlyingType t

        elif t.IsArray then
            let et = t.GetElementType()
            match t.GetArrayRank() with
            | 1 -> activate1 typedefof<ShapeArray<_>> et
            | 2 -> activate1 typedefof<ShapeArray2D<_>> et
            | 3 -> activate1 typedefof<ShapeArray3D<_>> et
            | 4 -> activate1 typedefof<ShapeArray4D<_>> et
            | _ -> raise <| UnsupportedShape t

        elif FSharpType.IsTuple t then
            let gas = t.GetGenericArguments()
            match gas.Length with
            | 1 -> activate typedefof<ShapeTuple<_>> gas
            | 2 -> activate typedefof<ShapeTuple<_,_>> gas
            | 3 -> activate typedefof<ShapeTuple<_,_,_>> gas
            | 4 -> activate typedefof<ShapeTuple<_,_,_,_>> gas
            | 5 -> activate typedefof<ShapeTuple<_,_,_,_,_>> gas
            | 6 -> activate typedefof<ShapeTuple<_,_,_,_,_,_>> gas
            | 7 -> activate typedefof<ShapeTuple<_,_,_,_,_,_,_>> gas
            | 8 -> activate typedefof<ShapeTuple<_,_,_,_,_,_,_,_>> gas
            | _ -> raise <| UnsupportedShape t

        elif isAssignableFrom typeof<exn> t then
            activate1 typedefof<ShapeException<_>> t

        elif FSharpType.IsFunction t then
            let d,c = FSharpType.GetFunctionElements t
            activate2 typedefof<ShapeFSharpFunc<_,_>> d c


        elif FSharpType.IsRecord(t, allMembers) then
            let genTy = 
                if t.IsGenericType then Some(t.GetGenericTypeDefinition())
                else None


            let ctor = FSharpValue.PreComputeRecordConstructorInfo(t, allMembers)
            let properties = FSharpType.GetRecordFields(t, allMembers)
            let info = { RCtor = ctor ; RProperties = properties }
            if genTy = Some typedefof<_ ref> then 
                activateArgs typedefof<ShapeFSharpRef<_>> (t.GetGenericArguments()) [|info|]
            else

            match properties with
            | [|p1|] -> activateArgs typedefof<ShapeFSharpRecord<_,_>> [|t;p1.PropertyType|] [|info|]
            | [|p1;p2|] -> activateArgs typedefof<ShapeFSharpRecord<_,_,_>> [|t;p1.PropertyType;p2.PropertyType|] [|info|]
            | [|p1;p2;p3|] -> activateArgs typedefof<ShapeFSharpRecord<_,_,_,_>> [|t;p1.PropertyType;p2.PropertyType;p3.PropertyType|] [|info|]
            | [|p1;p2;p3;p4|] -> activateArgs typedefof<ShapeFSharpRecord<_,_,_,_,_>> [|t;p1.PropertyType;p2.PropertyType;p3.PropertyType;p4.PropertyType|] [|info|]
            | [|p1;p2;p3;p4;p5|] -> activateArgs typedefof<ShapeFSharpRecord<_,_,_,_,_,_>> [|t;p1.PropertyType;p2.PropertyType;p3.PropertyType;p4.PropertyType;p5.PropertyType|] [|info|]
            | [|p1;p2;p3;p4;p5;p6|] -> activateArgs typedefof<ShapeFSharpRecord<_,_,_,_,_,_,_>> [|t;p1.PropertyType;p2.PropertyType;p3.PropertyType;p4.PropertyType;p5.PropertyType;p6.PropertyType|] [|info|]
            | [|p1;p2;p3;p4;p5;p6;p7|] -> activateArgs typedefof<ShapeFSharpRecord<_,_,_,_,_,_,_,_>> [|t;p1.PropertyType;p2.PropertyType;p3.PropertyType;p4.PropertyType;p5.PropertyType;p6.PropertyType;p7.PropertyType|] [|info|]
            | _ -> activate1 typedefof<TypeShape<_>> t

        elif FSharpType.IsUnion(t, allMembers) then
            let genTy = 
                if t.IsGenericType then Some(t.GetGenericTypeDefinition())
                else None

            if genTy = Some(typedefof<_ list>) then
                activate typedefof<ShapeFSharpList<_>> (t.GetGenericArguments())
            elif genTy = Some(typedefof<_ option>) then
                activate typedefof<ShapeFSharpOption<_>> (t.GetGenericArguments())
            else
                let isChoice =
                    Option.isSome genTy &&
                    t.Name.StartsWith "FSharpChoice" && 
                    t.Namespace = "Microsoft.FSharp.Core" && 
                    t.Assembly = typeof<int option>.Assembly

                let mkCaseInfo (uci : UnionCaseInfo) =
                    let fields = uci.GetFields()
                    let uctor = FSharpValue.PreComputeUnionConstructor(uci, allMembers)
                    match fields with
                    | [||] -> 
                        { UnionCaseInfo = uci ; PayloadType = typeof<unit> ;
                            UCtor = (fun _ -> uctor [||]) ;
                            UProj = (fun _ -> () :> _) }

                    | [|field|] -> 
                        { UnionCaseInfo = uci ; PayloadType = field.PropertyType ;
                            UCtor = (fun v -> uctor [|v|]) ;
                            UProj = (fun u -> field.GetValue(u, null)) }
                    | _ ->
                        let tupleType = fields |> Array.map (fun f -> f.PropertyType) |> FSharpType.MakeTupleType
                        let uReader = FSharpValue.PreComputeUnionReader(uci, allMembers)
                        let tupleCtor = FSharpValue.PreComputeTupleConstructor tupleType
                        let tupleReader = FSharpValue.PreComputeTupleReader tupleType
                        { UnionCaseInfo = uci ; PayloadType = tupleType ;
                            UCtor = tupleReader >> uctor ;
                            UProj = uReader >> tupleCtor }

                let tagReader = FSharpValue.PreComputeUnionTagReader(t, allMembers)
                let ucis = FSharpType.GetUnionCases(t, allMembers)
                let caseInfo = ucis |> Array.map mkCaseInfo
                let unionInfo = { IsChoiceType = isChoice ; TagReader = tagReader ; Cases = caseInfo }
                match caseInfo with
                | [|c1|] -> activateArgs typedefof<ShapeFSharpUnion<_,_>> [|t ; c1.PayloadType|] [|unionInfo|]
                | [|c1;c2|] -> activateArgs typedefof<ShapeFSharpUnion<_,_,_>> [|t ; c1.PayloadType ; c2.PayloadType|] [|unionInfo|]
                | [|c1;c2;c3|] -> activateArgs typedefof<ShapeFSharpUnion<_,_,_,_>> [|t ; c1.PayloadType ; c2.PayloadType ; c3.PayloadType|] [|unionInfo|]
                | [|c1;c2;c3;c4|] -> activateArgs typedefof<ShapeFSharpUnion<_,_,_,_,_>> [|t ; c1.PayloadType ; c2.PayloadType ; c3.PayloadType ; c4.PayloadType|] [|unionInfo|]
                | [|c1;c2;c3;c4;c5|] -> activateArgs typedefof<ShapeFSharpUnion<_,_,_,_,_,_>> [|t ; c1.PayloadType ; c2.PayloadType ; c3.PayloadType ; c4.PayloadType ; c5.PayloadType|] [|unionInfo|]
                | [|c1;c2;c3;c4;c5;c6|] -> activateArgs typedefof<ShapeFSharpUnion<_,_,_,_,_,_,_>> [|t ; c1.PayloadType ; c2.PayloadType ; c3.PayloadType ; c4.PayloadType ; c5.PayloadType ; c6.PayloadType|] [|unionInfo|]
                | [|c1;c2;c3;c4;c5;c6;c7|] -> activateArgs typedefof<ShapeFSharpUnion<_,_,_,_,_,_,_,_>> [|t ; c1.PayloadType ; c2.PayloadType ; c3.PayloadType ; c4.PayloadType ; c5.PayloadType ; c6.PayloadType ; c7.PayloadType|] [|unionInfo|]
                | _ -> activate1 typedefof<TypeShape<_>> t

        elif t.IsGenericType then
            let gt = t.GetGenericTypeDefinition()
            let gas = t.GetGenericArguments()

            if gt = typedefof<System.Nullable<_>> then
                activate typedefof<ShapeNullable<_>> gas
            elif gt = typedefof<System.Collections.Generic.Dictionary<_,_>> then
                activate typedefof<ShapeDictionary<_,_>> gas
            elif gt = typedefof<System.Collections.Generic.HashSet<_>> then
                activate typedefof<ShapeHashSet<_>> gas
            elif gt = typedefof<System.Collections.Generic.List<_>> then
                activate typedefof<ShapeResizeArray<_>> gas
            elif gt = typedefof<Map<_,_>> then
                activate typedefof<ShapeFSharpMap<_,_>> gas
            elif gt = typedefof<Set<_>> then
                activate typedefof<ShapeFSharpSet<_>> gas
            elif gt = typedefof<KeyValuePair<_,_>> then
                activate typedefof<ShapeKeyValuePair<_,_>> gas
            elif isAssignableFrom typedefof<ICollection<_>> gt then
                activate typedefof<ShapeCollection<_>> gas
            elif isAssignableFrom typedefof<IEnumerable<_>> gt then
                activate typedefof<ShapeEnumerable<_>> gas
            else
                activate1 typedefof<TypeShape<_>> t
        else 
            activate1 typedefof<TypeShape<_>> t


    let dict = new System.Collections.Concurrent.ConcurrentDictionary<Type, TypeShape>()
    let resolveTypeShapeCached(t : Type) = dict.GetOrAdd(t, resolveTypeShape)

//////////////////////////////////
///////////// Section: Public API

[<AutoOpen>]
module TypeShapeModule =

    /// Computes the type shape for given type
    let getShape (t : Type) = TypeShapeImpl.resolveTypeShapeCached t
    /// Computes the type shape for given object
    let getObjectShape (obj : obj) = 
        if obj = null then raise <| new ArgumentNullException()
        TypeShapeImpl.resolveTypeShapeCached (obj.GetType())

    /// Computes the type shape for given type
    let shapeof<'T> = TypeShapeImpl.resolveTypeShapeCached typeof<'T> :?> TypeShape<'T>

    let private SomeU = Some()
    let inline private test0<'T> (t : TypeShape) =
        match t with
        | :? TypeShape<'T> -> SomeU
        | _ -> None

    let inline private test1<'If> (t : TypeShape) =
        match t :> obj with
        | :? 'If as f -> Some f
        | _ -> None

    let (|ShapeBool|_|) t = test0<bool> t
    let (|ShapeByte|_|) t = test0<byte> t
    let (|ShapeSByte|_|) t = test0<sbyte> t
    let (|ShapeInt16|_|) t = test0<int16> t
    let (|ShapeInt32|_|) t = test0<int32> t
    let (|ShapeInt64|_|) t = test0<int64> t
    let (|ShapeUInt16|_|) t = test0<uint16> t
    let (|ShapeUInt32|_|) t = test0<uint32> t
    let (|ShapeUInt64|_|) t = test0<uint64> t
    let (|ShapeSingle|_|) t = test0<single> t
    let (|ShapeDouble|_|) t = test0<double> t
    let (|ShapeChar|_|) t = test0<char> t

    let (|ShapeString|_|) t = test0<string> t
    let (|ShapeGuid|_|) t = test0<Guid> t
    let (|ShapeDecimal|_|) t = test0<decimal> t
    let (|ShapeTimeSpan|_|) t = test0<TimeSpan> t
    let (|ShapeDateTime|_|) t = test0<DateTime> t
    let (|ShapeDateTimeOffset|_|) t = test0<DateTimeOffset> t
    let (|ShapeDBNull|_|) t = test0<DBNull> t
    let (|ShapeUnit|_|) t = test0<unit> t
    let (|ShapeFSharpUnit|_|) t = test0<unit> t
    let (|ShapeByteArray|_|) t = test0<byte []> t
    
    let (|ShapeNullable|_|) t = test1<IShapeNullable> t
    let (|ShapeEnum|_|) t = test1<IShapeEnum> t
    let (|ShapeKeyValuePair|_|) t = test1<IShapeKeyValuePair> t
    let (|ShapeDictionary|_|) t = test1<IShapeDictionary> t
    let (|ShapeHashSet|_|) t = test1<IShapeHashSet> t
    let (|ShapeResizeArray|_|) t = test1<IShapeResizeArray> t

    let (|ShapeArray|_|) t = test1<IShapeArray> t
    let (|ShapeArray2D|_|) t = test1<IShapeArray2D> t
    let (|ShapeArray3D|_|) t = test1<IShapeArray3D> t
    let (|ShapeArray4D|_|) t = test1<IShapeArray4D> t

    let (|ShapeTuple|_|)  t = test1<IShapeTuple> t
    let (|ShapeTuple1|_|) t = test1<IShapeTuple1> t
    let (|ShapeTuple2|_|) t = test1<IShapeTuple2> t
    let (|ShapeTuple3|_|) t = test1<IShapeTuple3> t
    let (|ShapeTuple4|_|) t = test1<IShapeTuple4> t
    let (|ShapeTuple5|_|) t = test1<IShapeTuple5> t
    let (|ShapeTuple6|_|) t = test1<IShapeTuple6> t
    let (|ShapeTuple7|_|) t = test1<IShapeTuple7> t
    let (|ShapeTuple8|_|) t = test1<IShapeTuple8> t

    let (|ShapeFSharpList|_|) t = test1<IShapeFSharpList> t
    let (|ShapeFSharpOption|_|) t = test1<IShapeFSharpOption> t
    let (|ShapeFSharpRef|_|) t = test1<IShapeFSharpRef> t
    let (|ShapeFSharpSet|_|) t = test1<IShapeFSharpSet> t
    let (|ShapeFSharpMap|_|) t = test1<IShapeFSharpMap> t
    let (|ShapeFSharpFunc|_|) t = test1<IShapeFSharpFunc> t
    let (|ShapeException|_|) t = test1<IShapeException> t

    let (|ShapeFSharpUnion|_|) t = test1<IShapeFSharpUnion> t
    let (|ShapeFSharpUnion1|_|) t = test1<IShapeFSharpUnion1> t
    let (|ShapeFSharpUnion2|_|) t = test1<IShapeFSharpUnion2> t
    let (|ShapeFSharpUnion3|_|) t = test1<IShapeFSharpUnion3> t
    let (|ShapeFSharpUnion4|_|) t = test1<IShapeFSharpUnion4> t
    let (|ShapeFSharpUnion5|_|) t = test1<IShapeFSharpUnion5> t
    let (|ShapeFSharpUnion6|_|) t = test1<IShapeFSharpUnion6> t
    let (|ShapeFSharpUnion7|_|) t = test1<IShapeFSharpUnion7> t

    let (|ShapeFSharpRecord|_|) t = test1<IShapeFSharpRecord> t
    let (|ShapeFSharpRecord1|_|) t = test1<IShapeFSharpRecord1> t
    let (|ShapeFSharpRecord2|_|) t = test1<IShapeFSharpRecord2> t
    let (|ShapeFSharpRecord3|_|) t = test1<IShapeFSharpRecord3> t
    let (|ShapeFSharpRecord4|_|) t = test1<IShapeFSharpRecord4> t
    let (|ShapeFSharpRecord5|_|) t = test1<IShapeFSharpRecord5> t
    let (|ShapeFSharpRecord6|_|) t = test1<IShapeFSharpRecord6> t
    let (|ShapeFSharpRecord7|_|) t = test1<IShapeFSharpRecord7> t

    let (|ShapeCollection|_|) t = test1<IShapeCollection> t
    let (|ShapeEnumerable|_|) t = test1<IShapeEnumerable> t

    let (|ShapePrimitive|_|) (t : TypeShape) =
        match t with
        | :? TypeShape<bool>  
        | :? TypeShape<byte>  
        | :? TypeShape<sbyte> 
        | :? TypeShape<int16> 
        | :? TypeShape<int32> 
        | :? TypeShape<int64> 
        | :? TypeShape<uint16>
        | :? TypeShape<uint32>
        | :? TypeShape<uint64>
        | :? TypeShape<single>
        | :? TypeShape<double>
        | :? TypeShape<decimal> -> SomeU
        | _ -> None