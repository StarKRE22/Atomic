# 📌 Iterating over Reactive Collections

When using reactive collections such
as [ReactiveArray](../Elements/Collections/ReactiveArray.md), [ReactiveList](../Elements/Collections/ReactiveList.md), [ReactiveLinkedList](../Elements/Collections/ReactiveLinkedList.md), [ReactiveHashSet](../Elements/Collections/ReactiveHashSet.md),
and [ReactiveDictionary](../Elements/Collections/ReactiveDictionary.md), it is important to understand that **each
reactive wrapper introduces a small overhead**. Knowing how to **optimize performance** becomes crucial when working
with a large number of elements.

---

### 1. Prefer concrete types

When iterating via interfaces using `IEnumerator<T>`, the iterator may **box**, causing **heap allocations** and **GC
pressure**. To avoid this, use the **concrete collection type** whenever possible.

#### ❌ Has Boxing

```csharp
IReactiveList<string> items = new ReactiveList<string>
{
    "Axe", 
    "Helmet",
    "Potion",
    "Sword"
} 

//Reference type of "IEnumerator<string>"
foreach(string item in items)
{
    ...
}
```

#### ✅ No Boxing

```csharp
ReactiveList<string> items = new ReactiveList<string>
{
    "Axe", 
    "Helmet",
    "Potion",
    "Sword"
} 

//Value type of "ReactiveList.Enumerator<string>"
foreach(string item in items) 
{
    ...
}
```

---

### 2. Iterating over `ReactiveArray` & `ReactiveList`

When iterating over a large number of elements in a `ReactiveArray` or `ReactiveList`, **always prefer a `for` loop
over `foreach`**. Using `foreach` involves additional operations, including **struct enumerator allocation on the stack
**, which can slightly slow down iteration.

> [!NOTE]  
> Performance tests confirm this behavior for both [ReactiveList](../Elements/Performance/ReactiveListPerformance.md)
> and [ReactiveArray](../Elements/Performance/ReactiveArrayPerformance.md).

#### ❌ Bad Practice

```csharp
ReactiveList<string> items = ... //1000+ elements

foreach(string item in items)
{
    ...
}
```

#### ✅ Good Practice

```csharp
ReactiveList<string> items = ... //1000+ elements

for (int i = 0, count = items.Count; i < count; i++)
{
    string item = items[i];
    ...
}
```

> [!TIP]
> It's also recommended to **cache the `Count` property** to avoid repeatedly calling the getter during iteration.

---

### 3. Iterating over `ReactiveLinkedList`, `ReactiveDictionary`, `ReactiveHashSet`

In contrast to arrays and lists, **always prefer `foreach` over `for`** when iterating over `ReactiveLinkedList`,
`ReactiveDictionary`, or `ReactiveHashSet`. This is especially important for `ReactiveLinkedList`, which is a *
*doubly-linked list** where accessing elements by index has **O(N) complexity**. Using a `for` loop with indexing would
therefore be very inefficient.

#### ❌ Bad Practice

```csharp
ReactiveLinkedList<string> items = ... //1000+ elements

for (int i = 0, count = items.Count; i < count; i++)
{
    string item = items[i];
    ...
}
```

#### ✅ Good Practice

```csharp
ReactiveLinkedList<string> items = ... //1000+ elements

foreach(string item in items)
{
    ...
}
```

> [!NOTE]  
> For detailed `performance` analysis, see the following sections:
> - [ReactiveLinkedList](../Elements/Performance/ReactiveLinkedListPerformance.md)
> - [ReactiveDictionary](../Elements/Performance/ReactiveDictionaryPerformance.md)
> - [ReactiveHashSet](../Elements/Performance/ReactiveHashSetPerformance.md)
