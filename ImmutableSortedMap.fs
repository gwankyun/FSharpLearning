namespace ImmutableSortedMap
open System.Collections.Immutable
open System.Collections.Generic

type ImmutableSortedMap<'TKey, 'TValue when 'TKey : comparison and 'TValue : equality> =
    ImmutableSortedDictionary<'TKey, 'TValue> * Map<'TKey, 'TValue>

module ImmutableSortedMap =
    let empty<'TKey, 'TValue when 'TKey : comparison and 'TValue : equality> =
        let keyComparer = {
            new IComparer<'TKey> with
                member this.Compare(x: 'TKey, y: 'TKey) =
                    match compare x y with
                    | 0 -> 0
                    | _ -> 1
        }
        let valueComparer = {
            new IEqualityComparer<'TValue> with
                member this.Equals(a: 'TValue, b: 'TValue) = a = b
                member this.GetHashCode(obj: 'TValue) = obj.GetHashCode()
        }
        let m : Map<'TKey, 'TValue> = Map.empty
        (ImmutableSortedDictionary.Create<'TKey, 'TValue>(keyComparer, valueComparer), m)

    let isEmpty (table: ImmutableSortedMap<'Key, 'T>) =
        let (t, _) = table
        t.IsEmpty

    let add (key: 'Key) (value: 'T) (table: ImmutableSortedMap<'Key, 'T>) =
        let (t, m) = table
        match t.ContainsKey(key) with
        | true -> (t, m |> Map.add key value)
        | false -> (t.Add(key, value), m)

    let remove (key: 'Key) (table: ImmutableSortedMap<'Key, 'T>) =
        let (t, m) = table
        (t.Remove(key), m |> Map.remove key)

    let ofList (elements: ('Key * 'T) list) =
        elements
        |> List.fold (fun s (k, v) -> s |> add k v) empty<'Key, 'T>

    let toSeq (table: ImmutableSortedMap<'Key, 'T>) =
        let (t, m) = table
        seq {
            for i in t do
                match m |> Map.tryFind i.Key with
                | Some(v) -> (i.Key, v)
                | None -> (i.Key, i.Value)
        }

    let toList (table: ImmutableSortedMap<'Key, 'T>) =
        let (t, m) = table
        let keys = t.Keys
        keys |> Seq.map (fun x -> (x, m.[x])) |> Seq.toList

