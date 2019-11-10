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

    let isEmpty (set: ImmutableSortedSet<'T>) =
        set.IsEmpty

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

    let difference (set1: ImmutableSortedSet<'T>) (set2: ImmutableSortedSet<'T>) =
        set1
        |> filter (fun v -> set2 |> contains v)

    let exists (predicate: 'T -> bool) (set: ImmutableSortedSet<'T>) =
        set
        |> toList
        |> List.exists predicate

    let fold (folder: 'State -> 'T -> 'State) (state: 'State) (set: ImmutableSortedSet<'T>) =
        set
        |> toList
        |> List.fold folder state

    let foldBack (folder: 'T -> 'State -> 'State) (set: ImmutableSortedSet<'T>) (state: 'State) =
        state
        |> List.foldBack folder (
            set
            |> toList
        )

    let intersect (set1: ImmutableSortedSet<'T>) (set2: ImmutableSortedSet<'T>) =
        set1.Intersect(set2)

    let intersectMany (sets: seq<ImmutableSortedSet<'T>>) =
        let sets = sets |> Seq.toList
        let head = List.head sets
        let tail = List.tail sets
        tail
        |> List.fold (fun s t -> s |> intersect t) head

    let union (set1: ImmutableSortedSet<'T>) (set2: ImmutableSortedSet<'T>) =
        set1.Union(set2)

    let unionMany (sets: seq<ImmutableSortedSet<'T>>) =
        let sets = sets |> Seq.toList
        let head = List.head sets
        let tail = List.tail sets
        tail
        |> List.fold (fun s t -> s |> union t) head

    let maxElement (set: ImmutableSortedSet<'T>) =
        set.Max

    let minElement (set: ImmutableSortedSet<'T>) =
        set.Min

    let partition (predicate: 'T -> bool) (set: ImmutableSortedSet<'T>) =
        let a = set |> filter predicate
        let b = set |> filter (fun v -> not (predicate v))
        (a, b)

    let isProperSubset (set1: ImmutableSortedSet<'T>) (set2: ImmutableSortedSet<'T>) =
        set1.IsProperSubsetOf(set2)

    let isProperSuperset (set1: ImmutableSortedSet<'T>) (set2: ImmutableSortedSet<'T>) =
        set1.IsProperSupersetOf(set2)

    let isSubset (set1: ImmutableSortedSet<'T>) (set2: ImmutableSortedSet<'T>) =
        set1.IsSubsetOf(set2)

    let isSuperset (set1: ImmutableSortedSet<'T>) (set2: ImmutableSortedSet<'T>) =
        set1.IsSupersetOf(set2)

    let forall (predicate: 'T -> bool) (set: ImmutableSortedSet<'T>) =
        set
        |> toSeq
        |> Seq.forall predicate

    let iter (action: 'T -> unit) (set: ImmutableSortedSet<'T>) =
        set
        |> toSeq
        |> Seq.iter action
