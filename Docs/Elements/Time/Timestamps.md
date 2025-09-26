# 🧩 Timestamps

Represents a **timestamp that can be tracked over time using ticks**. It provides properties and methods to start, stop,
and query the state of a timestamp, including remaining time, progress, and expiration status.

> [!TIP]
> Timestamp is especially useful in **tick-based systems**, where the game logic updates in discrete ticks. It is ideal
> for multiplayer scenarios with **client-side prediction**, as it allows precise tracking of time and progress independently of frame
> rate.

- [ITimestamp](ITimestamp.md) — Contract of the timestamp 
- [FixedTimestamp](FixedTimestamp.md) — Concrete implementation of the timestamp driven by Unity's `Time.fixedTime`
