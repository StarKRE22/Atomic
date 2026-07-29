# 🧩 InitSubscription

Represents a disposable subscription handle for an [IInitSource's](../Sources/IInitSource.md)
**OnInitialized** event. Automatically unsubscribes the callback when disposed, preventing memory leaks and repeated
invocations.

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
          <li><a href="#initsubscriptionIInitSource-action">InitSubscription(IInitSource, Action)</a></li>
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
// Assume we have an instance of IInitSource
IInitSource initSource = ...;

// Subscribe to the initialization event
var subscription = new InitSubscription(initSource, () => 
    Console.WriteLine("Source has been initialized"));

// Initializing the source triggers the subscription
initSource.Init();

// Later, we can unsubscribe from the source
subscription.Dispose();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public readonly struct InitSubscription : IDisposable
```

- **Inheritance:** `IDisposable`

---

<div id="ctor"></div>

### 🏗️ Constructors

#### `InitSubscription(IInitSource, Action)`

```csharp
public InitSubscription(IInitSource source, Action callback)
```

- **Description:** Subscribes the provided callback to the `OnInitialized` event of the given source.
- **Parameters:**
    - `source` — The initialization source to subscribe to.
    - `callback` — The callback action invoked upon initialization.

---

### 🏹 Methods

#### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Detaches the callback from the `OnInitialized` event.
- **Remarks:** Safe to call multiple times; should be invoked when the subscription is no longer needed.