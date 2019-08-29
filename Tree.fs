namespace Tree

type Tree<'a> =
    | Branch of 'a * Tree<'a> * Tree<'a>
    | Leaf

module Tree =
    module List =
        let split2 (lst: 'a list) =
            let k = (List.length lst) / 2
            List.splitAt k lst

        let beforeLast (lst: 'a list) =
            let length = List.length lst
            List.take (length - 1) lst

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

    //習題 4.15
    let rec isReflect (a: Tree<'a>) (b: Tree<'a>) =
        match (a, b) with
        | (Leaf, Leaf) -> true
        | (Branch(va, la, ra), Branch(vb, lb, rb)) ->
            va = vb && isReflect la rb && isReflect ra lb
        | _ -> false

    let rec preorder (tree: Tree<'a>) =
        match tree with
        | Leaf -> []
        | Branch(x, left, right) -> [x] @ preorder left @ preorder right

    let rec inorder (tree: Tree<'a>) =
        match tree with
        | Leaf -> []
        | Branch(x, left, right) -> inorder left @ [x] @ inorder right

    let rec postorder (tree: Tree<'a>) =
        match tree with
        | Leaf -> []
        | Branch(x, left, right) -> postorder left @ postorder right @ [x]

    let rec balpre (lst: 'a list) =
        match lst with
        | [] -> Leaf
        | h::t ->
            let (f, s) = List.split2 t
            let left = balpre f
            let right = balpre s
            Branch(h, left, right)

    let rec balin (lst: 'a list) =
        match lst with
        | [] -> Leaf
        | _ ->
            let (f, s) = List.split2 lst
            match s with
            | [] -> Leaf
            | h::t ->
                let left = balin f
                let right = balin t
                Branch(h, left, right)

    let rec balpost (lst: 'a list) =
        match lst with
        | [] -> Leaf
        | _ ->
            let (f, s) = List.split2 lst
            match s with
            | [] -> Leaf
            | _ ->
                let left = balpost f
                let right = balpost (List.beforeLast s)
                Branch(List.last s, left, right)