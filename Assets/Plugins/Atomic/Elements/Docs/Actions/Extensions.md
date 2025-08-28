# Extensions for IAction Collections

The **Extensions** class provides utility methods for working with collections of `IAction` instances.  
These extensions allow batch operations on multiple actions, simplifying invocation patterns.

---

## InvokeRange

Invokes all actions in an `IEnumerable<IAction>` sequence, safely skipping null actions.

```csharp
public static void InvokeRange(this IEnumerable<IAction> actions)
```

### Parameters
- **actions** – A sequence of `IAction` objects to invoke. Null actions are ignored.

### Description
- Iterates through the provided collection of actions and calls `Invoke()` on each one.
- Null checks are performed to ensure that missing actions do not cause exceptions.
- Marked with `[MethodImpl(MethodImplOptions.AggressiveInlining)]` for performance optimization.

### Example
```csharp
List<IAction> actions = new List<IAction>
{
    new InlineAction(() => Console.WriteLine("Action 1")),
    null,
    new InlineAction(() => Console.WriteLine("Action 2"))
};

// Invokes all actions in the list, skipping the null
actions.InvokeRange();

// Output:
// Action 1
// Action 2
```
### Notes
- **Null Safety** – Both the collection and individual actions are checked for null.
- **Performance** – Aggressively inlined for minimal call overhead.
- **Batch Execution** – Useful for invoking multiple callbacks, events, or commands in one operation.