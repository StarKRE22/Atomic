# 🧩 IAction&lt;T1, T2, T3&gt;

Represents an executable action that <b>takes three arguments</b>.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Invoke(T1, T2, T3)](#invoket1-t2-t3)

---

## 🗂 Example of Usage

Below is an example of moving resources between storages using `IAction<T1, T2, T2>`

```csharp
public sealed class MoveResourcesAction : IAction<Storage, Storage, int>
{
    public void Invoke(Storage source, Storage destination, int amount)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (destination == null) throw new ArgumentNullException(nameof(destination));
        if (amount <= 0) throw new ArgumentException("Amount of resources must be greater than zero.", nameof(amount));
        if (source.AvailableResources < amount) throw new InvalidOperationException("Source does not have enough resources.");
        if (destination.FreeCapacity < amount) throw new InvalidOperationException("Destination does not have enough free capacity.");

        source.SpendResources(amount);
        destination.EarnResources(amount);
    }
}

```

Usage:

```csharp
// Assume we have two resource storages
Storage storageA, storageB = ...

// Usage
IAction<Storage, Storage, int> action = new MoveResourcesAction();
action.Invoke(storageA, storageB, 100);
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IAction<in T1, in T2, in T3>
```

- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument

---

### 🏹 Methods

#### `Invoke(T1, T2, T3)`

```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3);
```

- **Description:** Executes the action with the specified arguments
- **Parameters:**
    - `arg1` — the first argument
    - `arg2` — the second argument
    - `arg3` — the third argument