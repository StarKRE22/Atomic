# 🧩 IValue&lt;T&gt;

Represents a **read-only value provider interface**. It inherits
from [IFunction&lt;R&gt;](../Functions/IFunction.md#ifunctionr) and exposes a strongly-typed `Value` property.

```csharp
public interface IValue<out T> : IFunction<T>
```

- **Type Parameters:** `T` – The type of the value being returned.

---

## 🔑 Properties

#### `Value`

```csharp
public T Value { get; }
```

- **Description:** Gets the current value.
- **Access:** Read-only

---

## 🏹 Methods

#### `Invoke()`

```csharp
public T Invoke()
```

- **Description:** Invokes the function and returns the value.
- **Returns:** The current value of type `T`.
- **Notes**: This is the default implementation from [IFunction&lt;R&gt;.Invoke()](../Functions/IFunction.md#invoke)

---

## 🗂 Example of Usage

**`IValue<T>` can wrap any data source, for example:**

```csharp
public class UsageExample : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private int _defaultScore;
    [SerializeField] private AudioClip[] _audioClips;
    
    private void Awake()
    {
        // Transform position as a value provider
        IValue<Vector3> playerPosition = new TransformPositionValueProvider(_transform);
        
        // PlayerPrefs as a value provider
        IValue<int> highScore = new PlayerPrefsIntProvider("HighScore", 0));
    
        // Random audio clip provider
        IValue<Vector3> audioClip = new RandomAudioClipProvider();
        
        Debug.Log("High Score: " + highScore.Value);
        Debug.Log("Player Position: " + playerPosition.Value);
        Debug.Log("Audio clip: " + audioClip.Value);
    }
}
```

---

### Example #1: Position from Transform

```csharp
public sealed class TransformPositionProvider : IValue<Vector3>
{
    private readonly Transform _transform;
    
    public Vector3 Value => _transform.position;
    
    public TransformPositionProvider(Transform transform) 
    {
        _transform = transform ?? throw new ArgumentNullExeption(nameof(transform));
    }
}
```

---

### Example #2: Integer value from PlayerPrefs

```csharp
public sealed class PlayerPrefsIntProvider : IValue<int>
{
    private readonly string key;
    private readonly int defaultValue;

    public int Value => PlayerPrefs.GetInt(key, defaultValue);
    
    public PlayerPrefsIntProvider(string key, int defaultValue = 0)
    {
        this.key = key;
        this.defaultValue = defaultValue;
    }
}
```

---

### Example #3: Random AudioClip from a list

```csharp
public class RandomAudioClipProvider : IValue<AudioClip>
{
    private readonly IReadOnlyList<AudioClip> clips;

    public RandomAudioClipProvider(IReadOnlyList<AudioClip> clips)
    {
        this.clips = clips;
    }

    public AudioClip Value
    {
        get
        {
            int index = Random.Range(0, this.clips.Count);
            return this.clips[index];
        }
    }
}
```

