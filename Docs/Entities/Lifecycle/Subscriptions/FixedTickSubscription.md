# 🧩 FixedTickSubscription

Represents a disposable subscription handle for an [ITickLifecycle's](../Sources/ITickLifecycle.md)
**OnFixedTicked** event. Automatically unsubscribes the callback when disposed, ensuring safe handling of physics or
fixed-timestep updates.

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
          <li><a href="#fixedticksubscriptioniticklifecycle-actionfloat">FixedTickSubscription(ITickLifecycle, Action&lt;float&gt;)</a></li>
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

// Subscribe to the FixedUpdate event
var subscription = new FixedTickSubscription(tickSource, deltaTime => 
    Console.WriteLine($"FixedUpdate tick: {deltaTime:F3}s"));

// Trigger a FixedUpdate — subscription callback will fire
tickSource.FixedTick(0.02f);

// Later, we can unsubscribe from the source
subscription.Dispose();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public readonly struct FixedTickSubscription : IDisposable
```

- **Inheritance:** `IDisposable`

---

<div id="ctor"></div>

### 🏗️ Constructors

#### `FixedTickSubscription(ITickLifecycle, Action<float>)`

```csharp
public FixedTickSubscription(ITickLifecycle source, Action<float> callback)
```

- **Description:** Subscribes the provided callback to the `OnFixedTicked` event of the given source.
- **Parameters:**
    - `source` — The updatable source to subscribe to.
    - `callback` — The callback action invoked during the FixedUpdate phase.

---

### 🏹 Methods

#### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Detaches the callback from the `OnFixedTicked` event.
- **Remarks:** Safe to call multiple times; should be invoked when the subscription is no longer needed.