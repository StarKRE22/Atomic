# 🧩 LateTickSubscription

Represents a disposable subscription handle for an [ITickSource's](../Sources/ITickSource.md)
**OnLateTicked** event. Automatically unsubscribes the callback when disposed, ensuring safe handling of late update
logic.

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
          <li><a href="#lateticksubscriptionITickSource-actionfloat">LateTickSubscription(ITickSource, Action&lt;float&gt;)</a></li>
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
// Assume we have an instance of ITickSource
ITickSource tickSource = ...;

// Subscribe to the LateUpdate event
var subscription = new LateTickSubscription(tickSource, deltaTime => 
    Console.WriteLine($"LateUpdate tick: {deltaTime:F3}s"));

// Trigger a LateUpdate — subscription callback will fire
tickSource.LateTick(0.016f);

// Later, we can unsubscribe from the source
subscription.Dispose();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public readonly struct LateTickSubscription : IDisposable
```

- **Inheritance:** `IDisposable`

---

<div id="ctor"></div>

### 🏗️ Constructors

#### `LateTickSubscription(ITickSource, Action<float>)`

```csharp
public LateTickSubscription(ITickSource source, Action<float> callback)
```

- **Description:** Subscribes the provided callback to the `OnLateTicked` event of the given source.
- **Parameters:**
    - `source` — The updatable source to subscribe to.
    - `callback` — The callback action invoked during the LateUpdate phase.

---

### 🏹 Methods

#### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Detaches the callback from the `OnLateTicked` event.
- **Remarks:** Safe to call multiple times; should be invoked when the subscription is no longer needed.