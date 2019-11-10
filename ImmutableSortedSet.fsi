namespace ImmutableSortedSet
open System.Collections.Immutable
open System.Collections.Generic

module ImmutableSortedSet =
    val empty<'T when 'T : comparison> : ImmutableSortedSet<'T>

    val isEmpty : set:ImmutableSortedSet<'T> -> bool

    val emptyWith : comparer:IComparer<'T> -> ImmutableSortedSet<'T>

    val add : value:'T -> set:ImmutableSortedSet<'T> -> ImmutableSortedSet<'T>

    val singleton : value:'T -> ImmutableSortedSet<'T> when 'T : comparison

    val singletonWith : comparer:IComparer<'T> -> value:'T -> ImmutableSortedSet<'T>

    val count : set:ImmutableSortedSet<'T> -> int

    val rev : set:ImmutableSortedSet<'T> -> ImmutableSortedSet<'T>

    val remove : value:'T -> set:ImmutableSortedSet<'T> -> ImmutableSortedSet<'T>

    val contains : value:'T -> set:ImmutableSortedSet<'T> -> bool

    val ofList : elements:'T list -> ImmutableSortedSet<'T> when 'T : comparison

    val ofArray : array:'T [] -> ImmutableSortedSet<'T> when 'T : comparison

    val ofSeq : elements:'T seq -> ImmutableSortedSet<'T> when 'T : comparison

    val toSeq : set:ImmutableSortedSet<'T> -> 'T seq

    val toList : set:ImmutableSortedSet<'T> -> 'T list

    val toArray : set:ImmutableSortedSet<'T> -> 'T []

    val ofListWith : comparer:IComparer<'T> -> elements:'T list -> ImmutableSortedSet<'T>

    val ofArrayWith : comparer:IComparer<'T> -> array:'T [] -> ImmutableSortedSet<'T>

    val ofSeqWith : comparer:IComparer<'T> -> elements:'T seq -> ImmutableSortedSet<'T>

    val map : mapping:('T -> 'U) -> set:ImmutableSortedSet<'T> -> ImmutableSortedSet<'U> when 'U : comparison

    val filter : predicate:('T -> bool) -> set:ImmutableSortedSet<'T> -> ImmutableSortedSet<'T> when 'T : comparison

    val difference : set1:ImmutableSortedSet<'T> -> set2:ImmutableSortedSet<'T> -> ImmutableSortedSet<'T> when 'T : comparison

    val intersect : set1:ImmutableSortedSet<'T> -> set2:ImmutableSortedSet<'T> -> ImmutableSortedSet<'T>

    val union : set1:ImmutableSortedSet<'T> -> set2:ImmutableSortedSet<'T> -> ImmutableSortedSet<'T>

    val isProperSubset : set1:ImmutableSortedSet<'T> -> set2:ImmutableSortedSet<'T> -> bool

    val isProperSuperset : set1:ImmutableSortedSet<'T> -> set2:ImmutableSortedSet<'T> -> bool

    val isSubset : set1:ImmutableSortedSet<'T> -> set2:ImmutableSortedSet<'T> -> bool

    val isSuperset : set1:ImmutableSortedSet<'T> -> set2:ImmutableSortedSet<'T> -> bool

    val maxElement : set:ImmutableSortedSet<'T> -> 'T

    val minElement : set:ImmutableSortedSet<'T> -> 'T

    val exists : predicate:('T -> bool) -> set:ImmutableSortedSet<'T> -> bool

    val fold : folder:('State -> 'T -> 'State) -> state:'State -> set:ImmutableSortedSet<'T> -> 'State

    val foldBack : folder:('T -> 'State -> 'State) -> set:ImmutableSortedSet<'T> -> state:'State -> 'State

    val intersectMany : sets:seq<ImmutableSortedSet<'T>> -> ImmutableSortedSet<'T>

    val unionMany : sets:seq<ImmutableSortedSet<'T>> -> ImmutableSortedSet<'T>

    val partition : predicate:('T -> bool) -> set:ImmutableSortedSet<'T> -> ImmutableSortedSet<'T> * ImmutableSortedSet<'T> when 'T : comparison

    val forall : predicate:('T -> bool) -> set:ImmutableSortedSet<'T> -> bool

    val iter : action:('T -> unit) -> set:ImmutableSortedSet<'T> -> unit

    val item : index:int -> set:ImmutableSortedSet<'T> -> 'T

    val head : set:ImmutableSortedSet<'T> -> 'T

    val tail : set:ImmutableSortedSet<'T> -> ImmutableSortedSet<'T>
