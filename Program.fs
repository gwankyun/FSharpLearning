// Learn more about F# at http://fsharp.org

open System
open Tree
open Dict
open OrderedSet
open FsCheck
open OrderedSetCheck
open System.Collections.Immutable
open System.Collections.Generic
open ImmutableSortedSet

//type StrComp< =
//    interface with IComparer

type Comp() =
    interface IComparer<string> with
        member this.Compare(a: string, b: string) =
            match a = b with
            | true -> 0
            | false -> 1

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

    Check.Quick OrderedSetCheck.add

    Check.Quick OrderedSetCheck.ofList

    Check.Quick OrderedSetCheck.count

    Check.Quick OrderedSetCheck.isEmpty

    Check.Quick OrderedSetCheck.contains

    Check.Quick OrderedSetCheck.removeFirst

    let com = new Comp()

    let os =
        ImmutableSortedSet.Create<string>({
            new IComparer<string> with
                member this.Compare(a: string, b: string) =
                    match a = b with
                    | true -> 0
                    | false -> 1
                })
        |> ImmutableSortedSet.add "3"
        |> ImmutableSortedSet.add "7"
        |> ImmutableSortedSet.add "2"
        |> ImmutableSortedSet.add "1"
        |> ImmutableSortedSet.add "7"

    for i in os do
        printfn "%s" i

    //Check.Quick OrderedSetCheck.remove

    //printfn "toList:%A" (os |> OrderedSet.toList)
    //printfn "ofList:%A" ([1; 2; 3] |> OrderedSet.ofList)
    //printfn "filter:%A" ([1; 2; 3] |> OrderedSet.ofList |> OrderedSet.filter (fun x -> x < 3))
    //printfn "map:%A" ([1; 2; 3] |> OrderedSet.ofList |> OrderedSet.map (fun x -> x * 2))
    //printfn "fold:%A" ([1; 2; 3] |> OrderedSet.ofList |> OrderedSet.fold (fun a b -> a + b) 0)
    0 // return an integer exit code
