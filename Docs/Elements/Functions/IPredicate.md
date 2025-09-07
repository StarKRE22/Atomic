# 🧩 IPredicate

The **IPredicate** interfaces define a family of contracts for **boolean-returning functions** (predicates).  
They are specialized forms of `IFunction` that evaluate a condition and return `true` or `false`, useful in filtering, validation, and decision-making logic.

## Key Features
- **Boolean Logic** – All interfaces return a `bool` value.
- **Extensibility** – Supports 0–2 input parameters.
- **Reusability** – Useful for rule engines, filtering collections, or conditional checks.

---

## IPredicate

Represents a **parameterless predicate**.

```csharp
public interface IPredicate : IFunction<bool>
{
}
```
---
## IPredicate&lt;T&gt;
Represents a **predicate that takes one argument**.
```csharp
public interface IPredicate<in T> : IFunction<T, bool>
{
}
```
---
## IPredicate<T1, T2>
Represents a **predicate that takes two arguments**.
```csharp
public interface IPredicate<in T1, in T2> : IFunction<T1, T2, bool>
{
}
```