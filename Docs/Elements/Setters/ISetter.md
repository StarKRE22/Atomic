# 🧩 ISetter&lt;T&gt;

```csharp
public interface ISetter<in T> : IAction<T>
```

- **Description:** Defines a contract for **assigning values**. 
- **Inheritance:** [IAction&lt;T&gt;](../Actions/IAction%601.md)
- **Type Parameter:** `T` – the type of the value to be set.

---

## 🔑 Properties

#### `Value`

```csharp
public T Value { set; }
```

- **Description:** Assigns the provided value.
- **Parameter:** `value` — the new value to be set.

---

## 🏹 Methods

#### `Invoke(T)`

```csharp
public void Invoke(T arg);
```

- **Description:** Invokes the setter by assigning the provided value.
- **Parameter:** `arg` — the value to set.
- **Notes:** Default implementation comes from [IAction&lt;T&gt;](../Actions/IAction%601.md)