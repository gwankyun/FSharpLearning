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

    let rec lookup (key: string) (dict: Dict<'a>) =
        match dict with
        | Leaf -> None
        | Branch((a, x), left, right) ->
            match (String.compare a key) with
            | Greater -> lookup key left
            | Equal -> Some x
            | Less -> lookup key right

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
