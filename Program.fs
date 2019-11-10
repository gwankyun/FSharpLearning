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

type ImmutableSortedSetCheck() =
    static member list (elements: int list) =
        let elements = elements |> List.distinct
        elements = (elements |> ImmutableSortedSet.ofList |> ImmutableSortedSet.toList)

    static member array (elements: int []) =
        let elements = elements |> Array.distinct
        elements = (elements |> ImmutableSortedSet.ofArray |> ImmutableSortedSet.toArray)

    static member seq (elements: int list) =
        let elements = elements |> Seq.toList |> List.distinct
        elements = (elements |> ImmutableSortedSet.ofList |> ImmutableSortedSet.toList)

    static member map (elements: int list) (mapping: int -> int) =
        let a =
            elements
            |> ImmutableSortedSet.ofList
            |> ImmutableSortedSet.map mapping
            |> ImmutableSortedSet.toList
        let b = elements |> List.map mapping |> List.distinct
        a = b

    static member filter (elements: int list) (predicate: int -> bool) =
        let a =
            elements
            |> ImmutableSortedSet.ofList
            |> ImmutableSortedSet.filter predicate
            |> ImmutableSortedSet.toList
        let b = elements |> List.filter predicate |> List.distinct
        a = b

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

    let os =
        ImmutableSortedSet.ofList ["3"; "7"; "2"; "1"]

    for i in os do
        printfn "%s" i

    Check.Quick ImmutableSortedSetCheck.list
    Check.Quick ImmutableSortedSetCheck.array
    Check.Quick ImmutableSortedSetCheck.seq
    Check.Quick ImmutableSortedSetCheck.map
    Check.Quick ImmutableSortedSetCheck.filter
    0 // return an integer exit code
