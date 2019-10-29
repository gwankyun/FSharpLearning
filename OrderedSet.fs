namespace OrderedSet

type OrderedSet<'a when 'a : comparison> =
    'a option * Map<'a, 'a option * 'a option> * 'a option

module OrderedSet =
    let empty : OrderedSet<'a> = (None, Map.empty, None)

    let isEmpty (set: OrderedSet<'a>) =
        let (_, s, _) = set
        s |> Map.isEmpty

    let first (set: OrderedSet<'a>) =
        let (f, _, _) = set
        f.Value

    let last (set: OrderedSet<'a>) =
        let (_, _, l) = set
        l.Value

    let count (set: OrderedSet<'a>) =
        let (_, s, _) = set
        s |> Map.count

    let removeFirst (set: OrderedSet<'a>) =
        match set |> count with
        | 1 -> empty
        | _ ->
            let (f, m, l) = set
            let fv = f.Value
            let (_, n) = m |> Map.find fv
            let nv = n.Value
            let (_, nn) = m |> Map.find nv
            (n, m |> Map.remove fv |> Map.add nv (None, nn), l)

    let removeLast (set: OrderedSet<'a>) =
        match set |> count with
        | 1 -> empty
        | _ ->
            let (f, m, l) = set
            let lv = l.Value
            let (p, _) = m |> Map.find lv
            let pv = p.Value
            let (pp, _) = m |> Map.find pv
            (f, m |> Map.remove lv |> Map.add pv (pp, None), p)

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
                //let (_, _, l) = s
                //let lv = l.Value
                let lv = s |> last
                inner (lv :: ls) (s |> remove lv)
        inner [] set

    let ofList (elements: 'a list) =
        let rec inner (ls: 'a list) (set: OrderedSet<'a>) =
            match ls with
            | h :: t -> inner t (set |> add h)
            | [] -> set
        inner elements empty

    let contains (key: 'a) (set: OrderedSet<'a>) =
        let (_, s, l) = set
        s |> Map.containsKey key

    let filter (predicate: 'a -> bool) (set: OrderedSet<'a>) =
        set |> toList |> List.filter predicate |> ofList

    let map (mapping: 'a -> 'b) (set: OrderedSet<'a>) =
        set |> toList |> List.map mapping |> ofList

    let fold (folder: 'a -> 'b -> 'a) (state: 'a) (set: OrderedSet<'b>) =
        set |> toList |> List.fold folder state
