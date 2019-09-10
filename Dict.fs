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
                Greater
            else if c = 0 then
                Equal
            else
                Less

    let empty = Leaf

    let rec lookup (dict: Dict<'a>) (key: string) =
        match (dict, key) with
        | (Leaf, _) -> None
        | (Branch((a, x), left, right), b) ->
            match (String.compare a b) with
            | Greater -> lookup left b
            | Equal -> Some x
            | Less -> lookup right b

    let rec insert (dict: Dict<'a>) (key: string) (value: 'a) =
        match (dict, key, value) with
        | (Leaf, _, _) -> Branch((key, value), Leaf, Leaf)
        | (Branch((k, v), left, right), _, _) ->
            let data = (k, v)
            match (String.compare k key) with
            | Greater ->
                let lf = insert left key value
                Branch(data, lf, right)
            | Equal ->
                Branch(data, left, right)
            | Less ->
                let rg = insert right key value
                Branch(data, left, rg)
