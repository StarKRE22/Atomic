# 🧩 Subscriptions

Represent a **subscription** to a [ISignal](ISignals.md) instance. Disposing an instance will automatically *
*unsubscribe the
associated action** from the **signal**, ensuring proper cleanup of event handlers.

There are several subscriptions, depending on the number of arguments they take:

- [Subscription](Subscription.md) — Subscription without parameters.
- [Subscription&lt;T&gt;](Subscription%601.md) — Subscription that takes one argument.
- [Subscription&lt;T1, T2&gt;](Subscription%602.md) — Subscription that takes two arguments.
- [Subscription&lt;T1, T2, T3&gt;](Subscription%603.md) — Subscription that takes three arguments.
- [Subscription&lt;T1, T2, T3, T4&gt;](Subscription%604.md) — Subscription that takes four arguments.

---

## 🗂 Examples of Usage

### 1️⃣ Non-generic Subscription

```csharp
//Assume we have a instance of ISignal
ISignal signal = ...

//Subscribe on the signal    
Subscription subscription = signal.Subscribe(lambda);

// Later, dispose to unsubscribe
subscription.Dispose();
```

---

### 2️⃣ Generic Subscription

```csharp
//Assume we have a instance of ISignal
ISignal<T> signal = ...

//Subscribe on the signal
Subscription<T> subscription = signal.Subscribe<T>(lambda);

// Later, dispose to unsubscribe
subscription.Dispose();
```