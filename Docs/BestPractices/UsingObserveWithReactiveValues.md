# 📌 Using Observe Extension for ReactiveValues

The [Observe](../Elements/Values/Extensions.md/#observe) extension method is the recommended way to **subscribe to
changes in [ReactiveValues](../Elements/Values/IReactiveValue.md)**. By caching the returned subscription handle, you
can ensure **proper disposal** and avoid memory leaks, while keeping your reactive systems clean and maintainable.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
    - [Subscribing to ReactiveValue Changes](#1-subscribing-to-reactivevalue-changes)
    - [Disposing of the Subscription](#2-disposing-of-the-subscription)
- [Conclusion](#-conclusion)
- [Benefits](#-benefits)

---

## 🗂 Example of Usage

### 1. Subscribing to ReactiveValue Changes

Below is an example of a `ScorePresenter` that observes changes in a score `ReactiveValue` and updates the UI text
automatically:

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
    
    private void OnScoreChanged(int score)
    {
        // Update UI text
        _scoreText.text = "Score: " + score;
    }
    
    public void Dispose()
    {
        // Unsubscribe from the reactive value
        _subscription.Dispose();
    }
}
```

---

### 2. Disposing of the Subscription

To **prevent memory leaks**, dispose of the subscription when the presenter or component is no longer needed:

```csharp
public void Dispose()
{
    // Unsubscribe from the reactive value
    _subscription.Dispose();
}
```

> [!TIP]
> Always store the subscription returned by `Observe` to allow **explicit disposal**.  
> This ensures that reactive callbacks do not persist beyond the lifetime of the object.

---

## 🏁 Conclusion

- The [Observe](../Elements/Values/Extensions.md/#observe) method provides a **simple, structured way** to react to changes in
  [ReactiveValues](../Elements/Values/IReactiveValue.md).
- Storing the returned [Subscription](../Elements/Events/Subscriptions.md) handle allows for **clean unsubscription**, avoiding memory leaks and unintended
  behavior.
- This approach encourages **modular, reactive architectures**, where UI components and gameplay systems can
  automatically update in response to state changes.
- Integrates seamlessly with the [Atomic.Entities](../Entities/Manual.md) framework, enabling **reactive UI, score
  tracking, stats, and other dynamic updates**.
- Using `Observe` promotes **clean, maintainable, and safe code** in reactive systems.

---

## ✅ Benefits

- Provides **automatic response** to changes in `ReactiveValue`s.
- Encourages **explicit subscription management** for better memory safety.
- Reduces **boilerplate code** for updating UI or gameplay elements.
- Supports **reactive, modular, and decoupled design patterns**.
- Improves **readability and maintainability** of reactive systems.