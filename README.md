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
    
If the key were not associated with the dictionary, a new `List<string>` would be re-created and the words would be lost. If you would like to test for a key without modifying the dictionary, you can use the `ContainsKey` or the `TryGetValue` methods. Notice that this modifying behavior doesn't apply to read-only dictionaries.

The `TryGetValue` method in the `Dictionary` class will set the `out` variable to `default(TValue)`. However, the `TryGetValue` method in the `DefaultDictionary` class will set the `out` variable to the result of the generator. Since `TryGetValue` does not modify the dictionary, the generator will get called each time for a missing key.

## OrderedDictionary
.NET provides a non-generic `OrderedDictionary` class in the `System.Collections.Specialized` namespace. I have created a type-safe version.

`OrderedDictionary` should not be confused with a `SortedDictionary`. An `OrderedDictionary` is simply a dictionary that remembers the order that its items were added. This is useful for representing things like parameter lists. Parameters have names, but they also appear in a particular order.

The generic `OrderedDictionary` has various advantages over the non-generic version. First, it has better runtime because of less conversions and it uses less memory. Second, it implements the `IList<T>` interface which means you can treat it like a `List<KeyValuePair<TKey, TValue>>`. Finally, it allows you to quickly retrieve the key at a particular index (which you can't do with the non-generic version for some unknown reason).

The only slow operation on an `OrderedDictionary` is finding the index of a key, which requires a linear search. Just be wary of operations that involve finding or removing an item by its key.

## PropertyDictionary
Many new libraries allow you to pass anonymous classes for configuration settings. For instance, ASP.NET MVC will allow you to pass an `HtmlAttributes` object - any of the properties you provide will become attribues in the final HTML tag. For example:

    @Html.TextBoxFor(model => model.FirstName, new { id = "txtFirstName", @class = "input" });
    
This will render a tag like this:

    <input name="FirstName" type="text" value="Bob" id="txtFirstName" class="input" />
    
As you can see, the properties in the anonymous class are used to build the tag.

Using anonymous classes like this will most likely become a common technique for designing flexible libraries. The problem for library designers is that they will need to use reflection (`System.Reflection`) in order inspect the anonymous object being passed to their code.

The `PropertyDictionary` class provides an easy way to inspect objects by creating a read-only dictionary whose keys are the property names and whose values are the property values. So, given the anonymous object from the example above, we could build our tag like this:

    var inspector = new PropertyDictionary(htmlAttributes);
    List<string> attributes = new List<string>(inspector.Count);
    foreach (KeyValuePair<string, string> pair in inspector)
    {
        string attribute = pair.Key + "=" + pair.Value;
        attributes.Add(attribute);
    }
    string attributesString = String.Join(" ", attributes);
    
The other approach would involve grabbing the `Type` of the object, requesting its properties via `GetProperties`, looping through the properties and calling `Name` and `GetValue` for each. Not only that, but you'd have to deal with inherited properties and special properties, like indexers.

Since `PropertyDictionary` is just a `Dictionary`, you can also check to see if properties are provided (`ContainsKey`) and use default values if they are missing. `PropertyDictionary` works with the `DefaultDictionary` class.

## ReadOnlyDictionary and ReadOnlySet
In the `System.Collections.ObjectModel` namespace, there is a `ReadOnlyCollection<T>` class for creating a read-only wrapper for `T[]`, `List<T>`, `Collection<T>`, etc. However, there are no complimentary classes for `ISet<T>` or `IDictionary<TKey, TValue>`.

With .NET 4.5, there are new interfaces for [read-only collections](http://visualstudiomagazine.com/articles/2012/08/07/new-read-only-collection-interfaces-for-net.aspx). These will technically perform better if they are available.

The `ReadOnlySet` and `ReadOnlyDictionary` classes are available if you don't have access to the new interfaces.
