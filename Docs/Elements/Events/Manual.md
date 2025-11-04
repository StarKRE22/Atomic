# 🧩 Events

Provides a set of abstractions for **events, subscriptions, and signals**. It allows developers to define, subscribe to,
and trigger events in a decoupled and reactive manner, making it ideal for event-driven architectures and real-time
systems.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Non-generic Event](#ex-1)
    - [Event with single argument](#ex-2)
    - [Non-generic Subscription](#ex-3)
    - [Generic Subscription](#ex-4)
- [API Reference](#-api-reference)
- [Best Practices](#-best-practices)

---

## 🗂 Examples of Usage

### 1️⃣ Non-generic Event <div id="ex-1"></div>

```csharp
IEvent playerDiedEvent = new Event();

// Subscribe to the event
playerDiedEvent.OnEvent += () => Console.WriteLine("Player died event triggered.");

// Invoke the event
playerDiedEvent.Invoke(); // Output: Player died event triggered.
```

### 2️⃣ Event with single argument <div id="ex-2"></div>

```csharp
IEvent<int> healthChangedEvent = new Event<int>();

// Subscribe to the event
healthChangedEvent.OnEvent += health => Console.WriteLine($"Health changed to: {health}");

// Invoke the event with a value
healthChangedEvent.Invoke(100); // Output: Health changed to: 100
healthChangedEvent.Invoke(75);  // Output: Health changed to: 75
```

### 3️⃣ Non-generic Subscription <div id="ex-3"></div>

```csharp
```csharp
// Assume we have an instance of fire event
ISignal fireEvent = ...

// Assume we have an AudioSource with fire AudioClip 
AudioSource fireSFX = ...

// Subscribe on the event    
Subscription subscription = fireEvent.Subscribe(fireSFX.Play);

// Later, dispose to unsubscribe
subscription.Dispose();
```

### 4️⃣ Generic Subscription <div id="ex-4"></div>

```csharp
//Assume we have an instance of ISignal
ISignal<T> signal = ...

//Subscribe on the signal
Subscription<T> subscription = signal.Subscribe<T>(lambda);

// Later, dispose to unsubscribe
subscription.Dispose();
```

---

## 🔍 API Reference

Below are interfaces and implementations corresponding with events and depending on the concrete scenario:  

 <details>
    <summary><a href="ISignals.md">ISignals</a></summary>
    <ul>
      <li><a href="ISignal.md">ISignal</a></li>
      <li><a href="ISignal%601.md">ISignal&lt;T&gt;</a></li>
      <li><a href="ISignal%602.md">ISignal&lt;T1, T2&gt;</a></li>
      <li><a href="ISignal%603.md">ISignal&lt;T1, T2, T3&gt;</a></li>
      <li><a href="ISignal%604.md">ISignal&lt;T1, T2, T3, T4&gt;</a></li>
    </ul>
  </details>

  <details>
    <summary><a href="InlineSignals.md">InlineSignals</a></summary>
    <ul>
      <li><a href="InlineSignal.md">InlineSignal</a></li>
      <li><a href="InlineSignal%601.md">InlineSignal&lt;T&gt;</a></li>
      <li><a href="InlineSignal%602.md">InlineSignal&lt;T1, T2&gt;</a></li>
      <li><a href="InlineSignal%603.md">InlineSignal&lt;T1, T2, T3&gt;</a></li>
      <li><a href="InlineSignal%604.md">InlineSignal&lt;T1, T2, T3, T4&gt;</a></li>
    </ul>
  </details>

  <details>
    <summary><a href="IEvents.md">IEvents</a></summary>
    <ul>
      <li><a href="IEvent.md">IEvent</a></li>
      <li><a href="IEvent%601.md">IEvent&lt;T&gt;</a></li>
      <li><a href="IEvent%602.md">IEvent&lt;T1, T2&gt;</a></li>
      <li><a href="IEvent%603.md">IEvent&lt;T1, T2, T3&gt;</a></li>
      <li><a href="IEvent%604.md">IEvent&lt;T1, T2, T3, T4&gt;</a></li>
    </ul>
  </details>

  <details>
    <summary><a href="BaseEvents.md">Events</a></summary>
    <ul>
      <li><a href="BaseEvent.md">Event</a></li>
      <li><a href="BaseEvent%601.md">Event&lt;T&gt;</a></li>
      <li><a href="BaseEvent%602.md">Event&lt;T1, T2&gt;</a></li>
      <li><a href="BaseEvent%603.md">Event&lt;T1, T2, T3&gt;</a></li>
      <li><a href="BaseEvent%604.md">Event&lt;T1, T2, T3, T4&gt;</a></li>
    </ul>
  </details>

  <details>
    <summary><a href="Subscriptions.md">Subscriptions</a></summary>
    <ul>
      <li><a href="Subscription.md">Subscription</a></li>
      <li><a href="Subscription%601.md">Subscription&lt;T&gt;</a></li>
      <li><a href="Subscription%602.md">Subscription&lt;T1, T2&gt;</a></li>
      <li><a href="Subscription%603.md">Subscription&lt;T1, T2, T3&gt;</a></li>
      <li><a href="Subscription%604.md">Subscription&lt;T1, T2, T3, T4&gt;</a></li>
    </ul>
  </details>

  <details>
    <summary>Extensions</summary>
    <ul>
      <li><a href="ExtensionsSubscribe.md">Subscribe</a></li>
      <li><a href="ExtensionsUnsubscribe.md">Unsubscribe</a></li>
      <li><a href="ExtensionsSubscribeRange.md">SubscribeRange</a></li>
      <li><a href="ExtensionsUnsubscribeRange.md">UnsubscribeRange</a></li>
    </ul>
  </details>

---

## 📌 Best Practices

- [Using Events with Entities](../../BestPractices/UsingEvents.md)