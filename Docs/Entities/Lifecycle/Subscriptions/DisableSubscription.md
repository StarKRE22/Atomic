# 🧩 DisableSubscription

Represents a disposable subscription handle for
an [IEnableLifecycle's](../Sources/IEnableLifecycle.md) **OnDisabled** event. Automatically unsubscribes the callback
when disposed, ensuring safe event management and preventing repeated invocations.

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
          <li><a href="#disablesubscriptionienablelifecycle-action">DisableSubscription(IEnableLifecycle, Action)</a></li>
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
 //Assume we have an instance of IEnableLifecycle
IEnableLifecycle enableSource = ...;

// Subscribe to the disable event
var subscription = new DisableSubscription(enableSource, () => 
    Console.WriteLine("Source has been disabled"));

// Disabling source triggers the subscription
enableSource.Disable();

// Later, we can unsubscribe from the source
subscription.Dispose();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public readonly struct DisableSubscription : IDisposable
```

- **Inheritance:** `IDisposable`

---

<div id="ctor"></div>

### 🏗️ Constructors

#### `DisableSubscription(IEnableLifecycle, Action)`

```csharp
public DisableSubscription(IEnableLifecycle source, Action callback)
```

- **Description:** Subscribes the provided callback to the `OnDisabled` event of the given source.
- **Parameters:**
    - `source` — The activatable source to subscribe to.
    - `callback` — The callback action invoked when the source is disabled.

---

### 🏹 Methods

#### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Detaches the callback from the `OnDisabled` event.
- **Remarks:** Safe to call multiple times; should be invoked when the subscription is no longer needed.