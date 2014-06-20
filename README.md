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

##DictionaryExtensions
There's no built-in way to compare dictionaries in .NET. The `DictionaryEquals` method will determine if two dictionaries have the same keys and values. While this sounds simple, it is fraught with danger...

Consider what should happen if you compared two dictionaries if one dictionary uses a case-sensitive key comparison and the other uses a case-insensitive key comparison. Should the key "dog" equal "DOG"? Which dictionary's key comparer should be used? `DictionaryEquals` will use the first dictionary's key comparer.

Now consider what happens if the values are user-defined types. How should values be compared? By default, `DictionaryEquals` will use the default `EqualityComparer<TValue>`. However, you can provide your own `IEqualityComparer<TValue>` to override the default behavior.

## LINQ Extensions
There are a handful of extensions to LINQ that are useful when manipulating collections *in-memory*. LINQ was designed with SQL generation in mind. Because of that, some otherwise obvious overloads were not provided because there's no translation to SQL. Nonetheless, they are useful operations. As long as you know you're working in-memory...

### CompareTo
If you want to do a lexicographical comparison of two enumerables, use `CompareTo`. It will return `-1` if the first collection has a smaller item or is shorter. It will return `1` if the second collection has a smaller item or is shorter. Otherwise, it will return `0` if the collections have the same items and are the same length.

`CompareTo` accepts an `IComparer<T>` if you need to customize how the values are compared.

### Except
LINQ's `Except` only works if both collections contain the same type. It will only match whole objects. At times, you want to remove items with a property whose value is found in another collection.

For example, say you have a list of `Product`s. The user can select which `Product`s they want to remove from the list. However, the UI only returns the primary keys of the `Product`s that should be removed. In this case, LINQ's `Except` won't work because you can only call `Except` with two lists of `Product`. You will need to first convert the primary keys into `Product`s and then call `Except`. Doing an entire database hit is wasteful if you are just managing a collection in-memory.

CollectionExtensions provides a version of `Except` for handling this use-case. It accepts a key-selector and a list of keys. It will efficiently remove any items from the source collection that have properties whose values are found in the key collection.

### ForEach
LINQ does not provide a way to perform an action for each item in a collection. The `ForEach` method will iterate over each item in a collection and peform an `Action` on it. Be careful not to overuse this method. Often a `foreach` is easier to read and understand.

### Intersect
LINQ's `Intersect` only works if both collections contain the same type. It will only match whole objects. At times, you want to remove items with a property whose value is not found in another collection.

For example, say you have a list of `Product`s. The user can select which `Product`s they want to remove from the list. However, the UI only returns the primary keys of the `Product`s that should remain. In this case, LINQ's `Intersect` won't work because you can only call `Intersect` with two lists of `Product`. You will need to first convert the primary keys into `Product`s and then call `Intersect`. Doing an entire database hit is wasteful if you are just managing a collection in-memory.

CollectionExtensions provides a version of `Intercept` for handling this use-case. It accepts a key-selector and a list of keys. It will efficiently remove any items from the source collection that have properties whose values are not found in the key collection.

### MaxByKey
LINQ's `Max` doesn't allow you to find an item whose property has the largest value. It will let you find the largest property value, but it won't give you the item it belonged to.

CollectionExtensions provides a method called `MaxByKey` for finding the item with the largest property.

### MinByKey
LINQ's `Min` doesn't allow you to find an item whose property has the smallest value. It will let you find the smallest property value, but it won't give you the item it belonged to.

CollectionExtensions provides a method called `MinByKey` for finding the item with the smallest property.

### RandomSamples
If you want to randomly select a number of items from a collection, you can use the `RandomSamples` method. If you ask for more items than are in the collection, the entire collection is returned.

### RotateLeft
If you want to create a new collection with the items from another collection rotated, you can use the `RotateLeft` method. `RotateLeft` is smart enough to handle negative shifts, which will rotate the collection to the right. It also handles shifts that are larger than the collection, performing a full rotation.

## License
If you are looking for a license, you won't find one. The software in this project is free, as in "free as air". Feel free to use my software anyway you like. Use it to build up your evil war machine, swindle old people out of their social security or crush the souls of the innocent.

I love to hear how people are using my code, so drop me a line. Feel free to contribute any enhancements or documentation you may come up with, but don't feel obligated. I just hope this code makes someone's life just a little bit easier.
