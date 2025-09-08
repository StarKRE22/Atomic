# 🧩 Value

`IValue<T>` is a **read-only value provider interface**. It inherits from [IFunction<T>](../Functions/IFunction.md) and exposes a strongly-typed `Value` property.

---

## Type Parameter
- `T` – The type of the value being returned.
---

## Properties
```csharp
T Value { get; }
```
- Description: Gets the current value.
- Access: Read-only

## Methods
```csharp
T Invoke()
```
- Description: Invokes the function and returns the value.
  This is the default implementation from IFunction<T> and simply returns Value.
- Returns: The current value of type T.


## Example of Usage
IValue<T> can wrap any data source, for example:

```csharp
public sealed class Example : MonoBehaviour
{
    [SerializeField]
    private Transform _transform;
    
    [SerializeField]
    private int _defaultScore;
    
    [SerializeField]
    private AudioClip[] _audioClips;
    
    void Awake()
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

// Example provider wrapping Transform.position
public sealed class TransformPositionProvider : IValue<Vector3>
{
    private readonly Transform _transform;
    
    public Vector3 Value => _transform.position;
    
    public TransformPositionProvider(Transform transform) =>
        _transform = transform ?? throw new ArgumentNullExeption(nameof(transform));
}

/// <summary>
/// Provides an integer value from PlayerPrefs as a read-only IValue<int>.
/// </summary>
public sealed class PlayerPrefsIntProvider : IValue<int>
{
    private readonly string key;
    private readonly int defaultValue;

    public PlayerPrefsIntProvider(string key, int defaultValue = 0)
    {
        this.key = key;
        this.defaultValue = defaultValue;
    }

    public int Value => PlayerPrefs.GetInt(key, defaultValue);
}

/// <summary>
/// Provides a random AudioClip from a list as a read-only IValue<AudioClip>.
/// </summary>
public class RandomAudioClipProvider : IValue<AudioClip>
{
    private readonly AudioClip[] clips;

    public RandomAudioClipProvider(AudioClip[] clips)
    {
        this.clips = clips;
    }

    public AudioClip Value
    {
        get
        {
            if (clips == null || clips.Length == 0)
                return null;
            int index = Random.Range(0, clips.Length);
            return clips[index];
        }
    }
}
```