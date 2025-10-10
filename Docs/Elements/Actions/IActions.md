# 🧩 IActions

Define a family of fundamental contracts for executing parameterized actions. They provide a lightweight
abstraction for invoking logic, often used in game mechanics and command patterns.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Non-generic action](#non-generic-action)
    - [Action with one parameter](#action-with-one-parameter)
    - [Action with two parameters](#action-with-two-parameters)
- [API Reference](#-api-reference)

---

## 🗂 Examples of Usage

Below are examples of using `IAction` family interface:

### 1️⃣ Non-generic action <div id="non-generic-action"></div>

```csharp
public sealed class HelloWorldAction : IAction
{
    public void Invoke() 
    {
        Console.WriteLine("Hello World!");  
    } 
}
```

---

### 2️⃣ Action with one parameter <div id="action-with-one-parameter"></div>

```csharp
public sealed class DestroyGameObjectAction : IAction<GameObject>
{
    public void Invoke(GameObject go) 
    {
        GameObject.Destroy(go);  
    } 
}
```

---

### 3️⃣ Action with two parameters <div id="action-with-two-parameters"></div>

```csharp
public sealed class DealDamageAction : IAction<Character, int>
{
    public void Invoke(Character character, int damage) 
    {
        character.TakeDamage(damage);
    } 
}
```

----

## 🔍 API Reference

There are several interfaces of actions, depending on the number of arguments the actions take:

- [IAction](IAction.md) — Non-generic version; works without parameters.
- [IAction&lt;T&gt;](IAction%601.md) — Action that takes one argument.
- [IAction&lt;T1, T2&gt;](IAction%602.md) — Action that takes two arguments.
- [IAction&lt;T1, T2, T3&gt;](IAction%603.md) — Action that takes three arguments.
- [IAction&lt;T1, T2, T3, T4&gt;](IAction%604.md) — Action that takes four arguments.