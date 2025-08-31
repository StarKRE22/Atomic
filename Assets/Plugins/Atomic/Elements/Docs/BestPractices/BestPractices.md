
- Принцип DIP в максимум используйте, 
- Interface Segregation на максимум тоже, чтобы более гибко было!
- Коллекции старайтесь указывать явный тип, так как Enumerator Struct!
Уходите от зависимостей Unity & зависимостей на всякие фреймворки особенно мультиплеер, чтобы код был максимально тестируемым!
- Используйте Observe для 
This section demonstrates how to implement `IVariable<T>` for **Transform position** and a **networked variable**.

- для однопользовательских игр используйте Requests AI или Input, а для сетевых Actions

- Разделять инсталлеры на маленькие фичи для больших проектов и EntityAPI

Best Practices
- Factory, Builder
- No Dependency Injection
- Flyweight Constants!


You can use extension Observe and cache subscription handle
```csharp
using UnityEngine;
using UnityEngine.UI; // Required for Text
using Atomic.Elements;
using System;

public sealed class ScorePresenter : IDisposable
{
    private readonly IReactiveValue<int> _score;
    private readonly Text _scoreText;
    private readonly Subscription<int> _subscription; //struct
    
    public ScorePresenter(IReactiveValue<int> score, Text scoreText)
    {
        _score = score;
        _scoreText = scoreText;

        // Subscribe and initialize UI with current value
        _subscription = _score.Observe(this.OnScoreChanged);
    }
    
    public void Dispose()
    {
        // Unsubscribe to avoid memory leaks
        _subscription.Dispose();
    }
    
    private void OnScoreChanged(int score)
    {
        // Update UI text
        _scoreText.text = "Score: " + score;
    }
}
```

```csharp
using UnityEngine;
using Atomic.Elements;
using System;

// -------------------- Transform Position Variable --------------------
public class TransformPositionVariable : IVariable<Vector3>
{
    private Transform _target;

    public TransformPositionVariable(Transform target)
    {
        _target = target;
    }

    public Vector3 Value
    {
        get => _target.position;
        set => _target.position = value;
    }
}

// -------------------- Network Variable --------------------
public class NetworkVariable<T> : IVariable<T> where T : unmanaged
{
    private readonly NetworkObject _networkObject;
    private IntPtr _ptr;
    
    public NetworkVariable(NetworkObject networkObject, IntPtr ptr)
    {
        _networkObject = networkObject;
        _ptr = ptr;
    }

    public T Value
    {
        get => _networkObject.ReadData<T>(_ptr);
        set => _networkObject.WriteData<T>(_ptr, value);
    }
}


/// <summary>
/// Represents a game character with health and position,
/// fully decoupled from Unity or multiplayer systems.
/// </summary>
public class Character
{
    /// <summary>
    /// Character's current health.
    /// </summary>
    public IVariable<int> Health { get; }

    /// <summary>
    /// Character's current position in the world.
    /// </summary>
    public IVariable<Vector3> Position { get; }

    public Character(IVariable<int> health, IVariable<Vector3> position)
    {
        Health = health ?? throw new ArgumentNullException(nameof(health));
        Position = position ?? throw new ArgumentNullException(nameof(position));
    }

    /// <summary>
    /// Apply damage to the character.
    /// </summary>
    public void TakeDamage(int damage)
    {
        Health.Value = Math.Max(0, Health.Value - damage);
    }

    /// <summary>
    /// Move the character by a delta vector.
    /// </summary>
    public void Move(Vector3 delta)
    {
        Position.Value += delta;
    }

    /// <summary>
    /// Check if the character is alive.
    /// </summary>
    public bool IsAlive => Health.Value > 0;
}

//Easy testing without multiplayer and Unity dependencies!
using System;
using System.Numerics;
using Atomic.Elements;

public class CharacterTest
{
    [Test]
    public void RunTest()
    {
        // Use BaseVariable to allow read-write for testing
        var health = new BaseVariable<int>(100);
        var position = new BaseVariable<Vector3>(new Vector3(0, 0, 0));

        var character = new Character(health, position);
        Console.WriteLine($"Initial Health: {character.Health.Value}"); // 100
        Console.WriteLine($"Initial Position: {character.Position.Value}"); // (0,0,0)

        // Test taking damage
        character.TakeDamage(30);
        Console.WriteLine($"Health after damage: {character.Health.Value}"); // 70

        // Test movement
        character.Move(new Vector3(1, 2, 0));
        Console.WriteLine($"Position after move: {character.Position.Value}"); // (1,2,0)

        // Test death
        character.TakeDamage(100);
        Console.WriteLine($"IsAlive: {character.IsAlive}"); // False
    }
}
```
