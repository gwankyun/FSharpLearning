module ImmutableSortedSetCheck
open ImmutableSortedSet

type ImmutableSortedSetCheck() =
    static member list (elements: int list) =
        let elements = elements |> List.distinct
        elements = (elements |> ImmutableSortedSet.ofList |> ImmutableSortedSet.toList)

    static member array (elements: int []) =
        let elements = elements |> Array.distinct
        elements = (elements |> ImmutableSortedSet.ofArray |> ImmutableSortedSet.toArray)

    static member seq (elements: int list) =
        let elements = elements |> Seq.toList |> List.distinct
        elements = (elements |> ImmutableSortedSet.ofList |> ImmutableSortedSet.toList)

    static member map (elements: int list) (mapping: int -> int) =
        let a =
            elements
            |> ImmutableSortedSet.ofList
            |> ImmutableSortedSet.map mapping
            |> ImmutableSortedSet.toList
        let b = elements |> List.map mapping |> List.distinct
        a = b

    static member add (elements: int list) (n: int) =
        let elements = elements |> List.distinct
        let a = elements |> List.append [n] |> List.distinct
        let b =
            elements
            |> ImmutableSortedSet.ofList
            |> ImmutableSortedSet.add n
            |> ImmutableSortedSet.toList
        printfn "%A %A" a b
        a = b

    static member filter (elements: int list) (predicate: int -> bool) =
        let a =
            elements
            |> ImmutableSortedSet.ofList
            |> ImmutableSortedSet.filter predicate
            |> ImmutableSortedSet.toList
        let b = elements |> List.filter predicate |> List.distinct
        a = b

    static member equal (elements1: int list) (elements2: int list) =
        let set1 = elements1 |> ImmutableSortedSet.ofList 
        let set2 = elements2 |> ImmutableSortedSet.ofList 
        (elements1 = elements2) = (set1 = set2)
