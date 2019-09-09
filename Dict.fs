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
