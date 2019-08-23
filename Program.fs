﻿// Learn more about F# at http://fsharp.org

open System
open Tree

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
    0 // return an integer exit code
