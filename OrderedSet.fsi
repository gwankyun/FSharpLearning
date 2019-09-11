module OrderedSet

type OrderedSet<'a when 'a : comparison> =
    'a option * Map<'a, 'a option * 'a option> * 'a option

module OrderedSet =
    val empty : OrderedSet<'a>

    val isEmpty : OrderedSet<'a> -> bool
