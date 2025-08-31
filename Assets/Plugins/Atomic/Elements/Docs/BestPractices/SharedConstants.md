# 🏆 Shared Constants

Whenever you have a **read-only, constant value** that multiple systems need to access, wrap it in `Const<T>`.  
This approach makes your code **lightweight, safe, and reference-friendly**, avoiding unnecessary copies of data.

---

## Benefits

- Acts as a **lightweight reference type**, even for primitive or value types.
- Can be **shared across multiple objects or systems** without duplication.
- Implements `IValue<T>`, so it can integrate seamlessly with reactive or ECS-style architectures.
- Keeps data **immutable**, preventing accidental modifications.
- Works seamlessly with **default constants**, ensuring consistent, reusable values throughout your project.

---

## Example: Shared Movement Speed

```csharp
public sealed class Character : MonoBehaviour
{
    private IValue<float> _moveSpeed;

    public void SetMoveSpeed(IValue<float> moveSpeed) =>
        _moveSpeed = moveSpeed;

    public void MoveStep(Vector3 direction) =>
        this.transform.position += direction * _moveSpeed.Value;
}

public sealed class GameInitializer : MonoBehaviour
{
    [SerializeField]
    private Const<float> _moveSpeed = 3f;  // Shared constant for all characters

    [SerializeField]
    private Character[] _characters; // 100+ characters

    void Start()
    {
        // Assign the same Const<float> instance to all characters
        foreach (Character character in _characters)
            character.SetMoveSpeed(_moveSpeed);
    }
}
```
---

##  Default Constants

`Const<T>` provides a convenient way to represent **default constant values** that can be reused across your systems.  
These constants are **lightweight, immutable**, and can be shared safely between multiple objects or components.

---

## Boolean Constants
| Name    | Value   | Description        |
|---------|---------|--------------------|
| `True`  | `true`  | Represents `true`  |
| `False` | `false` | Represents `false` |

## Mathematical Constants
| Name          | Value      | Description                         |
|---------------|------------|-------------------------------------|
| `PI`          | 3.1415927f | π (pi)                              |
| `TwoPI`       | 2 * PI     | 2π, for circular math               |
| `HalfPI`      | PI / 2     | π/2, for trigonometry               |
| `E`           | 2.7182818f | Euler's number                      |
| `GoldenRatio` | 1.6180339f | Golden ratio                        |
| `Deg2Rad`     | 0.01745    | Degrees to radians (Unity specific) |
| `Rad2Deg`     | 57.2958    | Radians to degrees (Unity specific) |

## Time Constants
| Name             | Value    | Description           |
|------------------|----------|-----------------------|
| `Second`         | 1f       | One second            |
| `Minute`         | 60f      | One minute in seconds |
| `Hour`           | 3600f    | One hour in seconds   |
| `FrameTime60FPS` | 1f / 60f | Frame time at 60 FPS  |

## Common Values
| Name          | Value | Description        |
|---------------|-------|--------------------|
| `ZeroInt`     | 0     | Integer zero       |
| `OneInt`      | 1     | Integer one        |
| `Zero`        | 0f    | Float zero         |
| `One`         | 1f    | Float one          |
| `NegativeOne` | -1f   | Float negative one |
| `Half`        | 0.5f  | Float one half     |

## Physics Constants
| Name           | Value | Description               |
|----------------|-------|---------------------------|
| `GravityEarth` | 9.81f | Standard gravity on Earth |
| `DefaultMass`  | 1f    | Default mass              |

## Unity-Specific Vectors
| Name         | Value    | Description         |
|--------------|----------|---------------------|
| `Up`         | (0,1,0)  | Unit vector up      |
| `Down`       | (0,-1,0) | Unit vector down    |
| `Left`       | (-1,0,0) | Unit vector left    |
| `Right`      | (1,0,0)  | Unit vector right   |
| `Forward`    | (0,0,1)  | Unit vector forward |
| `Back`       | (0,0,-1) | Unit vector back    |
| `ZeroVector` | (0,0,0)  | Zero vector         |
| `OneVector`  | (1,1,1)  | One vector          |

## Unity-Specific Colors
| Name          | Value     | Description       |
|---------------|-----------|-------------------|
| `White`       | (1,1,1,1) | White color       |
| `Black`       | (0,0,0,1) | Black color       |
| `Red`         | (1,0,0,1) | Red color         |
| `Green`       | (0,1,0,1) | Green color       |
| `Blue`        | (0,0,1,1) | Blue color        |
| `Transparent` | (0,0,0,0) | Fully transparent |

---

> **Tip:** Using default constants ensures consistency across your game systems, reduces memory usage, and provides a lightweight way to pass immutable values.
