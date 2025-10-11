# 🧩 InlineActions

The **InlineAction** classes provide wrappers around standard `System.Action` delegates.
They implement the corresponding [IAction](IActions.md) interfaces and allow invoking actions directly, optionally with
parameters.

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
IAction action = new InlineAction(() => Console.WriteLine("Hello World!"));
action.Invoke(); // Output: Hello World!
```

---

### 2️⃣ Action with one parameter <div id="action-with-one-parameter"></div>

```csharp
IAction<GameObject> destroyAction = new InlineAction<GameObject>(GameObject.Destroy);
destroyAction.Invoke(gameObject);
```

---

### 3️⃣ Action with two parameters <div id="action-with-two-parameters"></div>

```csharp
IAction<Character, int> damageAction = new InlineAction<Character, int>(
    (character, damage) => character.TakeDamage(damage)
);

damageAction.Invoke(enemy, 5);
```

---

## 🔍 API Reference

There are several implementations of inline actions, depending on the number of arguments the actions take:

- [InlineAction](InlineAction.md) — Non-generic version; works without parameters.
- [InlineAction&lt;T&gt;](InlineAction%601.md) — Inline action that takes one argument.
- [InlineAction&lt;T1, T2&gt;](InlineAction%602.md) — Inline action that takes two arguments.
- [InlineAction&lt;T1, T2, T3&gt;](InlineAction%603.md) — Inline action that takes three arguments.
- [InlineAction&lt;T1, T2, T3, T4&gt;](InlineAction%604.md) — Inline action that takes four arguments.