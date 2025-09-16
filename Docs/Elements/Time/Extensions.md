# 🧩 Time Extensions

Provides **extension methods for** [Sources](Sources.md) to simplify restarting timers, countdowns, or other startable sources.

---

#### `void Restart(this IStartSource source, float time)`
```csharp
public static void Restart(this IStartSource source, float time);
```
- **Description:** Stops the source and restarts it from a specific time.
- **Parameters:**
    - `source` — the source to restart.
    - `time` — the time to start the source from.
- **Notes:** Internally calls `Stop()` and then `Start(time)` on the source.
- **Example:**
    
    ```csharp
    // Restart a countdown from a specific time
    IStartSource countdown = ...;
    countdown.Restart(5f); // stops and starts from 5 seconds
    ```
---

#### `void Restart(this IStartSource source)`
```csharp
public static void Restart(this IStartSource source);
```
- **Description:** Stops the source and restarts it from the default start time.
- **Parameter:** `source` — the source to restart.
- **Notes:** Internally calls `Stop()` and then `Start()` on the source.
- **Example:**

    ```csharp
    // Restart a countdown from its default start time
    countdown.Restart(); // stops and starts from default
    ```
---