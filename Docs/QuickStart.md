# Quick Start
The Quick Start guide contains **two examples**:
- Using **Unity** with GameObjects and installers  
- Using **pure C#** without Unity components  

### With Unity

1. **Create a GameObject in a scene**
   
   <img width="360" height="255" alt="GameObject creation" src="https://github.com/user-attachments/assets/463a721f-e50d-4cb7-86be-a5d50a6bfa17" />

3. **Add the `Entity` component to the GameObject**
   
   <img width="464" height="346" alt="Entity component" src="https://github.com/user-attachments/assets/f74644ba-5858-4857-816e-ea47eed0e913" />

4. **Create a `CharacterInstaller` script and attach it to the GameObject**

 ```csharp
public sealed class CharacterInstaller : SceneEntityInstaller
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Const<float> _moveSpeed = 5.0f;
    [SerializeField] private ReactiveVariable<Vector3> _moveDirection;

    public override void Install(IEntity entity)
    {
        entity.AddTag("Character");
        entity.AddTag("Moveable");

        entity.AddValue("Transform", _transform);
        entity.AddValue("MoveSpeed", _moveSpeed);
        entity.AddValue("MoveDirection", _moveDirection);

        entity.AddBehaviour<MoveBehaviour>();
    }
}
```

5. **Write the `MoveBehaviour` class**

```csharp
public sealed class MoveBehaviour : IEntitySpawn, IEntityFixedUpdate
{
    private Transform _transform;
    private IValue<float> _moveSpeed;
    private IValue<Vector3> _moveDirection;

    public void OnSpawn(IEntity entity)
    {
        _transform = entity.GetValue<Transform>("Transform");
        _moveSpeed = entity.GetValue<IValue<float>>("MoveSpeed");
        _moveDirection = entity.GetValue<IValue<Vector3>>("MoveDirection");
    }

    public void OnFixedUpdate(IEntity entity, float deltaTime)
    {
        Vector3 direction = _moveDirection.Value;
        if (direction != Vector3.zero) 
            _transform.position += _moveSpeed.Value * deltaTime * direction;
    }
}
```
6. **Configure `CharacterInstaller`, Enter `PlayMode` and test your character movement**

<img width="464" height="153" alt="изображение" src="https://github.com/user-attachments/assets/1967b1d8-b6b7-41c7-85db-5d6935f6443e" />

### With C#

1. **Create a character**
```csharp
var character = new Entity("Character");

character.AddTag("Moveable");

character.AddValue("Position", new ReactiveVariable<Vector3>());
character.AddValue("MoveSpeed", new Const<float>(3.5f));
character.AddValue("MoveDirection", new ReactiveVariable<Vector3>());

character.AddBehaviour<MoveBehaviour>();
```

2. **Write `MoveBehaviour` for the character**
```csharp
public sealed class MoveBehaviour : IEntitySpawn, IEntityFixedUpdate
{
    private IVariable<Vector3> _position;
    private IValue<float> _moveSpeed;
    private IValue<Vector3> _moveDirection;

    public void OnSpawn(IEntity entity)
    {
        _position = entity.GetValue<IVariable<Vector3>>("Position");
        _moveSpeed = entity.GetValue<IValue<float>>("MoveSpeed");
        _moveDirection = entity.GetValue<IValue<Vector3>>("MoveDirection");
    }

    public void OnFixedUpdate(IEntity entity, float deltaTime)
    {
        Vector3 direction = _moveDirection.Value;
        if (direction != Vector3.zero) 
            _position.Value += _moveSpeed.Value * deltaTime * direction;
    }
}
```
3. **Activate the character one time**
```csharp
//Make entity spawned and calls IEntitySpawn
character.Spawn();

//Make entity active and calls IEntityActivate
character.Activate(); 
```

4. **Update the character multiple times**
```csharp
const float deltaTime = 0.02f;

//Calls IEntityFixedUpdate if entity is active
character.OnFixedUpdate(deltaTime); 
```

5. **Make entity inactive and despawned**
```csharp
//Make entity inactive
character.Deactivate();

//Despawn entity
character.Despawn();
```
