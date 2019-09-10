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

    let rec insert (key: string) (value: 'a) (dict: Dict<'a>) : Dict<'a> =
        match dict with
        | Leaf -> Branch((key, value), Leaf, Leaf)
        | Branch((k, v), left, right) ->
            let data = (k, v)
            match (String.compare k key) with
            | Greater ->
                let lf = insert key value left
                Branch(data, lf, right)
            | Equal ->
                Branch(data, left, right)
            | Less ->
                let rg = insert key value right
                Branch(data, left, rg)
