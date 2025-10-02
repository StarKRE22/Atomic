# 🧩 IReactiveValue&lt;T&gt;

```csharp
public interface IReactiveValue<T> : IValue<T>, ISignal<T>
```

- **Description:** Represents a **reactive value** that combines **read-only access** and **reactive
  observation**. It allows you to both **read the current value** and **subscribe to changes**.
- **Inheritance:**  [IValue&lt;T&gt;](IValue.md), [ISignal&lt;T&gt;](../Events/ISignal%601.md)
- **Type Parameter:** `T` – The type of the value.

---

## 🔑 Properties

#### `Value`

```csharp
public T Value { get; }
```

- **Description:** Gets the current value.
- **Access:** Read-only

---

## 🏹 Methods

#### `Invoke()`

```csharp
public T Invoke()
```

- **Description:** Invokes the function and returns the value.
- **Returns:** The current value of type `T`.
- **Notes**: This is the default implementation from [IFunction&lt;R&gt;](../Functions/IFunction.md)

#### `Subscribe(Action)`

```csharp
public Subscription<T> Subscribe(Action action)  
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** A [Subscription&lt;T&gt;](../Events/Subscription%601.md) struct representing the active
  subscription.

#### `Unsubscribe(Action)`

```csharp
public void Unsubscribe(Action action)  
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.

---

## 🗂 Example of Usage

Reactive Value is useful in **reactive programming**, where you want to **observe changes** to a value while keeping it
**read-only**. For example, rendering score text on a UI in Unity:

```csharp
public sealed class ScorePresenter : IDisposable
{
    private readonly IReactiveValue<int> _score;
    private readonly Text _scoreText;
    
    public ScorePresenter(IReactiveValue<int> score, Text scoreText)
    {
        _score = score;
        _scoreText = scoreText;

        // Subscribe to reactive updates
        _score.Subscribe(this.OnScoreChanged);

        // Initialize UI with current value
        this.OnScoreChanged(_score.Value);
    }
    
    public void Dispose()
    {
        // Unsubscribe when no longer needed
        _score.Unsubscribe(this.OnScoreChanged);
    }
    
    private void OnScoreChanged(int score)
    {
        // Update UI text
        _scoreText.text = "Score: " + score;
    }
}
```
