
<details>
  <summary>
    <h2 id="-context"> ▶️ Context Menu</h2>
  </summary>
<br>

### 🏹 Methods

#### `Compile`

```csharp
[ContextMenu("Compile")]
private void Compile();
```

- **Description:** Fully compiles entity state:
- **Behaviour**:
    1. Disable and Dispose entity in Edit mode if gameObject is not prefab. Only for behaviours
       with [RunInEditModeAttribute](../Attributes/RunInEditModeAttribute.md)
    2. Uninstall previous entity state
    3. Install new entity state
    4. Precomputes **capacity**, **tags**, **values**, **behaviours** of the entity
    5. Init and Enable entity in Edit mode if gameObject is not prefab. Only for behaviours
       with [RunInEditModeAttribute](../Attributes/RunInEditModeAttribute.md)

#### `Reset`

```csharp
[ContextMenu("Reset")]
private void Reset();
```

- **Description:** Fully resets entity state:
    1. Disable and Dispose entity in Edit mode if gameObject is not prefab. Only for behaviours
       with [RunInEditModeAttribute](../Attributes/RunInEditModeAttribute.md)
    2. Uninstall previous entity state
    3. Resets all parameters to default
    4. Gathers all SceneEntityInstallers and child Entities

</details>