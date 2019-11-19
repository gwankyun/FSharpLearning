namespace ImmutableSortedMap
open ImmutableSortedSet
open System.Collections.Immutable
open System.Collections.Generic

type ImmutableSortedMap<'Key, 'Value when 'Key : comparison and 'Value : equality> =
    ImmutableSortedSet<'Key> * Map<'Key, 'Value>

module ImmutableSortedMap =
    let empty<'Key, 'Value when 'Key : comparison and 'Value : equality> =
        (ImmutableSortedSet.empty<'Key>, Map.empty)

    let isEmpty (table: ImmutableSortedMap<'Key, 'T>) =
        let (t, _) = table
        t.IsEmpty

    let add (key: 'Key) (value: 'T) (table: ImmutableSortedMap<'Key, 'T>) =
        let (t, m) = table
        (t |> ImmutableSortedSet.add key, m |> Map.add key value)

    let remove (key: 'Key) (table: ImmutableSortedMap<'Key, 'T>) =
        let (t, m) = table
        (t |> ImmutableSortedSet.remove key, m |> Map.remove key)

    let ofList (elements: ('Key * 'T) list) =
        elements
        |> List.fold (fun s (k, v) -> s |> add k v) empty<'Key, 'T>

    //let toSeq (table: ImmutableSortedMap<'Key, 'T>) =
    //    let (t, m) = table
    //    seq {
    //        for i in t do
    //            match m |> Map.tryFind i.Key with
    //            | Some(v) -> (i.Key, v)
    //            | None -> (i.Key, i.Value)
    //    }

    let toList (table: ImmutableSortedMap<'Key, 'T>) =
        let (t, m) = table
        let keys = t |> ImmutableSortedSet.toList
        keys |> List.map (fun x -> (x, m.[x]))

