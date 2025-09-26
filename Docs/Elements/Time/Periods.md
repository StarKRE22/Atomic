# 🧩 Periods

Represents a **stateful looping cycle timer** that tracks time progression and emits events on completion of each cycle.
It provides full control over **start, pause, resume, stop**, progress tracking, duration management, and state notifications.

> [!TIP]
> Use `Period` when you need **repeating timers or
> cycle-based events**, such as gameplay loops, animations, or periodic system updates. Unlike [ITimer](ITimer.md),
> `Period` automatically loops on completion.

- [IPeriod](IPeriod.md) — Represents interface of the period
- [Period](Period.md) — Represents implementation of the period
- [PeriodState](PeriodState.md) — Represents current state of the period