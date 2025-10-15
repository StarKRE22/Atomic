# 🧩 Subscriptions

Represent a struct **subscription** to a [ISignal](ISignals.md) instance. Disposing an instance will automatically
**unsubscribe the associated action** from the **signal**, ensuring proper cleanup of event handlers.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Non-generic Subscription](#ex-1)
    - [Generic Subscription](#ex-2)
- [API Reference](#-api-reference)

---

## 🗂 Examples of Usage

### 1️⃣ Non-generic Subscription <div id="ex-1"></div>

```csharp
//Assume we have an instance of ISignal
ISignal signal = ...
    
//Assume we have an instance of Action 
Action action = ...

//Subscribe on the signal    
Subscription subscription = new Subscription(signal, action);

// Later, dispose to unsubscribe
subscription.Dispose();
```

---

### 2️⃣ Generic Subscription <div id="ex-2"></div>

```csharp
//Assume we have an instance of ISignal
ISignal<T> signal = ...
    
//Assume we have an instance of Action 
Action<T> action = ...

//Subscribe on the signal
Subscription<T> subscription = new Subscription<T>(signal, action);

// Later, dispose to unsubscribe
subscription.Dispose();
```

---

## 🔍 API Reference

There are several subscriptions, depending on the number of arguments they take:

- [Subscription](Subscription.md) — Subscription without parameters.
- [Subscription&lt;T&gt;](Subscription%601.md) — Subscription that takes one argument.
- [Subscription&lt;T1, T2&gt;](Subscription%602.md) — Subscription that takes two arguments.
- [Subscription&lt;T1, T2, T3&gt;](Subscription%603.md) — Subscription that takes three arguments.
- [Subscription&lt;T1, T2, T3, T4&gt;](Subscription%604.md) — Subscription that takes four arguments.