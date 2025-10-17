# 🧩 TickSubscription

Represents a disposable subscription handle for an [ITickLifecycle's](../Sources/ITickLifecycle.md)
**OnTicked** event. Automatically unsubscribes the callback when disposed, preventing memory leaks and repeated frame
updates.

---

## 📑 Table of Contents

<ul>
  <li><a href="#-example-of-usage">Example of Usage</a></li>
  <li>
    <a href="#-api-reference">API Reference</a>
    <ul>
      <li><a href="#-type">Type</a></li>
      <li>
        <a href="#ctor">Constructors</a>
        <ul>
          <li><a href="#ticksubscriptioniticklifecycle-actionfloat">TickSubscription(ITickLifecycle, Action&lt;float&gt;)</a></li>
        </ul>
      </li>
      <li>
        <a href="#-methods">Methods</a>
        <ul>
          <li><a href="#dispose">Dispose()</a></li>
        </ul>
      </li>
    </ul>
  </li>
</ul>

---

## 🗂 Example of Usage

```csharp
// Assume we have an instance of ITickLifecycle
ITickLifecycle tickSource = ...;

// Subscribe to the regular Update event
var subscription = new TickSubscription(tickSource, deltaTime => 
    Console.WriteLine($"Update tick: {deltaTime:F3}s"));

// Trigger an Update — subscription callback will fire
tickSource.Tick(0.016f);

// Later, we can unsubscribe from the source
subscription.Dispose();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public readonly struct TickSubscription : IDisposable
```

- **Inheritance:** `IDisposable`

---
<div id="ctor"></div>

### 🏗️ Constructors

#### `TickSubscription(ITickLifecycle, Action<float>)`

```csharp
public TickSubscription(ITickLifecycle source, Action<float> callback)
```

- **Description:** Subscribes the provided callback to the `OnTicked` event of the given source.
- **Parameters:**
    - `source` — The updatable source to subscribe to.
    - `callback` — The callback action invoked every frame during the Update phase.

---

### 🏹 Methods

#### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Detaches the callback from the `OnTicked` event.
- **Remarks:** Safe to call multiple times; should be invoked when the subscription is no longer needed.