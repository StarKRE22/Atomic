# 🧩 Default Constants

## Overview
The **Default Constants** collection provides a centralized set of commonly used values across multiple domains, including Boolean logic, mathematics, time, physics, and Unity-specific utilities.  

These constants are designed to improve **code readability**, **reduce magic numbers**, and ensure **consistency** throughout your projects.

- **Boolean Constants:** Standard `true` and `false` values.
- **Mathematical Constants:** Common mathematical numbers such as π (`PI`), Euler's number (`E`), and conversion factors like `Deg2Rad`.
- **Time Constants:** Convenient values for seconds, minutes, hours, and frame durations at 60 FPS.
- **Common Values:** Frequently used numeric constants (`Zero`, `One`, `Half`, `NegativeOne`) for integers and floats.
- **Physics Constants:** Default physics values, including Earth's gravity and standard mass.
- **Unity-Specific Vectors:** Standard unit vectors and directional helpers for 3D space (`Up`, `Forward`, `ZeroVector`, etc.).
- **Unity-Specific Colors:** Predefined colors and transparency for quick usage in Unity (`White`, `Red`, `Transparent`, etc.).

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