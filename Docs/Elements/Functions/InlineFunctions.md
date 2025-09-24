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
easy to define custom behavior inline for values, computations, reactive systems and tests.

Below is a demonstration of how to dynamically provide values for different types of transformations in
`AtomicEntities`:

### Setting transform via `Rigidbody`

```csharp
public class PhysicsTransformInstaller : SceneEntityInstaller
{
    [SerializeField]
    private Rigidbody _rigidbody;

    public void Install(IEntity entity)
    {
        entity.AddPosition(new InlineFunction<Vector3>(() => _rigidbody.position));
        entity.AddRotation(new InlineFunction<Quaternion>(() => _rigidbody.rotation));
    }
}
```

### Setting transform via `Transform`

```csharp
public class KinematicTransformInstaller : SceneEntityInstaller
{
    [SerializeField]
    private Transform _transform;

    public void Install(IEntity entity)
    {
        entity.AddPosition(new InlineFunction<Vector3>(() => _transform.position));
        entity.AddRotation(new InlineFunction<Quaternion>(() => _transform.rotation));
    }
}
```

### Usage in code

```csharp
// Now it doesn’t matter where the object’s coordinates come from — it’s abstracted away
IFunction<Vector3> position = entity.GetPosition();
IFunction<Quaternion> rotation = entity.GetRotation();
```