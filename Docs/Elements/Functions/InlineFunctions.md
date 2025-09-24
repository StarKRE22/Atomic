# 🧩 InlineFunctions

The **InlineFunction** classes provide wrappers around standard `System.Func` delegates. They implement the
corresponding [IFunction](IFunctions.md) interfaces and allow invoking functions
directly, optionally with parameters.

There are several implementations of inline functions, depending on the number of arguments the actions take:

- [InlineFunction&lt;R&gt;](InlineFunction.md) — Function without parameters.
- [InlineFunction&lt;T, R&gt;](InlineFunction%601.md) — Function that takes one argument.
- [InlineFunction&lt;T1, T2, R&gt;](InlineFunction%602.md) — Function that takes two arguments.

---

## 🗂 Examples of Usage

### Function without arguments

```csharp
GameObject gameObject = ...
IFunction<bool> function = new InlineFunction<bool>(
    () => gameObject.activeSelf);

bool activeSelf = function.Invoke();
```

---

### Function with one argument

```csharp
Character player, enemy = ...
IFunction<bool> function = new InlineFunction<Character, bool>(
    other => player.Team != other.Team);

bool isEnemies = function.Invoke(enemy);
```

### Function with two arguments

```csharp
IFunction<int, int, int> function = new InlineFunction<int, int, int>(
    (a, b) => a + b);

int sum = function.Invoke(3, 4); // 7
```

---

## 📌 Best Practice

`InlineFunction` is ideal for creating functions for specific game objects using **lambda expressions**, making it
easy to define custom behavior inline for values, computations, or reactive systems.

Below is an example of using `InlineFunction` to provide dynamic values for different entities.

```csharp
//Using position and rotation from Rigidbody
var entity = new Entity("Tank");
entity.AddPosition(new InlineFunction<Vector3>(() => rigidbody.position));
entity.AddRotation(new InlineFunction<Quaternion>(() => rigidbody.rotation));
```

```csharp
//Using position and rotation from Transform
var entity = new Entity("Ship");
entity.AddPosition(new InlineFunction<Vector3>(() => transform.position));
entity.AddRotation(new InlineFunction<Quaternion>(() => transform.rotation));
```