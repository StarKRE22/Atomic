# 🧩 Timestamps

Represents a **timestamp that can be tracked over time using ticks**. It provides properties and methods to start, stop,
and query the state of a timestamp, including remaining time, progress, and expiration status. Timestamp is especially useful in **tick-based systems**, where the game logic updates in discrete ticks. It is ideal
for multiplayer scenarios with **client-side prediction**, as it allows precise tracking of time and progress
independently of frame rate.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)

---

## 🗂 Example of Usage

```csharp
public class Example : MonoBehaviour 
{
    ITimestamp _timestamp = new FixedTimestamp();
    
    private void Awake()
    {
        _timestamp.StartFromSeconds(5f);
        //_timestamp.StartFromTicks(250); (equivalent to 5 seconds)
    }
    
    private void FixedUpdate()
    {
        if (_timestamp.IsExpired())
            Debug.Log("Timestamp expired!");
        else if (_timestamp.IsPlaying())
            Debug.Log("Timestamp is still running.");
        else if (_timestamp.IsIdle())
            Debug.Log("Timestamp is idle.");
    }
}
```

---

## 🔍 API Reference

- [ITimestamp](ITimestamp.md) — Contract of the timestamp
- [FixedTimestamp](FixedTimestamp.md) — Concrete implementation of the timestamp driven by Unity's `Time.fixedTime`
