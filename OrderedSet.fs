module OrderedSet

type OrderedSet<'a when 'a : comparison> =
    'a option * Map<'a, 'a option * 'a option> * 'a option

module OrderedSet =
    let empty : OrderedSet<'a> = (None, Map.empty, None)

    let isEmpty (set: OrderedSet<'a>) =
        let (_, s, _) = set
        s |> Map.isEmpty

    let add (key: 'a) (set: OrderedSet<'a>) : OrderedSet<'a> =
        let (f, s, l) = set
        match set |> isEmpty with
        | true ->
            let f = Some key
            let l = Some key
            let s = Map.empty |> Map.add key (None, None)
            (f, s, l)
        | false ->
            let lv = l.Value
            let (p, _) = s |> Map.find lv
            let s =
                s
                |> Map.add lv (p, Some key)
                |> Map.add key (l, None)
            let l = Some key
            (f, s, l)

    let remove (key: 'a) (set: OrderedSet<'a>) =
        let (f, s, l) = set
        match set |> isEmpty with
        | true -> set
        | false ->
            match s |> Map.tryFind key with
            | Some(p, n) -> 
                let s = s |> Map.remove key
                match (f.Value = key, l.Value = key) with
                | (true, true) -> empty
                | (true, _) ->
                    let nv = n.Value
                    let (_, nn) = s |> Map.find nv
                    (n, s |> Map.add nv (None, nn), l)
                | (_, true) ->
                    let pv = p.Value
                    let (lp, _) = s |> Map.find pv
                    (f, s |> Map.add pv (lp, None), p)
                | (_, _) ->
                    let pv = p.Value
                    let nv = n.Value
                    let (lp, _) = s |> Map.find pv
                    let (_, nn) = s |> Map.find nv
                    (f, s |> Map.add pv (lp, n) |> Map.add nv (p, nn), l)
            | None -> set

    let toList (set: OrderedSet<'a>) =
        let rec inner (ls: 'a list) (s: OrderedSet<'a>) =
            match s |> isEmpty with
            | true -> ls
            | false ->
                let (f, _, l) = s
                let fv = f.Value
                inner (fv :: ls) (s |> remove fv)
        (inner [] set) |> List.rev
