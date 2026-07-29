# 🧩 EnableSubscription

Represents a disposable subscription handle for
an [IEnableSource's](../Sources/IEnableSource.md) **OnEnabled** event. Automatically unsubscribes the callback
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
          <li><a href="#enablesubscriptionIEnableSource-action">EnableSubscription(IEnableSource, Action)</a></li>
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
// Assume we have an instance of IEnableSource
IEnableSource enableSource = ...;

// Subscribe to the enable event
var subscription = new EnableSubscription(enableSource, () => 
    Console.WriteLine("Source has been enabled"));

// Enabling the source triggers the subscription
enableSource.Enable();

// Later, we can unsubscribe from the source
subscription.Dispose();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public readonly struct EnableSubscription : IDisposable
```

- **Inheritance:** `IDisposable`

---

<div id="ctor"></div>

### 🏗️ Constructors

#### `EnableSubscription(IEnableSource, Action)`

```csharp
public EnableSubscription(IEnableSource source, Action callback)
```

- **Description:** Subscribes the provided callback to the `OnEnabled` event of the given source.
- **Parameters:**
    - `source` — The activatable source to subscribe to.
    - `callback` — The callback action invoked when the source is enabled.

---

### 🏹 Methods

#### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Detaches the callback from the `OnEnabled` event.
- **Remarks:** Safe to call multiple times; should be invoked when the subscription is no longer needed.