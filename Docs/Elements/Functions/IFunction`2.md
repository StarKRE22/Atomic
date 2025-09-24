
<details>
  <summary>
    <h2>🧩 IFunction&lt;T1, T2, R&gt;</h2>
    <br> Represents a function with <b>two input arguments</b> that returns a result.
  </summary>

<br>

```csharp
public interface IFunction<in T1, in T2, out R>
```

- **Type parameters:**
    - `T1` — the first input argument type
    - `T2` — the second input argument type
    - `R` — the return type

---

### 🏹 Methods

#### `Invoke(T1, T2)`

```csharp
public R Invoke(T1 arg1, T2 arg2);
```

- **Description:** Executes the function with the specified input arguments.
- **Parameters:**
    - `arg1` — the first input argument
    - `arg2` — the second input argument
- **Returns:** The result of type `R`.

---

### 🗂 Example of Usage

```csharp
public class SumFunction : IFunction<int, int, int>
{
    public int Invoke(int a, int b) => a + b;
}
```

```csharp
//Usage
IFunction<int, int, int> func = new SumFunction();
int sum = func.Invoke(3, 4); // sum = 7
```

</details>