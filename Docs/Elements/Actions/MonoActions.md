# 🧩 MonoActions Abstract

Define **scene-based actions** in Unity that implement the corresponding [IAction](IActions.md) interfaces.
These abstract classes inherit from `MonoBehaviour`, allowing actions to be attached to GameObjects in a scene.
They serve as a base for **custom scene logic** and are designed to be subclassed to implement specific behavior.

> [!TIP]
> Extremely useful for cutscenes, trigger-based actions, level initialization, and similar scene-driven logic.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Non-generic action](#non-generic-action)
    - [Action with one parameter](#action-with-one-parameter)
    - [Action with two parameters](#action-with-two-parameters)
- [API Reference](#-api-reference)

---

## 🗂 Examples of Usage

### 1️⃣ Non-generic action <div id="non-generic-action"></div>

```csharp
public sealed class HelloWorldAction : MonoAction
{
    public override void Invoke() 
    {
        Console.WriteLine("Hello World!");  
    } 
}
```

---

### 2️⃣ Action with one parameter <div id="action-with-one-parameter"></div>

```csharp
public sealed class DestroyGameObjectAction : MonoAction<GameObject>
{
    public override void Invoke(GameObject go) 
    {
        GameObject.Destroy(go);  
    } 
}
```

---

### 3️⃣ Action with two parameters <div id="action-with-two-parameters"></div>

```csharp
public sealed class DealDamageAction : MonoAction<Character, int>
{
    public override void Invoke(Character character, int damage) 
    {
        character.TakeDamage(damage);
    } 
}
```

---

## 🔍 API Reference

There are several classes of abstract scene actions, depending on the number of arguments the actions take:

- [MonoAction](MonoAction.md) — Non-generic version; works without parameters.
- [MonoAction&lt;T&gt;](MonoAction%601.md) — Action that takes one argument.
- [MonoAction&lt;T1, T2&gt;](MonoAction%602.md) — Action that takes two arguments.
- [MonoAction&lt;T1, T2, T3&gt;](MonoAction%603.md) — Action that takes three arguments.
- [MonoAction&lt;T1, T2, T3, T4&gt;](MonoAction%604.md) — Action that takes four arguments.
