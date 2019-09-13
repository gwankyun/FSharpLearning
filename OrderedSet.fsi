module OrderedSet

type OrderedSet<'a when 'a : comparison> =
    'a option * Map<'a, 'a option * 'a option> * 'a option

module OrderedSet =
    val empty : OrderedSet<'a>

    val isEmpty : OrderedSet<'a> -> bool

    val add : 'a -> OrderedSet<'a> -> OrderedSet<'a>

    val remove : 'a -> OrderedSet<'a> -> OrderedSet<'a>

    val toList : OrderedSet<'a> -> 'a list

    val contains : 'a -> OrderedSet<'a> -> bool

    val count : OrderedSet<'a> -> int
