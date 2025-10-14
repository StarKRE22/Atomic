# 🧩 Cooldowns

Represent a **family of cooldown timers**. It tracks remaining time, provides progress feedback and raises events
when its state changes. It is useful for game mechanics such as ability cooldowns, weapon reloads, and timed delays.

There are an interface and two implementations of the timer.

- [ICooldown](ICooldown.md)
- [Cooldown](Cooldown.md)
- [RandomCooldown](RandomCooldown.md)

---

## 🗂 Example of Usage

The following example demonstrates how to use **cooldown** for spawning coins as game objects in a scene, together with
the `Atomic.Entities` framework.

---
