# 🧩 DisposableAction

A simple **`IDisposable`** implementation that invokes a specified action when disposed.  
Useful for inline or ad-hoc cleanup logic, such as unsubscribing from events or releasing temporary resources.

---

## Overview

`DisposableAction` allows you to define a **disposable object** whose disposal logic is defined by a single `Action`.  
When `Dispose()` is called, the action provided at construction is executed.

---

## Struct Definition

```csharp
public readonly struct DisposableAction : IDisposable
```

---

## Constructor

```csharp
public DisposableAction(Action action)
```
- **Parameters:**
    - `action` — The action to invoke when `Dispose()` is called.
- **Exceptions:**
    - `ArgumentNullException` — Thrown if `action` is `null`.

---

## Methods

### Dispose
```csharp
public void Dispose()
```
- Invokes the action specified at construction time.
- Can be used in `using` statements or manually to ensure cleanup.

---

## Usage Examples

### Inline Cleanup

```csharp
var disposable = new DisposableAction(() => Console.WriteLine("Cleanup executed."));
disposable.Dispose(); // Prints: "Cleanup executed."
```

### Using `using` Statement

```csharp
using (var disposable = new DisposableAction(() => Console.WriteLine("Resource released.")))
{
    // Do something while resource is active
}   // Automatically calls Dispose() at the end of the block, prints: "Resource released."

```

### Event Unsubscription Example

```csharp
EventHandler handler = (s, e) => Console.WriteLine("Event fired.");
someEvent += handler;

using var unsub = new DisposableAction(() => someEvent -= handler);
// Event will be unsubscribed automatically when `unsub.Dispose()` is called

```
