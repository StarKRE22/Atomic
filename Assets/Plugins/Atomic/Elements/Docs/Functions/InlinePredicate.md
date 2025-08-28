#  🧩 InlinePredicate Classes

The **InlinePredicate** classes provide a lightweight and serializable way to wrap boolean-returning delegates (`Func`) into reusable objects.  
They extend the `InlineFunction` base classes and implement the corresponding `IPredicate` interfaces, making them useful for filtering, validation, and decision-making logic.

## Key Features
- **Boolean Evaluation** – Designed specifically for `bool` results.
- **Serialization Support** – Can be stored and used as serializable fields.
- **Extensibility** – Supports predicates with zero, one, or two input parameters.
- **Compatibility** – Implements `IPredicate` interfaces for consistent usage.

---

## InlinePredicate

Represents a **parameterless predicate**.

```csharp
public class InlinePredicate : InlineFunction<bool>, IPredicate
{
    public InlinePredicate(Func<bool> func);
}
```
### Members
- **Constructor** – Initializes the class with a `Func<bool>` delegate.
---
## InlinePredicate&lt;T&gt;
Represents a **predicate with one input parameter**.
```csharp
public class InlinePredicate<T> : InlineFunction<T, bool>, IPredicate<T>
{
    public InlinePredicate(Func<T, bool> func);
}
```
### Members
- **Constructor** – Initializes the class with a `Func<T, bool>` delegate.
---
## InlinePredicate<T1, T2>
Represents a **predicate with two input parameters**.
```csharp
public class InlinePredicate<T1, T2> : InlineFunction<T1, T2, bool>, IPredicate<T1, T2>
{
    public InlinePredicate(Func<T1, T2, bool> func);
}
```
### Members
- **Constructor** – Initializes the class with a `Func<T1, T2, bool>` delegate.

# Notes
- **Exception Safety** – Constructors throw `ArgumentNullException` if a null delegate is provided.
- **Usage Context** – Can be used in collections, rule engines, pipelines, or other scenarios where conditions need to be evaluated.


### Example of Usage
Procedural polymorphism and composition over inheritance

```csharp
var tank = new Entity("Tank");
tank.AddValue<IPredicate>("CanMove",
    new InlinePredicate(() => ArmorExists(tank) && FuelExists(tank));
);

var soldier = new Entity("Soldier");
soldier.AddValue<IPredicate>("CanMove",
    new InlinePredicate(() => HealthExists(soldier));
);

// Invoke functions
tank.GetValue<IFunction<bool>>("CanMove").Invoke();
soldier.GetValue<IFunction<bool>>("CanMove").Invoke();
```