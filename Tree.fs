namespace Tree

type Tree<'a> =
    | Branch of 'a * Tree<'a> * Tree<'a>
    | Leaf

module Tree =
    let rec size (tree: Tree<'a>) =
        match tree with
        | Branch(_, left, right) -> 1 + size left + size right
        | Leaf -> 0

    let rec depth (tree: Tree<'a>) =
        match tree with
        | Branch(_, left, right) -> 1 + max (size left) (size right)
        | Leaf -> 0

    let rec comptree k n =
        match n with
        | 0 -> Leaf
        | _ ->
            let left = comptree (2 * k) (n - 1)
            let right = comptree (2 * k + 1) (n - 1)
            Branch(k, left, right)

    let rec reflect (tree: Tree<'a>) =
        match tree with
        | Leaf -> Leaf
        | Branch(v, left, right) -> Branch(v, reflect right, reflect left)

    //習題 4.13
    let rec compsame k n =
        match n with
        | 0 -> Leaf
        | _ ->
            let left = compsame k (n - 1)
            let right = compsame k (n - 1)
            Branch(k, left, right)
