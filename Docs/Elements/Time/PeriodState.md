# 🧩 PeriodState

Represents the current state of a period. It is used by [IPeriod](IPeriod.md) and [Period](Period.md) to track the
lifecycle of a timer and respond to state changes.


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
public enum PeriodState
```

---

### 🔑 Values

#### `IDLE`

- **Description:** The period timer is not running and has not been started.
- **Usage:** Initial state before `Start()` is called.

#### `PLAYING`

- **Description:** The period timer is currently running and advancing time.
- **Usage:** Indicates the timer is active and `Tick()` is progressing the cycle.

#### `PAUSED`

- **Description:** The period timer is paused.
- **Usage:** Timer is temporarily halted by `Pause()` and can continue counting when `Resume()` is called.