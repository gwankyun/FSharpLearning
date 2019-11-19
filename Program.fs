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
open ImmutableSortedSetCheck
open ImmutableSortedMap

type Comp() =
    interface IComparer<string> with
        member this.Compare(a: string, b: string) =
            match a = b with
            | true -> 0
            | false -> 1

//type ImmutableSortedMapCheck() =
//    static member list (elements: (int * int) list) =
//        let m = elements |> Map.ofList
//        let key = elements |> List.map (fun (k, _) -> k) |> List.distinct |> List.map (fun x -> (x, m.[x]))
//        printfn "key: %A" key
//        let mapList = elements |> ImmutableSortedMap.ofList |> ImmutableSortedMap.toList
//        printfn "map: %A" mapList
//        key = mapList

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

    let os =
        ImmutableSortedSet.ofList ["3"; "7"; "2"; "1"]

    for i in os do
        printfn "%s" i

    //let dict =
    //    ImmutableSortedMap.empty<int, int>
    //    |> ImmutableSortedMap.add 3 1
    //    |> ImmutableSortedMap.add 7 2
    //    |> ImmutableSortedMap.add 2 3
    //    |> ImmutableSortedMap.add 1 4

    //for i in dict do
    //    printfn "%i: %i" i.Key i.Value

    Check.Quick ImmutableSortedSetCheck.list
    Check.Quick ImmutableSortedSetCheck.array
    Check.Quick ImmutableSortedSetCheck.seq
    Check.Quick ImmutableSortedSetCheck.map
    Check.Quick ImmutableSortedSetCheck.filter

    //Check.Quick ImmutableSortedMapCheck.list
    0 // return an integer exit code
