![OPEX](https://github.com/Moreault/OPEX/blob/master/opex.png)
# OPEX
OverPowered Enumerable eXtensions - _It makes your collections OP AF!_

Adds the following methods (and more) to basic .NET collections :

- IsNullOrEmpty : So you never have to check both all the time!
- RemoveAll : Don't just want to remove the first item? We got you!
- IndexesOf : Ever wanted to get all indexes of an occurence inside your collection? We got you again!
- Swap : Want to swap two indexes together? list.Swap(5, 1)! WE TOTALLY GOT YOU!
- Uniform & UniformBy : Similar to First() and Single() except that this one expects a collection or properties to all be equal so it doesn't matter which one you get.
- TryGetSingle : Like Single() except that it returns the result in a TryGetResult<T> which clearly indicates whether or not a "default" result is the outcome of a failure
- TryGetFirst : Like TryGetSingle() except that it only returns the first occurence
- TryGetLast : Should be self-explanatory by now
- SequenceEqualOrNull : Uses the existing `Enumerable.SequenceEqual` extension but handles null cases

OPEX even provides methods that add randomness to collections such as :

- Shuffle
- GetRandom
- GetRandomIndex
- GetManyRandoms

If you are already familiar with ToolBX.Collections then the extension methods will be natural to use for you. We like to throw a lot but we also provide "Try" overloads for the lazy devs out there.
