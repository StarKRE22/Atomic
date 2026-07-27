# 📖 Creating the Entity in Unity

In this section, we’ll walk through the complete process of creating a character entity in Unity using the Atomic source
generators. Step by step, we’ll declare a type-safe Entity API, add the data to the entity, and implement a simple
movement mechanic.

For full details on the source generators, see the [Code Generation Walkthrough](Codegeneration.md) and the
[Code Generation Manual](../CodeGeneration/Manual.md).

---

## 📑 Table of Contents

- [Creating a Character](#creating-a-character)
- [Adding Keyboard Input](#adding-keyboard-input)

---

## Creating a Character

#### Step 1. Creating a game object

In the Scene Hierarchy, right-click and choose `3D Object → Capsule` to create a new game object.

<img width="400" height="" alt="GameObject creation" src="https://github.com/user-attachments/assets/463a721f-e50d-4cb7-86be-a5d50a6bfa17" />

#### Step 2. Adding the Entity Component

In the Inspector window of the created object, go to `Atomic → Entities → Entity` to add the Entity component.

<img width="400" height="" alt="Entity component" src="../Images/EntityComponent.png" />

Make sure the following checkboxes are enabled:

- `useUnityLifecycle` — the entity updates along with the **MonoBehaviour** lifecycle.
- `installOnAwake` — the entity is constructed during the **Awake** phase.

#### Step 3. Declare the Entity API

Create a static partial class and mark it with `[GenerateEntityExtensionsAPI]`. Declare the character data as `ValueKey<>` fields:

```csharp
using Atomic.Entities;
using UnityEngine;

[GenerateEntityExtensionsAPI]
public static partial class CharacterAPI
{
    public static readonly ValueKey<IEntity, Transform> Transform = new(nameof(Transform));
    public static readonly ValueKey<IEntity, IValue<float>> MoveSpeed = new(nameof(MoveSpeed));
    public static readonly ValueKey<IEntity, IVariable<Vector3>> MoveDirection = new(nameof(MoveDirection));
}
```

After the first compilation, the generator creates extension methods such as:

```csharp
entity.AddTransform(transform);
entity.AddMoveSpeed(moveSpeed);
entity.AddMoveDirection(moveDirection);

Transform t = entity.GetTransform();
IValue<float> speed = entity.GetMoveSpeed();
IVariable<Vector3> direction = entity.GetMoveDirection();
```

See the [Entity API Generator](../CodeGeneration/EntityAPI/EntityAPIGenerator.md) for all supported key types and
configuration options.

#### Step 4. Creating the Movement Mechanic

Let’s write a behaviour that will move our entity in the direction of its movement:

<!-- <img width="600" height="" alt="Entity component" src="Docs/Images/MovementMechanics.png"/> -->

```csharp
// Controller that moves entity by its direction
public sealed class MoveBehaviour : IEntityInit, IEntityFixedTick
{
    private Transform _transform;
    private IValue<float> _moveSpeed;
    private IVariable<Vector3> _moveDirection;

    // Called when Start() is invoked
    public void Init(IEntity entity)
    {
        _transform = entity.GetTransform();
        _moveSpeed = entity.GetMoveSpeed();
        _moveDirection = entity.GetMoveDirection();
    }

    // Called when FixedUpdate() is invoked
    public void FixedTick(IEntity entity, float deltaTime)
    {
        Vector3 direction = _moveDirection.Value;
        if (direction != Vector3.zero)
            _transform.position += _moveSpeed.Value * deltaTime * direction;
    }
}
```

> [!IMPORTANT]
> It’s important to note that in the Atomic approach, the developer **always works with data abstractions represented
> as reference-type wrappers.**
>
> This design greatly simplifies project maintenance, testing, and multiplayer development, as it removes the tight
> coupling to data storage methods that is typical for component-based ECS architectures.
>
> A simple example:
> Under the `IValue<T>` interface, you can substitute either a `Variable<T>` or a `Const<T>` implementation.

#### Step 5. Creating the Installer

To add the data and movement logic to the entity, let’s create a script that will inject the corresponding atomic
elements and behaviour into it.

 ```csharp
// Populates entity with tags, values and behaviours
public sealed class CharacterInstaller : MonoEntityInstaller
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Const<float> _moveSpeed = 5.0f;
    [SerializeField] private Variable<Vector3> _moveDirection = Vector3.forward;

    public override void Install(IEntity entity)
    {
        // Add properties to a character
        entity.AddTransform(_transform);
        entity.AddMoveSpeed(_moveSpeed);
        entity.AddMoveDirection(_moveDirection);

        // Add behaviours to a character
        entity.AddBehaviour<MoveBehaviour>();
    }
}
```

#### Step 6. Configuring the Game Object

Next, add the `CharacterInstaller` component to your entity through the Inspector and configure its settings.

<img width="400" height="" alt="изображение" src="https://github.com/user-attachments/assets/1967b1d8-b6b7-41c7-85db-5d6935f6443e" />

#### Step 7. Connecting the Installer to the Entity

To link the `CharacterInstaller` to the `Entity` component, drag and drop it into the **Mono Installers** field.

<img width="400" height="" alt="изображение" src="../Images/EntityInstalling.png" />

#### Step 8. Running the Character

In the Unity Editor, press Play to verify that the character starts moving forward.

## Adding Keyboard Input

Next, we’ll look at how to implement movement control using the WASD or arrow keys and show how to modify entity
structure through code.

#### Step 1. Create an Input Controller

```csharp
public class InputBehaviour : IEntityInit, IEntityTick
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private ISetter<Vector3> _moveDirection;

    public void Init(IEntity entity)
    {
        _moveDirection = entity.GetMoveDirection();
    }

    public void Tick(IEntity entity, float deltaTime)
    {
        float dx = Input.GetAxis(HORIZONTAL);
        float dz = Input.GetAxis(VERTICAL);
        _moveDirection.Value = new Vector3(dx, 0, dz);
    }
}
```

> [!IMPORTANT]
> Here, it’s important to note that to change the entity’s data, we simply modify the value through its reference.
> No `SetMoveDirection` on the `IEntity` is required.

#### Step 2. Add the InputBehaviour

Next, let’s register the `InputBehaviour` inside the `CharacterInstaller`:

```csharp
public sealed class CharacterInstaller : MonoEntityInstaller
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Const<float> _moveSpeed = 5.0f;
    [SerializeField] private Variable<Vector3> _moveDirection = Vector3.zero;

    public override void Install(IEntity entity)
    {
        // Add properties to a character
        entity.AddTransform(_transform);
        entity.AddMoveSpeed(_moveSpeed);
        entity.AddMoveDirection(_moveDirection);

        // Add behaviours to a character
        entity.AddBehaviour<MoveBehaviour>();
        entity.AddBehaviour<InputBehaviour>();
    }
}
```

Now the character can be controlled with the keyboard.

---

## ✅ Result

You now have a character entity that:

- Declares type-safe keys with `[GenerateEntityExtensionsAPI]`
- Uses generated extension methods to add and access data
- Reuses behaviours for movement and input

For more advanced scenarios, see the [Code Generation Walkthrough](Codegeneration.md).
