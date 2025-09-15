# 🧩️ ICooldown

`ICooldown` represents a contract of **cooldown timer** that tracks remaining time,  
provides progress feedback (0–1), and raises events when its state changes.

> [!NOTE]
> It is useful for game mechanics such as ability cooldowns, weapon reloads, and timed delays.


> The interface combines multiple sources (`IDurationSource`, `ITimeSource`, `IProgressSource`, `ICompleteSource`, `ITickSource`)  
to provide flexible access to timer data and notifications.