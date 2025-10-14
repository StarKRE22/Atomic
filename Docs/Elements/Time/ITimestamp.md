# 🧩 ITimestamp

Represents a **timestamp that can be tracked over time using ticks**. It provides properties and
methods to start, stop, and query the state of a timestamp, including remaining time, progress, and expiration status.

---

## 📑 Table of Contents


<ul>
  <li><a href="#-api-reference">API Reference</a>
    <ul>
      <li><a href="#-type">Type</a></li>
      <li>
        <details>
          <summary><a href="#-properties">Properties</a></summary>
          <ul>
            <li><a href="#endtick">EndTick</a></li>
            <li><a href="#remainingticks">RemainingTicks</a></li>
            <li><a href="#remainingtime">RemainingTime</a></li>
          </ul>
        </details>
      </li>
      <li>
        <details>
          <summary><a href="#-methods">Methods</a></summary>
          <ul>
            <li><a href="#startfromsecondsfloat">StartFromSeconds(float)</a></li>
            <li><a href="#startfromticksint">StartFromTicks(int)</a></li>
            <li><a href="#stop">Stop()</a></li>
            <li><a href="#getprogressfloat">GetProgress(float)</a></li>
            <li><a href="#isidle">IsIdle()</a></li>
            <li><a href="#isplaying">IsPlaying()</a></li>
            <li><a href="#isexpired">IsExpired()</a></li>
          </ul>
        </details>
      </li>
    </ul>
  </li>
</ul>

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface ITimestamp
```

---

### 🔑 Properties

#### `EndTick`

```csharp
public int EndTick { get; }
```

- **Description:** Gets the tick at which the timestamp is considered complete.

#### `RemainingTicks`

```csharp
public int RemainingTicks { get; }
```

- **Description:** Gets the number of ticks remaining until expiration.

#### `RemainingTime`

```csharp
public float RemainingTime { get; }
```

- **Description:** Gets the remaining time until expiration in seconds.

---

### 🏹 Methods

#### `StartFromSeconds(float)`

```csharp
public void StartFromSeconds(float seconds);
```

- **Description:** Starts the timestamp from the current time with a specified duration.
- **Parameters:** `seconds` — duration in seconds.

#### `StartFromTicks(int)`

```csharp
public void StartFromTicks(int ticks);
```

- **Description:** Starts the timestamp using a specified number of ticks.
- **Parameters:** `ticks` — duration in ticks.

#### `Stop()`

```csharp
public void Stop();
```

- **Description:** Stops and resets the timestamp. After calling this, the timestamp is idle.

#### `GetProgress(float)`

```csharp
public float GetProgress(float duration);
```

- **Description:** Returns the progress of the timestamp relative to a given duration.
- **Parameters:** `float duration` — the full duration in seconds.
- **Returns:** `float` — progress value between `0` and `1`.

#### `IsIdle()`

```csharp
public bool IsIdle();
```

- **Description:** Indicates whether the timestamp is stopped and has not started.
- **Returns:** `true` if idle; otherwise, `false`.

#### `IsPlaying()`

```csharp
public bool IsPlaying();
```

- **Description:** Indicates whether the timestamp is currently active and counting.
- **Returns:** `true` if playing; otherwise, `false`.

#### `IsExpired()`

```csharp
public bool IsExpired();
```

- **Description:** Indicates whether the timestamp has expired.
- **Returns:** `true` if expired; otherwise, `false`.