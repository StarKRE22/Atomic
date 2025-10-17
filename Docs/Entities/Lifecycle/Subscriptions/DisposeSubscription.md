# 🧩 DisposeSubscription

Represents a disposable subscription handle for an [IInitLifecycle's](../Sources/IInitLifecycle.md)
**OnDisposed** event. Automatically unsubscribes the callback when disposed, ensuring safe cleanup and preventing memory
leaks.

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
          <li><a href="#disposesubscriptioniinitlifecycle-action">DisposeSubscription(IInitLifecycle, Action)</a></li>
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
// Assume we have an instance of IInitLifecycle
IInitLifecycle initSource = ...;

// Subscribe to the dispose event
var subscription = new DisposeSubscription(initSource, () => 
    Console.WriteLine("Source has been disposed"));

// Disposing the source triggers the subscription
initSource.Dispose();

// Later, we can unsubscribe from the source
subscription.Dispose();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public readonly struct DisposeSubscription : IDisposable
```

- **Inheritance:** `IDisposable`

---

<div id="ctor"></div>

### 🏗️ Constructors

#### `DisposeSubscription(IInitLifecycle, Action)`

```csharp
public DisposeSubscription(IInitLifecycle source, Action callback)
```

- **Description:** Subscribes the provided callback to the `OnDisposed` event of the given source.
- **Parameters:**
    - `source` — The initialization source to subscribe to.
    - `callback` — The callback action invoked upon disposal.

---

### 🏹 Methods

#### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Detaches the callback from the `OnDisposed` event.
- **Remarks:** Safe to call multiple times; should be invoked when the subscription is no longer needed.