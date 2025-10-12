# 🧩 InlineSignal

Represents a signal that can notify subscribers of events <b>without passing any data</b>.

---

## 📑 Table of Contents

- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructors](#-constructors)
        - [InlineSignal(Action\<Action>, Action\<Action>)](#inlinesignalactionaction-actionaction)
    - [Events](#-events)
        - [OnEvent](#onevent)

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class InlineSignal : ISignal
```

- **Description:** Represents a signal that can notify subscribers of events <b>without passing any data</b>.
- **Inheritance:** [ISignal](ISignal.md)

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `InlineSignal(Action<Action>, Action<Action>)`

```csharp
public InlineSignal(Action<Action> subscribe, Action<Action> unsubscribe)
```

- **Description:** Initializes a new instance with provided subscription and unsubscription delegates.
- **Parameters:**
    - `subscribe` — Action handling subscription logic
    - `unsubscribe` — Action handling unsubscription logic
- **Throws:** `ArgumentNullException` if `subscribe` or `unsubscribe` is null.

---

### ⚡ Events

#### `OnEvent`

```csharp
public event Action OnEvent;
```

- **Description:** Occurs when the signal is emitted with single argument