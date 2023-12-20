![OPEX](https://github.com/Moreault/OPEX/blob/master/opex.png)
# OPEX
OverPowered Enumerable eXtensions - _It makes your collections OP AF!_

## Good to know
If you are already familiar with [ToolBX.Collections](https://github.com/Moreault/Collections) then these extension methods will feel natural. As with the rest of ToolBX, we like to throw a lot but we also provide "Try" overloads for the lazier devs out there.

## IsNullOrEmpty
You used to do this
```cs
if (collection == null || !collection.Any())
{
	...
}
```

Now you can do this for the exact same effect
```cs
if (collection.IsNullOrEmpty())
{
	...
}
```

## AddRange for everyone!
`List<T>` has an `AddRange` method in C# that isn't on any other collection but it definitely should be standard across the board if you ask me. OPEX adds an `AddRange` extension method to `IList<T>` which means that it can now be used with any non-fixed collection that implements `IList<T>`. 

```cs
var items = _database.GetEverything();
collection.AddRange(items);
```

## Concat for singles!
Isn't it annoying that you have to do this?
```cs
var newList = list.Concat(new [] { item }).ToList();
```

Now you can do this
```cs
var newList = list.Concat(item).ToList();
```

You can even do this
```cs
var newList = list.Concat(item1, item2, item3).ToList();
```

## FirstIndexOf and LastIndexOf
Returns the index of the first or last match for your item or predicate in the collection or `-1` if there is no match.
```cs
var index = collection.FirstIndexOf(item);
var index = collection.FirstIndexOf(x => x.Name == "Roger");

var index = collection.LastIndexOf(item);
var index = collection.LastIndexOf(x => x.Age > 21);
```

## IndexesOf
Returns the index of all matches or an empty collection.
```
var indexes = collection.IndexesOf(item);
var indexes = collection.IndexOf(x => x.Money <= 12000);
```

## SingleIndexOf
TODO

## GetRandomIndex, GetRandom
```cs
var index = collection.GetRandomIndex();

var item = collection.GetRandom();
```

## GetManyRandoms
Returns a specified number of unique random items from the collection or the entire, shuffled collection if that number is equal or greather than the collection.
```cs
var items = collection.GetManyRandoms(8);
```

## GetManyRandomIndexes
TODO

## Shuffle
Reorganizes the collection in a random order.
```cs
collection.Shuffle();

//As of 2.2.0, Shuffle() returns the collection so you can chain it
var collection = new List<int> { 1, 2, 3, 4, 5 }.Shuffle();
```

## ToShuffled
Returns a new collection with the same items in a random order.
```cs
var shuffled = collection.ToShuffled();
```

## IsWithinRange
Returns true if the index specified is within the collection's range.
```cs
if (collection.IsWithinRange(8)) 
{
	...
}
```

## LastIndex
Returns the last index in the collection. Normally, you would do `collection.Count - 1` to get this value.
```cs
var index = collection.LastIndex();
```

## PopAll
Removes and returns all desired items from a collection. It has overloads for a specific item or a predicate.
```cs
var items = collection.PopAll(item);

var items = collection.PopAll(x => x.Name.StartsWith("Bogus"));
```

## PopAt
Removes and returns the item at the specified index. It has an overload for multiple indexes.
```cs
var item = collection.PopAt(8);

var items = collection.PopAt(8, 12, 16);
```

## PopAtOrDefault
TODO

## TryPopAt
TODO

## PopFirst
Removes and returns the first item in the collection. It has an overload for a specific item or a predicate.
```cs
var result = collection.PopFirst();

var result = collection.PopFirst(item);

var result = collection.PopFirst(x => x.Name.StartsWith("Bogus"));
```

## PopFirstOrDefault
TODO

## TryPopFirst
TODO

## PopSingle
TODO

## PopSingleOrDefault
TODO

## TryPopSingle
TODO

## PopLast
Removes and returns the last item in the collection. It has an overload for a specific item or a predicate.
```cs
var result = collection.PopLast();

var result = collection.PopLast(item);

var result = collection.PopLast(x => x.Name.StartsWith("Bogus"));
```

## PopLastOrDefault
TODO

## TryPopLast
TODO

## PopRandom
Removes and returns a random item from the collection.
```cs
var result = collection.PopRandom();
```

## PopManyRandoms
Removes and returns a specified number of unique random items from the collection or the entire, shuffled collection if that number is equal or greather than the collection.
```cs

## RemoveAll
Removes all occurences from a collection. It has overloads for a specific item or a predicate.
```cs
collection.RemoveAll(item);

collection.RemoveAll(x => x.Name.StartsWith("Bogus"));
```

## RemoveFirst, TryRemoveFirst, RemoveLast and TryRemoveLast
Removes the first or last item in the collection according to the item or predicate specified. Overloads without `Try` in their name will throw exceptions if nothing is removed.

```cs
//Will remove the first item it finds
collection.RemoveFirst(item);

//Or the last
collection.RemoveLast(item);

collection.RemoveFirst(x => x.Id > 2);

collection.TryRemoveLast(x => x.Age == 24);
```

## SequenceEqualOrNull
Behaves like `SequenceEqual` with the exception that it checks for nulls. In other words, it will not throw exceptions if either or both collections are null. This is especially useful when overloading equality operators where you want to check sequence equality rather than reference equality.

```cs
public string? Name { get; init; }

public IReadOnlyList<int>? Ids { get; init; }

public bool Equals(Thingy other)
{
	if (other is null) return false;
	if (ReferenceEquals(this, other)) return true;
	return Name == other.Name && Ids.SequenceEqualOrNull(other.Ids);
}
```

## Split
Splits a collection into two by keeping the results into two collections in a `Splitted<T>` object.

```cs
//Used like a regular .Where method
var result = collection.Split(x => x.Name.Length > 10);

//Iterates through every item with names that are longer than ten characters
foreach (result.Remaining)
{
	...
}

//Iterates through every item with names that are shorter than or equal to ten characters
foreach (result.Excluded)
{
	...
}
```

## Swap
Swap two indexes toghether.

```cs
//Swaps the items at index 3 and 8
collection.Swap(3, 8);
```

## TryGetFirst, TryGetLast and TryGetSingle
These work like LINQ's `FirstOrDefault`, `LastOrDefault` and `SingleOrDefault` except that you're given a `TryGetResult<T>` instead of `T?`. The issue with `T?` is that there is no clear way of knowing whether the method returned `default(T)` because it could not be found or because that's what was found. `TryGetResult<T>` makes it clear with its `IsSuccess` property.

```cs
//They are used exactly like their `OrDefault` counterparts
var item = collection.TryGetFirst(x => x.Name == "Seb");

//This is also allowed in case you expect your collection to have exactly one (or no) item
var item = collection.TryGetSingle();
```

## Uniform and UniformOrDefault
Validates that all entries in the collection are "duplicates" (or that the collection is "uniform") and returns any of those entries. It's basically a cleaner alternative to using `First` or `FirstOrDefault` in cases where you have more than one entry in the collection but you don't care which entry is returned. What's wrong with `First` and `FirstOrDefault`, you say? It should really only be used when your intent is to actually get the first element in the collection. `Single` or `SingleOrDefault` should be used instead when there's not supposed to be more than one entry. But sometimes, you just get a garbage collection that you have little to no control over and you just need whichever duplicate entry.

It can also validate that all elements in the list have the same value for a specified property if used with a lambda. `All` could be used but `Uniform` returns the "first" of those elements as well.

```cs
//Returns any item in the collection if they're all duplicates or throws
var item = collection.Uniform();

//Returns any item in the collection if they're all duplicates or default
var item = collection.UniformOrDefault();

//Returns "Roger" if all items in the collection have "Roger" for a middle name or throws
var item = collection.Uniform(x => x.MiddleName == "Roger");

//Returns "Roger" if all items in the collection have "Roger" for a middle name or null
var item = collection.UniformOrDefault(x => x.MiddleName == "Roger");
```

## UniformBy and UniformByOrDefault
Same as `Uniform` and `UniformOrDefault` except that it will return the whole object instead of the property from the lambda. There are no non-lambda overloads of `UniformBy` and `UniformByOrDefault`

```cs
//Returns the item with "Roger" for a middle name if all items have the same value or throws
var item = collection.UniformBy(x => x.MiddleName == "Roger");

//Returns the item with "Roger" for a middle name if all items have the same value or default
var item = collection.UniformByOrDefault(x => x.MiddleName == "Roger");
```

## Default LINQ methods with custom exceptions and messages
LINQ methods like `First`, `Last`, `Single`, `FirstOrDefaut`, `LastOrDefault` and `SingleOrDefault` all have overloads that take a custom exception and message. This is useful when you want to throw a custom exception with a custom message instead of the default `InvalidOperationException` and message.

```cs
var item = collection.First(x => x.Name == "Seb", new ArgumentException("Seb is not in the collection"));

//Or with a message
var item = collection.First(x => x.Name == "Seb", x => new ArgumentException("Seb is not in the collection"));

//Or with a message
var item = collection.First(x => x.Name == "Seb", "Seb is not in the collection");
```


## IReadOnlyList extensions
These extensions are from the now depreacted [ToolBX.Collections.ReadOnly](https://github.com/Moreault/Collections/ReadOnly) as of 2.2.0. Starting with 3.0.0, they will be removed from ToolBX.Collections and will only be available in OPEX.

### With
Returns a new collection with the specified item added at the end. This is the same as `Concat` except that it returns an `IReadOnlyList<T>` instead of an `IEnumerable<T>`.

```cs
var list = new List<int> { 1, 2, 3, 4, 5 };

//Returns a new list containing 1, 2, 3, 4, 5, 6, 7 and 8
var newList = list.With(6, 7, 8);
```

### WithAt
Returns a new collection with the specified item or items added at the specified index. This is the same as `Insert` or `InsertRange` except that it returns an `IReadOnlyList<T>` instead of modifying a collection.

```cs
var list = new List<char> { 'a', 'b', 'c', 'd', 'e' };

//Returns a new list containing 'a', 'b', 'c', 'f', 'd', 'e'
var newList = list.WithAt(3, 'f');

//Returns a new list containing 'a', 'b', 'c', 'f', 'g', 'h', 'd', 'e'
var newList = list.WithAt(3, 'f', 'g', 'h');
```

### Without
Returns a new collection with the specified item removed. This is the same as `Remove` or `RemoveAll` except that it returns an `IReadOnlyList<T>` instead of modifying a collection.

```cs
var list = new List<int> { 1, 2, 3, 4, 5 };

//Returns a new list containing 1, 2, 3, 5
var newList = list.Without(4);
```

### WithoutAt
Returns a new collection with the item at the specified index removed. This is the same as `RemoveAt` except that it returns an `IReadOnlyList<T>` instead of modifying a collection.

```cs
var list = new List<int> { 1, 2, 3, 4, 5 };

//Returns a new list containing 1, 2, 3, 5
var newList = list.WithoutAt(3);
```

### WithSwapped
Returns a new collection with the items at the specified indexes swapped. This is the same as `Swap` except that it returns an `IReadOnlyList<T>` instead of modifying a collection.

```cs
var list = new List<int> { 1, 2, 3, 4, 5 };

//Returns a new list containing 1, 2, 3, 5, 4
var newList = list.WithSwapped(3, 4);
```