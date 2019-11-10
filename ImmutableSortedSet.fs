namespace ImmutableSortedSet
open System.Collections.Immutable

module ImmutableSortedSet =
    let add (value: 'T) (set: ImmutableSortedSet<'T>) =
        set.Add(value)

    let count (set: ImmutableSortedSet<'T>) =
        set.Count

    let rev (set: ImmutableSortedSet<'T>) =
        set.Reverse()

    let remove (value: 'T) (set: ImmutableSortedSet<'T>) =
        set.Remove(value)

    let contains (value: 'T) (set: ImmutableSortedSet<'T>) =
        set.Contains(value)
