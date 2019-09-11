module OrderedSet

type OrderedSet<'a when 'a : comparison> =
    'a option * Map<'a, 'a option * 'a option> * 'a option

module OrderedSet =
    let empty : OrderedSet<'a> = (None, Map.empty, None)

    let isEmpty (set: OrderedSet<'a>) =
        let (_, s, _) = set
        s |> Map.isEmpty

    let add (key: 'a) (set: OrderedSet<'a>) =
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
