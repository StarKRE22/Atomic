
### 🧩 Const<T>

`Const<T>` represents a **serialized, immutable (read-only) constant value wrapper**.  
It implements `IValue<T>` and supports **implicit conversions**, making it useful in systems where values must be serialized or treated as data sources.

---

#### Type Parameter

- `T` – The type of the wrapped constant value.

---

#### Constructors

```csharp
// Default constructor
public Const()

// Constructor with a specified value
public Const(T value)
```
- Description:
  - Const() initializes a new instance with the default value of T.
  - Const(T value) initializes a new instance with a specified constant value.

#### Properties
```csharp
T Value { get; }
```
- Description: Gets the wrapped constant value.
- Access: Read-only
#### Methods
```csharp
T Invoke()
```
