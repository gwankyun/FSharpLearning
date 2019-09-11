module OrderedSet

type OrderedSet<'a when 'a : comparison> =
    'a option * Map<'a, 'a option * 'a option> * 'a option

module OrderedSet =
    let empty : OrderedSet<'a> = (None, Map.empty, None)

    let isEmpty (set: OrderedSet<'a>) =
        let (_, s, _) = set
        s |> Map.isEmpty
