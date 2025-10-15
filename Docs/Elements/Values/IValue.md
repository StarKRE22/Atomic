# 🧩 IValue&lt;T&gt;

Represents a **read-only value provider interface** and exposes a strongly-typed `Value` property.


---

## 📑 Table of Contents

- [Examples of Usage](#examples-of-usage)
    - [Transform Position](#ex1)
    - [PlayerPrefs Value](#ex2)
    - [Random AudioClip](#ex3)
- [API Reference](#api-reference)
    - [Type](#type)
    - [Properties](#properties)
        - [Value](#value)
    - [Methods](#methods)
        - [Invoke()](#invoke)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Transform Position

```csharp
public class TransformPositionProvider : IValue<Vector3>
{
    private readonly Transform _transform;
    
    public Vector3 Value => _transform.position;
    
    public TransformPositionProvider(Transform transform) 
    {
        _transform = transform ?? throw new ArgumentNullExeption(nameof(transform));
    }
}
```

```csharp
public class Example : MonoBehaviour
{
    [SerializeField] 
    private Transform _transform;
    
    private void Awake()
    {
        IValue<Vector3> position = new TransformPositionProvider(_transform);
        Debug.Log("Position: " + playerPosition.Value);
    }
}
```

---

<div id="ex2"></div>

### 2️⃣ PlayerPrefs Value

```csharp
public class IntPlayerPrefsProvider : IValue<int>
{
    private readonly string key;
    private readonly int defaultValue;

    public int Value => PlayerPrefs.GetInt(key, defaultValue);
    
    public IntPlayerPrefsProvider(string key, int defaultValue = 0)
    {
        this.key = key;
        this.defaultValue = defaultValue;
    }
}
```

```csharp
public class Example : MonoBehaviour
{
    [SerializeField] 
    private int _defaultScore;
    
    private void Awake()
    {
        IValue<int> highScore = new IntPlayerPrefsProvider("HighScore", _defaultScore));
        Debug.Log("High Score: " + highScore.Value);
    }
}
```

---

<div id="ex3"></div>

### 3️⃣ Random AudioClip

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

```csharp
public class Example : MonoBehaviour
{
    [SerializeField] 
    private AudioClip[] _audioClips;
    
    private void Awake()
    {
        IValue<Vector3> audioClip = new RandomAudioClipProvider(_audioClips);
        Debug.Log("Audio clip: " + audioClip.Value.name);
    }
}
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IValue<out T> : IFunction<T>
```

- **Description:** Represents a **read-only value provider interface** and exposes a strongly-typed `Value` property.
- **Inheritance:** [IFunction&lt;R&gt;](../Functions/IFunction.md)
- **Type Parameter:** `T` – The type of the value being returned.

---

### 🔑 Properties

#### `Value`

```csharp
public T Value { get; }
```

- **Description:** Gets the current value.
- **Access:** Read-only

---

### 🏹 Methods

#### `Invoke()`

```csharp
public T Invoke()
```

- **Description:** Invokes the function and returns the value.
- **Returns:** The current value of type `T`.
- **Notes**: This is the default implementation from [IFunction&lt;R&gt;](../Functions/IFunction.md)
