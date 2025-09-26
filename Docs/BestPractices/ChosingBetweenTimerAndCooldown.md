# **Choosing Between `ITimer` and `ICooldown`**

A common question for developers is: if both a timer and a cooldown perform a countdown function, which one should you
choose?

The answer is simple:

- Use `ICooldown` when you just need a time interval for a mechanic.
- Use `ITimer` when you need a full simulation of a countdown as an object.

Examples:

- For time between attacks, `ICooldown` works best.
- For complex levels that include a timer, `ITimer` is the better choice.

Below is a comparison table between a timer and a cooldown:

| Feature        | `ITimer`                                                                                                                     | `ICooldown`                                                                                    |
|----------------|------------------------------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------|
| **Complexity** | Advanced, stateful timer                                                                                                     | Lightweight countdown                                                                          |
| **Control**    | Start, Pause, Resume, Stop                                                                                                   | Only tracks remaining time                                                                     |
| **Events**     | Full event support: OnStarted, OnPaused, OnResumed, OnStopped, OnCompleted, OnTimeChanged, OnProgressChanged, OnStateChanged | None                                                                                           |
| **Use Case**   | Timed buffs, game rounds, special abilities — when the timer is part of game logic                                           | Simple countdowns, repeated delays, ability cooldowns where pausing/state control isn’t needed |

**Rule of Thumb:**

- ✅ Use `ICooldown` for **simple timers**.
- ✅ Use `ITimer` for **complex, interactive timers** that require full state and control.
