# 🧩 Reference&lt;T&gt;

A **serialized reference wrapper** for a value of type `T`. This class is useful when you want
to **wrap a value** so it can be **serialized, displayed in inspectors**, or **passed by reference safely**.

> [!TIP]
> It can also be used as a lightweight shared reference for multiple objects, allowing them to access and modify the
> same value instance without duplicating data.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
    - [Basic Usage](#ex1)
    - [Shared Reference](#ex2)
    - [Result for Coroutines](#ex3)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Properties](#-properties)
        - [Value](#value)
    - [Constructors](#-constructors)
        - [Reference(T)](#referencet)
    - [Operators](#-operators)
        - [Reference<T>(T)](#operator-referencett)
- [Notes](#-notes)

---

## 🗂 Example of Usage

<div id="ex1"></div>

### 1️⃣ Basic Usage

```csharp
var health = new Reference<int>(100);

// Accessing the value by reference
ref int healthRef = ref health.Value;
healthRef += 50;

Console.WriteLine(health.Value); // Output: 150
```

---

<div id="ex2"></div>

### 2️⃣ Shared Reference

Multiple objects can safely share a single `Reference<T>` instance. Modifications from any object are reflected in all
objects referencing the same instance.

```csharp
public class Player
{
    private Reference<int> scoreRef;

    public Player(Reference<int> sharedScore)
    {
        scoreRef = sharedScore;
    }

    public void AddScore(int amount)
    {
        ref int score = ref scoreRef.Value;
        score += amount;
    }
}
```

```csharp
public class Example : MonoBehaviour
{
    private readonly Reference<int> sharedScore = 0;

    private void Awake()
    {
        var player1 = new Player(sharedScore);
        var player2 = new Player(sharedScore);

        player1.AddScore(5);
        player2.AddScore(10);

        Debug.Log($"Shared score: {sharedScore.Value}"); // Outputs 15
    }
}
```

---

<div id="ex3"></div>

### 3️⃣ Result for Coroutines

The reference can also serve as a lightweight container for `out` parameters in Unity coroutines or asynchronous tasks.
This allows coroutines or async methods to update a value that the caller can access after the operation completes.

```csharp
public class Example : MonoBehaviour
{
    private Reference<int> result = new();

    private void Start()
    {
        StartCoroutine(CalculateRoutine(result));
    }

    private IEnumerator CalculateRoutine(Reference<int> output)
    {
        yield return new WaitForSeconds(2f);
        output.Value = 42; // Set the result
    }
}
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[Serializable]
public class Reference<T>
```

- **Type Parameter:** `T` — The type of the value being referenced.
- **Note:** Supports Unity serialization and Odin Inspector

---

<div id="-constructors"></div>

### 🏗️ Constructors

#### `Reference(T)`

```csharp
public Reference(T value = default);
```

- **Description:** Initializes a new instance of the `Reference<T>` class.
- **Parameter:** `value` — The initial value to wrap. Defaults to `default(T)`.

---

### 🔑 Properties

#### `Value`

```csharp
public ref T Value { get; }
```

- **Description:** Provides a **reference** to the wrapped value.
- **Remarks:** Modifying this reference will update the underlying value directly.

---

### 🪄 Operators

#### `operator Reference<T>(T)`

```csharp
public static implicit operator Reference<T>(T value);
```

- **Description:** Allows seamless conversion from a raw value of type `T` to a `Reference<T>`.
- **Parameter:** `value` — The value to wrap inside a `Reference<T>`.
- **Returns:** A new `Reference<T>` instance containing the provided value.
- **Remarks:** Useful for writing cleaner code without explicitly creating a `Reference<T>` instance.

---

## 📝 Notes

- Wraps a value of type `T` for serialization.
- Provides a `ref` accessor to the wrapped value for direct modification.
- Can act as a lightweight shared reference for multiple objects.
- Compatible with Unity serialization and optionally Odin Inspector.