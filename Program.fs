// Learn more about F# at http://fsharp.org

open System

type Tree<'a> =
    | Branch of 'a * Tree<'a> * Tree<'a>
    | Leaf

module Tree =
    let rec size (tree: Tree<'a>) =
        match tree with
        | Branch(_, left, right) -> 1 + (size left) + (size right)
        | Leaf -> 0

    let rec depth (tree: Tree<'a>) =
        match tree with
        | Branch(_, left, right) -> (1 + max (size left) (size right))
        | Leaf -> 0

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    let tree : Tree<int> = Branch(0, Branch(1, Leaf, Leaf), (Branch(2, Leaf, Branch(3, Leaf, Leaf))))
    printfn "size: %i" (Tree.size tree)
    printfn "depth: %i" (Tree.depth tree)
    0 // return an integer exit code
