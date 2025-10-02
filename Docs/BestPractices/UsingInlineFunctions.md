# 📌 Using Inline Functions

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