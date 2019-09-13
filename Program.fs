// Learn more about F# at http://fsharp.org

open System
open Tree
open Dict
open OrderedSet

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    let tree : Tree<int> = Branch(0, Branch(1, Leaf, Leaf), (Branch(2, Leaf, Branch(3, Leaf, Leaf))))
    printfn "size: %i" (Tree.size tree)
    printfn "depth: %i" (Tree.depth tree)
    let ct = Tree.comptree 1 5
    printfn "comptree:%A" ct
    let lt = Tree.reflect ct
    printfn "reflect:%A" lt
    let compsame = Tree.compsame 0 5
    printfn "compsame:%A" compsame
    printfn "isReflect:%A" (Tree.isReflect ct (Tree.reflect ct))
    printfn "preorder:%A" (Tree.preorder ct)
    printfn "inorder:%A" (Tree.inorder ct)
    printfn "postorder:%A" (Tree.postorder ct)
    let ls = [1..9]
    printfn "balpre:%A" (ls |> Tree.balpre |> Tree.preorder)
    printfn "balin:%A" (ls |> Tree.balin |> Tree.inorder)
    printfn "balpost:%A" (ls |> Tree.balpost |> Tree.postorder)
    let dict = Dict.empty |> Dict.insert "3" 3 |> Dict.insert "7" 7 |> Dict.insert "2" 2 |> Dict.insert "1" 1
    printfn "dict:%A" dict
    let os = OrderedSet.empty |> OrderedSet.add 1 |> OrderedSet.add 2 |> OrderedSet.add 3
    printfn "OrderedSet 1:%A" (os |> OrderedSet.remove 1)
    printfn "OrderedSet 2:%A" (os |> OrderedSet.remove 2)
    printfn "OrderedSet 3:%A" (os |> OrderedSet.remove 3)
    printfn "toList:%A" (os |> OrderedSet.toList)
    printfn "contains:%A" (os |> OrderedSet.contains 2)
    0 // return an integer exit code
