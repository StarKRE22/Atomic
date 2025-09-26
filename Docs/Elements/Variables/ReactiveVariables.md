# 🧩 Reactive Variables

For convenience, several specialized implementations of reactive variables are provided. It is recommended to use them,
as they compare values without relying on `EqualityComparer`, which makes them slightly faster than the generic
[ReactiveVariable&lt;T&gt;](ReactiveVariable.md) version.

---

## 🧩 Common Types

| Variable        | Description               |
|-----------------|---------------------------|
| `ReactiveBool`  | Boolean reactive variable |
| `ReactiveInt`   | Integer reactive variable |
| `ReactiveFloat` | Float reactive variable   |

---

## 🧩 Unity Types

| Variable             | Description           |
|----------------------|-----------------------|
| `ReactiveQuaternion` | Stores a `Quaternion` |
| `ReactiveVector2`    | Stores a `Vector2`    |
| `ReactiveVector3`    | Stores a `Vector3`    |
| `ReactiveVector4`    | Stores a `Vector4`    |
| `ReactiveVector2Int` | Stores a `Vector2Int` |
| `ReactiveVector3Int` | Stores a `Vector3Int` |

---

## 🧩 Unity Mathematics Types

| Variable              | Description           |
|-----------------------|-----------------------|
| `reactive_int2`       | Stores an `int2`      |
| `reactive_int3`       | Stores an `int3`      |
| `reactive_int4`       | Stores an `int4`      |
| `reactive_float2`     | Stores a `float2`     |
| `reactive_float3`     | Stores a `float3`     |
| `reactive_float4`     | Stores a `float4`     |
| `reactive_quaternion` | Stores a `quaternion` |
