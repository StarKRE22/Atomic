# 🧩 TimerState

Represents the current state of a timer. It is used by [ITimer](ITimer.md), [DownTimer](DownTimer.md) and [UpTimer](UpTimer.md) to track the
lifecycle of a timer and respond to state changes.

---

## 📑 Table of Contents

- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Values](#-values)
    - [Idle](#idle)
    - [Playing](#playing)
    - [Paused](#paused)
    - [Completed](#completed)


---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public enum TimerState
```

---

### 🔑 Values

#### `IDLE`

- **Description:** The timer is not running and has not been started.
- **Usage:** Initial state of a timer before `Start()` is called.

#### `PLAYING`

- **Description:** The timer is currently counting down or running.
- **Usage:** Indicates that the timer is active and `Tick()` is advancing its time.

#### `PAUSED`

- **Description:** The timer is paused and can be resumed.
- **Usage:** Timer is temporarily halted by `Pause()` and can continue counting when `Resume()` is called.

#### `COMPLETED`

- **Description:** The timer has finished counting down and expired.
- **Usage:** Indicates that the timer has reached its end, and `OnCompleted` is typically triggered. To restart, the
  timer should be started again.