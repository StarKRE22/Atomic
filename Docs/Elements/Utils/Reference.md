# 🧩 Reference<T>

`Reference<T>` is a serialized wrapper for a value of type `T`.  
It allows you to store and reference a value in a way that Unity can serialize, while still providing direct access to the underlying value via a reference.

It can also be used as a lightweight shared reference for multiple objects, allowing them to access and modify the same value instance without duplicating data.

---

## Overview

- Wraps a value of type `T` for serialization.
- Provides a `ref` accessor to the wrapped value for direct modification.
- Can act as a lightweight shared reference for multiple objects.
- Compatible with Unity serialization and optionally Odin Inspector.

---

## Type Parameters

- `T` — The type of the value being wrapped.

---

## Properties

### `ref T Value { get; }`

Provides a reference to the wrapped value, allowing read and write access directly.

---

## Constructor

### `Reference(T value = default)`

Initializes a new instance of `Reference<T>` with an optional initial value.

- **Parameters:**  
  `value` — the initial value to wrap (default is `default(T)`).

---

## Example Usage

### Single Object Reference

```csharp
using Atomic.Elements;
using UnityEngine;

public class ReferenceExample : MonoBehaviour
{
    [SerializeField]
    private Reference<int> health = new Reference<int>(100);

    private void Start()
    {
        // Access and modify the value directly
        ref int currentHealth = ref health.Value;
        currentHealth -= 10;
        
        Debug.Log($"Current health: {health.Value}");
    }
}
```
### Shared Reference Across Multiple Objects
```csharp
using Atomic.Elements;
using UnityEngine;

public class SharedReferenceExample : MonoBehaviour
{
    private Reference<int> sharedScore = new Reference<int>(0);

    private void Awake()
    {
        var player1 = new Player(sharedScore);
        var player2 = new Player(sharedScore);

        player1.AddScore(5);
        player2.AddScore(10);

        Debug.Log($"Shared score: {sharedScore.Value}"); // Outputs 15
    }
}

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

- Multiple objects can safely share a single `Reference<T>` instance.
- Modifications from any object are reflected in all objects referencing the same instance.

## Example Usage with Coroutines
`Reference<T>` can also serve as a lightweight container for "out" parameters in Unity coroutines or asynchronous tasks.  
This allows coroutines or async methods to update a value that the caller can access after the operation completes.

```csharp
using Atomic.Elements;
using UnityEngine;
using System.Collections;

public class CoroutineReferenceExample : MonoBehaviour
{
    private Reference<int> result = new Reference<int>();

    private void Start()
    {
        StartCoroutine(CalculateAsync(result));
    }

    private IEnumerator CalculateAsync(Reference<int> output)
    {
        yield return new WaitForSeconds(2f);
        output.Value = 42; // Set the result
    }
}
```