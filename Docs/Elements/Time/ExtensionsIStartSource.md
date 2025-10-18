# 🧩 IStartSource Extensions

Provides **extension methods for** [IStartSource](IStartSource.md) to simplify restarting timers, countdowns, or other startable
sources.

---

## 📑 Table of Contents


<ul>
  <li><a href="#-examples-of-usage">Examples of Usage</a>
    <ul>
      <li><a href="#ex1">Restart from a specific time</a></li>
      <li><a href="#ex2">Restart from the default time</a></li>
    </ul>
  </li>
  <li><a href="#-api-reference">API Reference</a>
    <ul>
      <li><a href="#-type">Type</a></li>
      <li>
        <details>
          <summary><a href="#-methods">Methods</a></summary>
          <ul>
            <li><a href="#restartistartsource">Restart(IStartSource)</a></li>
            <li><a href="#restartistartsource-float">Restart(IStartSource, float)</a></li>
          </ul>
        </details>
      </li>
    </ul>
  </li>
</ul>

---

## 🗂 Examples of Usage

<div id="ex1"></div>


<div id="ex2"></div>

### 1️⃣ Restart from the default time

```csharp
// Restart a countdown from its default start time
countdown.Restart(); // stops and starts from default
```


### 2️⃣ Restart from a specific time

```csharp
// Restart a countdown from a specific time
IStartSource countdown = ...;
countdown.Restart(5f); // stops and starts from 5 seconds
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public static class Extensions
```

---

### 🏹 Methods


#### `Restart(IStartSource)`

```csharp
public static void Restart(this IStartSource source);
```

- **Description:** Stops the source and restarts it from the default start time.
- **Parameter:** `source` — the source to restart.
- **Notes:** Internally calls `Stop()` and then `Start()` on the source.

---

#### `Restart(IStartSource, float)`

```csharp
public static void Restart(this IStartSource source, float time);
```

- **Description:** Stops the source and restarts it from a specific time.
- **Parameters:**
    - `source` — the source to restart.
    - `time` — the time to start the source from.
- **Notes:** Internally calls `Stop()` and then `Start(time)` on the source.