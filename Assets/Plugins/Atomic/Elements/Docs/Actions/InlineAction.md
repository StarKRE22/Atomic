# 🧩 InlineAction

The **InlineAction** classes are lightweight wrappers around standard `System.Action` delegates.  
They implement the corresponding `IAction` interfaces, allowing actions to be executed and used polymorphically.

### Key Features

- Wrap any `System.Action` or `System.Action<T>` delegate.
- Support implicit conversion from native `Action` types.
- Optional integration with Odin Inspector for inline property and button execution.
- Provides `ToString()` to return the method name of the wrapped delegate.
- Supports up to **four generic parameters** (`InlineAction<T1, T2, T3, T4>`).

> **Note:** `InlineAction` is ideal for game development scenarios where actions on game objects need to be *
*polymorphic**, such as event handling, command execution, or reactive systems.

### Example of Usage
Procedural polymorphism and composition over inheritance
```csharp
var tank = new Entity("Tank");
tank.AddValue<IAction<Vector3>>("MoveAction",
    new InlineAction<Vector3>(direction => MoveByRigidbody(tank, direction))
);

var ship = new Entity("Ship");
ship.AddValue<IAction<Vector3>>("MoveAction",
    new InlineAction<Vector3>(direction => MoveByTransform(ship, direction))
);

// Invoke actions
tank.GetValue<IAction<Vector3>>("MoveAction").Invoke(Vector3.forward); // Moves by Rigidbody
ship.GetValue<IAction<Vector3>>("MoveAction").Invoke(Vector3.forward); // Moves by Transform
```
