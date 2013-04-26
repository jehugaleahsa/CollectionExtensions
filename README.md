# CollectionExtensions

Useful .NET collection classes and wrappers.

Download using NuGet: [CollectionExtensions](http://nuget.org/packages/collectionextensions)

## Purpose
.NET has a pretty extensive collection library. Still, every once in a while I've needed to build my own classes. Those that are general-purpose enough have found their way into this project. This project also have some extensions for LINQ.

## DefaultDictionary
Many times you want a dictionary that returns a default value whenever a key is missing. With `Dictionary`, a `KeyNotFoundException` is thrown. The `DefaultDictionary` class will wrap a dictionary and return a default value whenever a missing key is encountered.

A really nice feature of `DefaultDictionary` is that you can pass a generator function for creating default values when a missing key is encountered. Consider if you wanted to return a new `List` everytime you encountered an unknown key. The generator makes this possible.

An important feature of `DefaultDictionary` is that it will automatically add missing keys whenever they are first requested. It will associate the missing key with the default value returned by the generator. This is a good thing because it means you can write code like this:

    var lengthLookup = new DefaultDictionary<int, List<string>>(k => new List<string>());
    foreach (string word in getWords())
    {
        lengthLookup[word.Length].Add(word);
    }
    
If the key were not associated with the dictionary, a new `List<string>` would be re-created and the words would be lost. If you would like to test for a key without modifying the dictionary, you can use the `ContainsKey` or the `TryGetValue` methods.

The `TryGetValue` method in the `Dictionary` class will set the `out` variable to `default(TValue)`. However, the `TryGetValue` method in the `DefaultDictionary` class will set the `out` variable to the result of the generator. Since `TryGetValue` does not modify the dictionary, the generator will get called each time for a missing key.
