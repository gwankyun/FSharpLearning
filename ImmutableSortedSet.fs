namespace ImmutableSortedSet
open System.Collections.Immutable
open System.Collections.Generic

module ImmutableSortedSet =
    let empty<'T when 'T : comparison> =
        ImmutableSortedSet.Create<'T>({
            new IComparer<'T> with
                member this.Compare(a: 'T, b: 'T) =
                    match compare a b with
                    | 0 -> 0
                    | _ -> 1
        })

    let emptyWith (comparer: IComparer<'T>) =
        ImmutableSortedSet.Create<'T>(comparer)

    let add (value: 'T) (set: ImmutableSortedSet<'T>) =
        set.Add(value)

    let singleton (value: 'T) =
        empty<'T> |> add value

    let singletonWith (comparer: IComparer<'T>) (value: 'T) =
        comparer |> emptyWith |> add value

    let count (set: ImmutableSortedSet<'T>) =
        set.Count

    let rev (set: ImmutableSortedSet<'T>) =
        set.Reverse()

    let remove (value: 'T) (set: ImmutableSortedSet<'T>) =
        set.Remove(value)

    let contains (value: 'T) (set: ImmutableSortedSet<'T>) =
        set.Contains(value)

    let ofList (elements: 'T list) =
        elements
        |> List.fold (fun s t -> s |> add t) empty<'T>

    let ofArray (array: 'T []) =
        array
        |> Array.fold (fun s t -> s |> add t) empty<'T>

    let ofSeq (elements: 'T seq) =
        elements
        |> Seq.fold (fun s t -> s |> add t) empty<'T>

    let toSeq (set: ImmutableSortedSet<'T>) : 'T seq =
        seq {
            for i in set.ToImmutableList() -> i
        }

    let toList (set: ImmutableSortedSet<'T>) : 'T list =
        set |> toSeq |> Seq.toList

    let toArray (set: ImmutableSortedSet<'T>) : 'T [] =
        set |> toSeq |> Seq.toArray

    let ofListWith (comparer: IComparer<'T>) (elements: 'T list) =
        elements
        |> List.fold (fun s t -> s |> add t) (emptyWith comparer)

    let ofArrayWith (comparer: IComparer<'T>) (array: 'T array) =
        array
        |> Array.fold (fun s t -> s |> add t) (emptyWith comparer)

    let ofSeqWith (comparer: IComparer<'T>) (elements: 'T seq) =
        elements
        |> Seq.fold (fun s t -> s |> add t) (emptyWith comparer)

    let map (mapping: 'T -> 'U) (set: ImmutableSortedSet<'T>) =
        set
        |> toList
        |> List.map mapping
        |> List.distinct
        |> ofList

    let filter (predicate: 'T -> bool) (set: ImmutableSortedSet<'T>) =
        set
        |> toList
        |> List.filter predicate
        |> List.distinct
        |> ofList
