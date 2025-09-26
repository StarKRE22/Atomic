# 🧩 ITickSource

```csharp
public interface ITickSource
```

- **Description:** Represents a source that <b>can be updated over time through the ticks</b>.

---

## 🏹 Methods

#### `Tick(float)`

```csharp
public void Tick(float deltaTime);  
```

- **Description:** Updates the source by a specified time increment.
- **Parameter:** `deltaTime` — The amount of time (in seconds) to advance the source.
- **Remarks:** This method is typically called repeatedly (e.g., once per frame) to progress time-dependent systems.