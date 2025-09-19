# 🧩 IReactiveValue&lt;T&gt;

Represents a **reactive value** that combines **read-only access** through [IValue&lt;T&gt;](IValue.md) and **reactive
observation** through [ISignal&lt;T&gt;](../Signals/ISignal.md#-isignalt). It allows you to both **read the current
value** and **subscribe to changes**.

```csharp
public interface IReactiveValue<T> : IValue<T>, ISignal<T>
```

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
- **Notes**: This is the default implementation from [IFunction&lt;R&gt;.Invoke()](../Functions/IFunction.md#invoke)

#### `Subscribe(Action)`

```csharp
public Subscription<T> Subscribe(Action action)  
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** A [Subscription&lt;T&gt;](../Signals/Subscription.md#subscriptiont) struct representing the active
  subscription.
- **Notes**: This is the default implementation
  from [ISignal&lt;T&gt;.Subscribe()](../Signals/ISignal.md#subscribeactiont)

#### `Unsubscribe(Action)`

```csharp
public void Unsubscribe(Action action)  
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.
- **Notes**: This is the default implementation
  from [ISignal&lt;T&gt;.Unsubscribe()](../Signals/ISignal.md#unsubscribeactiont)

---

## 🗂 Example of Usage

Reactive Value is useful in **reactive programming**, where you want to **observe changes** to a value while keeping it
**read-only**.

For example, rendering score text on a UI in Unity:

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

## 📌 Best Practice

It is recommended to use the [Observe](Extensions.md/#observe) extension method and store (cache) the returned
subscription handle for proper disposal or reuse.

```csharp
public sealed class ScorePresenter : IDisposable
{
    private readonly IReactiveValue<int> _score;
    private readonly Text _scoreText;
    private readonly Subscription<int> _subscription; //struct
    
    public ScorePresenter(IReactiveValue<int> score, Text scoreText)
    {
        _score = score;
        _scoreText = scoreText;

        // Subscribe and initialize UI with current value
        _subscription = _score.Observe(this.OnScoreChanged);
    }
    
    public void Dispose()
    {
        // Unsubscribe to avoid memory leaks
        _subscription.Dispose();
    }
    
    private void OnScoreChanged(int score)
    {
        // Update UI text
        _scoreText.text = "Score: " + score;
    }
}
```