
<details>
  <summary>
    <h2>🧩 IAction&lt;T1, T2, T3&gt;</h2>
    <br> Represents an executable action that <b>takes three arguments</b>.
  </summary>

<br>

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
void Invoke(T1 arg1, T2 arg2, T3 arg3);
```

- **Description:** Executes the action with the specified arguments
- **Parameters:**
    - `arg1` — the first argument
    - `arg2` — the second argument
    - `arg3` — the third argument

---

### 🗂 Example of Usage

```csharp
public sealed class MoveResourcesAction : IAction<Storage, Storage, int>
{
    public void Invoke(Storage source, Storage destination, int amount)
    {
        source.SpendResources(amount);
        destination.EarnResources(amount);
    }
}

// Usage
IAction<Storage, Storage, int> action = new MoveResourcesAction();
action.Invoke(storageA, storageB, 100);
```

</details>