namespace Tree

type Tree<'a> =
    | Branch of 'a * Tree<'a> * Tree<'a>
    | Leaf

module Tree =
    val size : Tree<'a> -> int

    val depth : Tree<'a> -> int

    val comptree : int -> int -> Tree<int>

    val reflect : Tree<'a>  -> Tree<'a>

    val compsame : 'a -> int -> Tree<'a>

    val isReflect : Tree<'a> -> Tree<'a> -> bool when 'a : equality

    val preorder : Tree<'a> -> 'a list

    val inorder : Tree<'a> -> 'a list

    val postorder : Tree<'a> -> 'a list
