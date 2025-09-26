Represent a **timer family** that supports starting, pausing, resuming, stopping, progress tracking,
and state change notifications. Useful for gameplay timers, ability cooldowns, animation timers, and any system
requiring precise time management.

- [ITimer](ITimer.md)
- [UpTimer](UpTimer.md)
- [DownTimer](DownTimer.md)
- [TimerState](TimerState.md)

---

## 📌 Best Practice

**Choosing Between `ITimer` and `ICooldown`**

- `ITimer` is a **more advanced, stateful timer**:
    - Supports **Start, Pause, Resume, Stop**.
    - Exposes **all events**: OnStarted, OnPaused, OnResumed, OnStopped, OnCompleted, OnTimeChanged, OnProgressChanged,
      OnStateChanged.
    - Suitable for scenarios where the **timer itself is part of game logic**, e.g., timed buffs, game rounds, or
      special abilities.

- [ICooldown](ICooldown.md) is a **lightweight countdown**:
    - Only tracks remaining time and normalized progress.
    - Best for **simple countdowns**, repeated delays, or ability cooldowns where pausing or manual state control isn’t
      needed.

**Rule of thumb:**

- Use `ICooldown` for **simple timers**.
- Use `ITimer` for **complex, interactive timers** that need full state and control.
