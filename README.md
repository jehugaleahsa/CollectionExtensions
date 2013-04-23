# CollectionExtensions

Useful wrappers to the built-in .NET collection types.

Download using NuGet: [CollectionExtensions](http://nuget.org/packages/collectionextensions)

## Deprecated
I decided that there are just too many cross-cutting concerns. I have created a new project called [NDex](http://github.com/jehugaleahsa/ndex) that contains the `IList`-related classes.

I may eventually move the other code to their own projects. But, to be honest, I see very little demand for some of them. I will create projects as demand calls for them. Once I feel the important functionality of this project has been ported, I will eventually remove it.

## Overview
The CollectionExtensions library provides collections for working in .NET. These include:
* A DefaultDictionary for handling missing keys.
* A PropertyDictionary for treating an object as a read-only dictionary of its properties.
* A type-safe OrderedDictionary class to replace the System.Collections.Specialized.OrderedDictionary class.
* A PriorityQueue class.
* A ReversedList class for iterating over indexed collections in reverse.
* A TypedList class for making older, non-generic collections type-safe.
* A ReadOnlyList class for making an efficient read-only wrapper around a list.
* A ReadOnlyDictionary class for making an efficient read-only wrapper around a dictionary.
* A ReadOnlySet class for making an efficient read-only wrapper around a set.
* A Sublist for working with a segment of an indexed collection, with dozens of algorithms.
* Some useful extensions to LINQ.
