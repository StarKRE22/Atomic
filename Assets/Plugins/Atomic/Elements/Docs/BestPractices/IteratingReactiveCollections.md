# 🏆 Iterating Reactive Collections

When using reactive collections (`ReactiveArray`, `ReactiveList`, `ReactiveLinkedList`, etc.),  
it's important to understand that **each reactive wrapper introduces a small overhead**.  
For performance-critical loops, consider optimizing iteration patterns.

---

## Key Guidelines

- **Prefer concrete types**  
  When iterating via interfaces, enumerators may **box**, causing GC pressure. Use the concrete collection type whenever possible.

- **`ReactiveArray` & `ReactiveList`**  
  Use **`for` loops** for iteration. Caching the `Count` property is recommended:

```csharp
var count = reactiveList.Count;
for (int i = 0; i < count; i++)
{
    var item = reactiveList[i];
    // process item
}
```

## `ReactiveLinkedList`

- **Use `foreach` loops only**  
  Do **not** use `for` loops, as indexing is inefficient and may traverse the list repeatedly.

- **Minimize mutations during iteration**  
  Avoid adding or removing elements inside loops; instead, batch updates to reduce reactive event overhead.
