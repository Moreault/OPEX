![OPEX](https://github.com/Moreault/OPEX/blob/master/opex.png)
# OPEX
OverPowered Enumerable eXtensions - _It makes your collections OP AF!_

## Good to know
If you are already familiar with [ToolBX.Collections](https://github.com/Moreault/Collections) then the extension methods will feel natural. We like to throw a lot but we also provide "Try" overloads for the lazy devs out there.

## IsNullOrEmpty
You used to do this
````c#
if (collection == null || !collection.Any())
{
	...
}
```

Now you can do this for the exact same effect
````c#
if (collection.IsNullOrEmpty())
{
	...
}
```

## AddRange for everyone!
`List<T>` has an `AddRange` method in C# that isn't on any other collection but it definitely should be standard across the board if you ask me. OPEX adds an `AddRange` extension method to `IList<T>` which means that it can now be used with any non-fixed collection that implements `IList<T>`. 

```c#
var items = _database.GetEverything();
collection.AddRange(items);
```

## Concat for singles!
Isn't it annoying that you have to do this?
```c#
var newList = list.Concat(new [] { item }).ToList();
```

Now you can do this
```c#
var newList = list.Concat(item).ToList();
```

You can even do this
```c#
var newList = list.Concat(item1, item2, item3).ToList();
```

## FirstIndexOf and LastIndexOf
Returns the index of the first or last match for your item or predicate in the collection or `-1` if there is no match.
```c#
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

## GetRandomIndex, GetRandom
```c#
var index = collection.GetRandomIndex();

var item = collection.GetRandom();
```

## GetManyRandoms
Returns a specified number of unique random items from the collection or the entire, shuffled collection if that number is equal or greather than the collection.
```c#
var items = collection.GetManyRandoms(8);
```

## Shuffle
Reorganizes the collection in a random order.
```c#
collection.Shuffle();
```

## IsWithinRange
Returns true if the index specified is within the collection's range.
```c#
if (collection.IsWithinRange(8)) 
{
	...
}
```

## LastIndex
Returns the last index in the collection. Normally, you would do `collection.Count - 1` to get this value.
```c#
var index = collection.LastIndex();
```

## RemoveAll
Removes all occurences from a collection. It has overloads for a specific item or a predicate.
```c#
collection.RemoveAll(item);

collection.RemoveAll(x => x.Name.StartsWith("Bogus"));
```

## RemoveFirst, TryRemoveFirst, RemoveLast and TryRemoveLast
Removes the first or last item in the collection according to the item or predicate specified. Overloads without `Try` in their name will throw exceptions if nothing is removed.

```c#
//Will remove the first item it finds
collection.RemoveFirst(item);

//Or the last
collection.RemoveLast(item);

collection.RemoveFirst(x => x.Id > 2);

collection.TryRemoveLast(x => x.Age == 24);
```

## SequenceEqualOrNull
Behaves like `SequenceEqual` with the exception that it checks for nulls. In other words, it will not throw exceptions if either or both collections are null. This is especially useful when overloading equality operators where you want to check sequence equality rather than reference equality.

```c#
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

```c#
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

```c#
//Swaps the items at index 3 and 8
collection.Swap(3, 8);
```

## TryGetFirst, TryGetLast and TryGetSingle
These work like LINQ's `FirstOrDefault`, `LastOrDefault` and `SingleOrDefault` except that you're given a `TryGetResult<T>` instead of `T?`. The issue with `T?` is that there is no clear way of knowing whether the method returned `default(T)` because it could not be found or because that's what was found. `TryGetResult<T>` makes it clear with its `IsSuccess` property.

```c#
//They are used exactly like their `OrDefault` counterparts
var item = collection.TryGetFirst(x => x.Name == "Seb");

//This is also allowed in case you expect your collection to have exactly one (or no) item
var item = collection.TryGetSingle();
```

## Uniform and UniformOrDefault
Validates that all entries in the collection are "duplicates" (or that the collection is "uniform") and returns any of those entries. It's basically a cleaner alternative to using `First` or `FirstOrDefault` in cases where you have more than one entry in the collection but you don't care which entry is returned. What's wrong with `First` and `FirstOrDefault`, you say? It should really only be used when your intent is to actually get the first element in the collection. `Single` or `SingleOrDefault` should be used instead when there's not supposed to be more than one entry. But sometimes, you just get a garbage collection that you have little to no control over and you just need whichever duplicate entry.

It can also validate that all elements in the list have the same value for a specified property if used with a lambda. `All` could be used but `Uniform` returns the "first" of those elements as well.

```c#
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

```c#
//Returns the item with "Roger" for a middle name if all items have the same value or throws
var item = collection.UniformBy(x => x.MiddleName == "Roger");

//Returns the item with "Roger" for a middle name if all items have the same value or default
var item = collection.UniformByOrDefault(x => x.MiddleName == "Roger");
```