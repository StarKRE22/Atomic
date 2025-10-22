# 📌 Timers vs Cooldowns

Both **[Timer](../Elements/Time/Timers.md)** and **[Cooldown](../Elements/Time/Cooldowns.md)** perform countdown-related
functions, but they serve different purposes depending on your
gameplay or system requirements. This document explains the distinction between them and provides guidelines for when to
use each.

## 📑 Table of Contents

- [When to Use](#when-to-use)
- [Key Differences](#key-differences)
- [Summary](#summary)
- [Example Scenarios](#example-scenarios)

---

## When to Use

| Use Case                                                                | Recommended Element                         | Description                                            |
|-------------------------------------------------------------------------|---------------------------------------------|--------------------------------------------------------|
| Simple time interval between actions (e.g., attacks, reloads, respawns) | [`Cooldown`](../Elements/Time/Cooldowns.md) | Use when you only need to measure time between events. |
| Complex systems involving simulated or global time                      | [`Timer`](../Elements/Time/Timers.md)       | Use when you need a fully controllable timer object.   |

---

## Key Differences

| Feature           | **Timer**                                                                                                                | **Cooldown**                                                                   |
|-------------------|--------------------------------------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------|
|  **Complexity** | Advanced, stateful timer                                                                                                 | Lightweight countdown                                                          |
| **Control**    | `Start`, `Pause`, `Resume`, `Stop`                                                                                       | Only tracks remaining time                                                     |
| **Events**     | `OnStarted`, `OnPaused`, `OnResumed`, `OnStopped`, `OnCompleted`, `OnTimeChanged`, `OnProgressChanged`, `OnStateChanged` | `OnCompleted`, `OnTimeChanged`, `OnProgressChanged`, `OnStateChanged`          |
| **Use Case**   | Timed buffs, game rounds, special abilities — when the timer is part of gameplay logic                                   | Simple countdowns, repeated delays, or ability cooldowns without state control |

---

## Summary

| If you need...                                              | Use          |
|-------------------------------------------------------------|--------------|
| A simple interval between actions                           | **Cooldown** |
| A controllable, stateful timer with pause/resume and events | **Timer**    |

---

## Example Scenarios

- **Cooldown** — ideal for:
    - Weapon reload times
    - Ability cooldowns
    - Respawn delays

- **Timer** — ideal for:
    - Timed buffs or power-ups
    - Round-based game timers
    - Event-driven time simulations

---

**In short:**
> Use `Cooldown` for simplicity, and `Timer` when you need control.


<!--

A common question for developers is: if both a timer and a cooldown perform a countdown function, which one should you
choose? The answer is simple:

- Use [Cooldown](../Elements/Time/Cooldowns.md) when you just need a time interval for a mechanic.
- Use [Timer](../Elements/Time/Timers.md) when you need a full simulation of a countdown as an object.

Examples:

- For time between attacks, reloads, respawn, [Cooldown](../Elements/Time/Cooldowns.md) works best.
- For complex systems that include a game time, [Timer](../Elements/Time/Timers.md) is the better choice.

Below is a comparison table between a timer and a cooldown:

| Feature        | `Timer`                                                                                                 | `Cooldown`                                                                                      |
|----------------|---------------------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------|
| **Complexity** | Advanced, stateful timer                                                                                | Lightweight countdown                                                                           |
| **Control**    | Start, Pause, Resume, Stop                                                                              | Only tracks remaining time                                                                      |
| **Events**     | OnStarted, OnPaused, OnResumed, OnStopped, OnCompleted, OnTimeChanged, OnProgressChanged, OnStateChanged | OnCompleted, OnTimeChanged, OnProgressChanged, OnStateChanged                                   |
| **Use Case**   | Timed buffs, game rounds, special abilities — when the timer is part of game logic                      | Simple countdowns, repeated delays, ability cooldowns where pausing / state control isn’t needed |

-->