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

    static member filter (elements: int list) (predicate: int -> bool) =
        let a =
            elements
            |> ImmutableSortedSet.ofList
            |> ImmutableSortedSet.filter predicate
            |> ImmutableSortedSet.toList
        let b = elements |> List.filter predicate |> List.distinct
        a = b
