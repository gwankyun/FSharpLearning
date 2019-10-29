namespace OrderedSetCheck
open OrderedSet

module OrderedSetCheck =
    let add (ls: int list) =
        match ls with
        | [] -> true
        | _ ->
            let ls = ls |> List.distinct
            let s = ls |> List.fold (fun s t -> s |> OrderedSet.add t) OrderedSet.empty
            let (f, _, l) = s
            let fv = f.Value
            let lv = l.Value
            (fv, lv) = (ls |> List.head, ls |> List.last)

    let ofList (ls: int list) =
        match ls with
        | [] -> true
        | _ ->
            let a = ls |> List.fold (fun s t -> s |> OrderedSet.add t) OrderedSet.empty
            let o = ls |> OrderedSet.ofList
            a = o

    let count (x: int list) =
        x |> OrderedSet.ofList |> OrderedSet.count = (x |> List.distinct |> List.length)

    let isEmpty (x: int list) =
        x |> OrderedSet.ofList |> OrderedSet.isEmpty = (x |> List.isEmpty)

    let contains (x: int list) (a: int) =
        x |> OrderedSet.ofList |> OrderedSet.contains a = (x |> List.contains a)
