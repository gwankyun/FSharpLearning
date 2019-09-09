namespace Dict

type Dict<'a> = Tree.Tree<string * 'a>

module Dict = 
    open Tree

    type StringCompare =
        | Greater
        | Equal
        | Less

    module String =
        let compare (a: string) (b: string) =
            let c = a.CompareTo(b)
            if c > 0 then
                StringCompare.Greater
            else if c = 0 then
                StringCompare.Equal
            else
                StringCompare.Less

    let empty = Leaf

    let rec lookup (dict: Dict<'a>) (key: string) =
        match (dict, key) with
        | (Leaf, _) -> None
        | (Branch((a, x), left, right), b) ->
            match (String.compare a b) with
            | StringCompare.Greater -> lookup left b
            | StringCompare.Equal -> Some x
            | StringCompare.Less -> lookup right b

    let rec insert (dict: Dict<'a>) (key: string) (value: 'a) =
        match (dict, key, value) with
        | (Leaf, _, _) -> Branch((key, value), Leaf, Leaf)
        | (Branch((k, v), left, right), _, _) ->
            let data = (k, v)
            match (String.compare k key) with
            | StringCompare.Greater ->
                let lf = insert left key value
                Branch(data, lf, right)
            | StringCompare.Equal ->
                Branch(data, left, right)
            | StringCompare.Less ->
                let rg = insert right key value
                Branch(data, left, rg)
