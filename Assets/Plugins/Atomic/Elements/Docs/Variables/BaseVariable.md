# 🧩 BaseVariable<T>

`BaseVariable<T>` is a **simple serialized container** for a value of type `T`.  
It implements `IVariable<T>`, providing **read-write access** to the stored value.

---

## Type Parameter

- `T` – The type of the value to store.

---

## Properties

```csharp
T Value { get; set; }
``` 
- Description: Gets or sets the stored value.
- Access: Read-write
- Notes: Serialized in Unity when [SerializeField] is available. 

## Methods
```csharp  
T Invoke()
```
- Returns the current value.

```csharp  
public override string ToString()
```
- Returns a string representation of the stored value.

## Constructors

```csharp  
// Default constructor
public BaseVariable()

// Constructor with a specified initial value
public BaseVariable(T value)
```
- Description:
  - BaseVariable() initializes with default(T).
  - BaseVariable(T value) initializes with the specified value.

## Implicit Conversion
```csharp  
public static implicit operator BaseVariable<T>(T value)
```
- Allows assigning a plain value to a BaseVariable<T> instance directly.

## Example Usage
```csharp
using UnityEngine;
using Atomic.Elements;

public class Example : MonoBehaviour
{
    private IVariable<int> _score;

    void Start()
    {
        // Initialize a variable
        _score = new BaseVariable<int>(10);

        // Read value
        Debug.Log("Score: " + _score.Value);  // Output: 10

        // Write value
        _score.Value = 20;
        Debug.Log("Updated Score: " + _score.Value);  // Output: 20
    }
}
```