# 🧩 Base Variables

For convenience, several specialized implementations of base variables are provided. It is recommended to use them, as
they compare values without relying on `EqualityComparer`, which makes them slightly faster than the generic
[BaseVariable&lt;T&gt;](BaseVariable.md) version.

---

## 🧩 Common Types

| Variable        | Description      |
|-----------------|------------------|
| `BoolVariable`  | Boolean variable |
| `IntVariable`   | Integer variable |
| `FloatVariable` | Float variable   |

---

## 🧩 Unity Types

| Variable             | Description           |
|----------------------|-----------------------|
| `QuaternionVariable` | Stores a `Quaternion` |
| `Vector2Variable`    | Stores a `Vector2`    |
| `Vector3Variable`    | Stores a `Vector3`    |
| `Vector4Variable`    | Stores a `Vector4`    |
| `Vector2IntVariable` | Stores a `Vector2Int` |
| `Vector3IntVariable` | Stores a `Vector3Int` |

---

## 🧩 Unity Mathematics Types

| Variable              | Description           |
|-----------------------|-----------------------|
| `int2_variable`       | Stores an `int2`      |
| `int3_variable`       | Stores an `int3`      |
| `int4_variable`       | Stores an `int4`      |
| `float2_variable`     | Stores a `float2`     |
| `float3_variable`     | Stores a `float3`     |
| `float4_variable`     | Stores a `float4`     |
| `quaternion_variable` | Stores a `quaternion` |
