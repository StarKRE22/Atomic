# 🧩 Source Interfaces

Provides a set of flexible interfaces for **time tracking**, **state management**, and **progress monitoring** in
reactive systems. These interfaces allow you to create sources that:

---

## 🔍 API Reference

There are several **source interfaces** depending on the required properties, methods, and events:

- [ITimeSource](ITimeSource.md) — Track **current time**  and notify listeners of changes.
- [IDurationSource](IDurationSource.md) — Handle total duration tracking.
- [ITickSource](ITickSource.md) — Update incrementally via **ticks** .
- [IStartSource](IStartSource.md) — Start, stop execution.
- [IPauseSource](IPauseSource.md) — Pause, or resume execution.
- [ICompleteSource](ICompleteSource.md) — Signal completion.
- [IProgressSource](IProgressSource.md) — Progress updates.
- [IStateSource&lt;T&gt;](IStateSource.md) — Maintain and notify **state changes**.