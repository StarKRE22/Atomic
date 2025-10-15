# 🧩 IReactiveValue&lt;T&gt;

Represents a **reactive value** that combines **read-only access** and **reactive
observation**. It allows you to both **read the current value** and **subscribe to changes**.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Events](#-events)
        - [OnEvent](#onevent)
    - [Properties](#-properties)
        - [Value](#value)
    - [Methods](#-methods)
        - [Invoke()](#invoke)

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

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IReactiveValue<T> : IValue<T>, ISignal<T>
```

- **Description:** Represents a **reactive value** that combines **read-only access** and **reactive
  observation**. It allows you to both **read the current value** and **subscribe to changes**.
- **Inheritance:**  [IValue&lt;T&gt;](IValue.md), [ISignal&lt;T&gt;](../Events/ISignal%601.md)
- **Type Parameter:** `T` – The type of the value.

---

### ⚡ Events

#### `OnEvent`

```csharp
public event Action<T> OnEvent;
```

- **Description:** Occurs when the value changed.
- **Parameters:** `T` — the emitted value.

---

### 🔑 Properties

#### `Value`

```csharp
public T Value { get; }
```

- **Description:** Gets the current value.
- **Access:** Read-only

---

### 🏹 Methods

#### `Invoke()`

```csharp
public T Invoke()
```

- **Description:** Invokes the function and returns the value.
- **Returns:** The current value of type `T`.
- **Notes**: This is the default implementation from [IFunction&lt;R&gt;](../Functions/IFunction.md)