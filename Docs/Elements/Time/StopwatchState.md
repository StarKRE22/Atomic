# 🧩 StopwatchState

Represents the **current state of a stopwatch**. It is used by [IStopwatch](IStopwatch.md) and [Stopwatch](Stopwatch.md) to indicate whether the stopwatch is
idle, running, paused.

---

## 📑 Table of Contents

<ul>
  <li><a href="#-api-reference">API Reference</a>
    <ul>
      <li><a href="#-type">Type</a></li>
      <li>
        <details>
          <summary><a href="#-values">Values</a></summary>
          <ul>
            <li><a href="#idle">IDLE</a></li>
            <li><a href="#playing">PLAYING</a></li>
            <li><a href="#paused">PAUSED</a></li>
          </ul>
        </details>
      </li>
    </ul>
  </li>
</ul>


---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public enum StopwatchState
```

---

### 🔑 Values

#### `IDLE`

- **Description:** The stopwatch has not been started yet.
- **Usage:** Returned by `IStopwatch.GetState()` before `Start()` is called.

#### `PLAYING`

- **Description:** The stopwatch is actively measuring elapsed time.
- **Usage:** Returned by `IStopwatch.GetState()` while the stopwatch is counting.
- **Events triggered:** `OnTimeChanged` updates as time progresses.

#### `PAUSED`

- **Description:** The stopwatch was running but has been temporarily paused.
- **Usage:** Returned by `IStopwatch.GetState()` after `Pause()` is called and before `Resume()`.
- **Remarks:** Elapsed time does not increase until resumed.