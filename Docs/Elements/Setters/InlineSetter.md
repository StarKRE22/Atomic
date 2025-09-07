# 🧩 InlineSetter Class

The **InlineSetter<T>** class provides a concrete implementation of the `ISetter<T>` interface.  
It wraps an `Action<T>` delegate to allow values to be set through a callback, making it suitable for flexible, inline assignment logic.

## Key Features
- **Delegate-Based Assignment** – Uses an `Action<T>` to handle value assignment.
- **Integration with ISetter<T>** – Implements the `ISetter<T>` interface for unified setter usage.
- **Serialization Support** – Marked as `[Serializable]`, allowing storage in Unity or other serializable systems.
- **Editor Integration** – Optionally supports `[Button]` in Odin Inspector for manual invocation in the Unity Editor.

---

## InlineSetter<T>

```csharp
public class InlineSetter<T> : ISetter<T>
{
    public T Value { set; }
    private readonly Action<T> action;

    public InlineSetter(Action<T> action);
}
```
## Members
- **Value** – Invokes the internal action with the assigned value.
- **Constructor** – `InlineSetter(Action<T> action)` initializes the setter with a delegate. Throws `ArgumentNullException` if `action` is null.
- **Editor_SetValue** – (Editor only) method to invoke the setter manually in the Unity Editor (with Odin Inspector `[Button]` attribute).

## Notes
- **Null Safety** – The constructor ensures the `Action<T>` delegate is not null.
- **Unity Editor Support** – Conditional compilation provides editor-only buttons for testing setters directly.
- **Flexible Binding** – Can bind to any external target by passing a delegate.