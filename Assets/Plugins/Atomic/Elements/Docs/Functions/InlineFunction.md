# 🧩 InlineFunction Classes

The **InlineFunction** classes provide a convenient way to wrap delegates (`Func`) into serializable objects that can be invoked or passed around.  
They are designed to integrate with `IFunction` interface and optionally support **Odin Inspector** attributes for enhanced editor experience.

## Key Features
- **Serialization** – Allows functions to be stored and used as serializable fields.
- **Implicit Conversion** – Easily convert from `Func` delegates to `InlineFunction`.
- **Extensibility** – Supports functions with zero, one, or two parameters.
- **Odin Inspector Support** – Optionally includes inline property and button attributes for editor usage.

# Notes
- **ODIN_INSPECTOR** – When the `ODIN_INSPECTOR` directive is enabled, additional attributes like `[InlineProperty]` and `[Button]` are applied for improved editor interaction.
- **Exception Safety** – Constructors throw `ArgumentNullException` if a null delegate is provided.
---

## InlineFunction&lt;T&gt;
Represents a **parameterless function returning a value** of type `T`.
```csharp
[Serializable]
public class InlineFunction<T> : IValue<T>
{
    private readonly Func<T> func;

    public InlineFunction(Func<T> func);
    public static implicit operator InlineFunction<T>(Func<T> value);
    public T Invoke();
}
```
### Members
- Constructor – Initializes the class with a Func<T>.
- Implicit Operator – Converts a Func<T> to an InlineFunction<T>.
- Invoke() – Invokes the function and returns the result.

## InlineFunction<T, R>
Represents a function that takes one argument and returns a result.

```csharp
[Serializable]
public class InlineFunction<T, R> : IFunction<T, R>
{
    private readonly Func<T, R> func;

    public InlineFunction(Func<T, R> func);
    public static implicit operator InlineFunction<T, R>(Func<T, R> value);
    public R Invoke(T args);
}
```
### Members
- Constructor – Initializes the class with a Func<T, R>.
- Implicit Operator – Converts a Func<T, R> to an InlineFunction<T, R>.
- Invoke(T args) – Executes the function with the given argument.

## InlineFunction<T1, T2, R>
Represents a function that takes two arguments and returns a result.
```csharp
[Serializable]
public class InlineFunction<T1, T2, R> : IFunction<T1, T2, R>
{
    private readonly Func<T1, T2, R> func;

    public InlineFunction(Func<T1, T2, R> func);
    public static implicit operator InlineFunction<T1, T2, R>(Func<T1, T2, R> value);
    public R Invoke(T1 arg1, T2 arg2);
}
```
### Members
- Constructor – Initializes the class with a Func<T1, T2, R>.
- Implicit Operator – Converts a Func<T1, T2, R> to an InlineFunction<T1, T2, R>.
- Invoke(T1 arg1, T2 arg2) – Executes the function with the specified arguments.

### Example of Usage
Procedural polymorphism and composition over inheritance

```csharp
var tank = new Entity("Tank");
tank.AddValue<IFunction<Vector3>>("Position",
    new InlineFunction<Vector3>(() => _rigidbody.position);
);

var ship = new Entity("Ship");
ship.AddValue<IFunction<Vector3>>("Position",
    new InlineFunction<Vector3>(() => _transform.position)
);

// Invoke functions
tank.GetValue<IFunction<Vector3>>("Position").Invoke(); // Returns position of Rigidbody
ship.GetValue<IFunction<Vector3>>("Position").Invoke(); // Returns position of Transform
```