# 🧩 Collections

Provide a set of **reactive collection types** such as arrays, lists, dictionaries, and sets. These collections
automatically notify subscribers of changes, making them ideal for **data binding, UI updates, and reactive
programming**. Both read-only and fully mutable reactive collections are supported, allowing fine-grained control over
data access and modification.

- Base Interfaces
  - [IReadOnlyReactiveCollection](IReadOnlyReactiveCollection.md) <!-- + -->
  - [IReactiveCollection](IReactiveCollection.md) <!-- + -->
- ReactiveArrays
  - [IReadOnlyReactiveArray](IReadOnlyReactiveArray.md) <!-- + -->
  - [IReactiveArray](IReactiveArray.md) <!-- + -->
  - [ReactiveArray](ReactiveArray.md) <!-- + -->
- ReactiveLists
  - [IReadOnlyReactiveList](IReadOnlyReactiveList.md) <!-- + -->
  - [IReactiveList](IReactiveList.md)
  - [ReactiveList](ReactiveList.md)
  - [ReactiveLinkedList](ReactiveLinkedList.md)
- ReactiveDictionaries
  - [IReadOnlyReactiveDictionary](IReadOnlyReactiveDictionary.md)
  - [IReactiveDictionary](IReactiveDictionary.md)
  - [ReactiveDictionary](ReactiveDictionary.md)
- ReactiveSets
  - [IReactiveSet](IReactiveHashSet.md)
  - [ReactiveHashSet](ReactiveHashSet.md)
