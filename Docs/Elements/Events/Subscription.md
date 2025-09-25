
---

<details>
  <summary>
    <h2 id="subscription">🧩 Subscription</h2>
    <br> Represents a subscription to a <b>parameterless signal</b>.
  </summary>

<br>

```csharp
public readonly struct Subscription : IDisposable
```

---

### 🏗️ Constructors

#### `Subscription(ISignal, Action)`

```csharp
public Subscription(ISignal signal, Action action)
```

- **Description:** Initializes a new subscription for a parameterless signal.
- **Parameters:**
    - `signal` — The signal source.
    - `action` — The delegate to unsubscribe on disposal.

---

### 🏹 Methods

#### `Dispose()`

```csharp
public void Dispose()
```

- **Description:** Unsubscribes the associated action from the signal source.

---

### 🗂 Example of Usage

```csharp
//Assume we have a instance of ISignal
ISignal signal = ...

//Subscribe on the signal    
Subscription subscription = signal.Subscribe(lambda);

// Later, dispose to unsubscribe
subscription.Dispose();
```

</details>

---

<details>
  <summary>
    <h2 id="subscriptiont">🧩 Subscription&lt;T&gt;</h2>
    <br> Represents a subscription to a <b>signal emitting one value</b>.
  </summary>

<br>

```csharp
public readonly struct Subscription<T> : IDisposable
```

- **Type parameter:** `T` — The type of the emitted value.

---

### 🏗️ Constructors

#### `Subscription(ISignal<T>, Action<T>)`

```csharp
public Subscription(ISignal<T> signal, Action<T> action)
```

- **Description:** Initializes a new subscription for a signal emitting one value.
- **Parameters:**
    - `signal` — The signal source.
    - `action` — The delegate to unsubscribe on disposal.

---

### 🏹 Methods

#### `Dispose()`

```csharp
public void Dispose()
```

- **Description:** Unsubscribes the associated action from the signal source.

---

### 🗂 Example of Usage

```csharp
//Assume we have a instance of ISignal
ISignal<T> signal = ...

//Subscribe on the signal
Subscription<T> subscription = signal.Subscribe<T>(lambda);

// Later, dispose to unsubscribe
subscription.Dispose();
```

</details>

---

<details>
  <summary>
    <h2 id="subscriptiont1-t2">🧩 Subscription&lt;T1, T2&gt;</h2>
    <br> Represents a subscription to a <b>signal emitting two values</b>.
  </summary>

<br>

```csharp
public readonly struct Subscription<T1, T2> : IDisposable
```

- **Type parameters:**
    - `T1` — The type of the first emitted value.
    - `T2` — The type of the second emitted value.

---

### 🏗️ Constructors

#### `Subscription(ISignal<T1, T2>, Action<T1, T2>)`

```csharp
public Subscription(ISignal<T1, T2> signal, Action<T1, T2> action)
```

- **Description:** Initializes a new subscription for a signal emitting two values.
- **Parameters:**
    - `signal` — The signal source.
    - `action` — The delegate to unsubscribe on disposal.

---

### 🏹 Methods

#### `Dispose()`

```csharp
public void Dispose()
```

- **Description:** Unsubscribes the associated action from the signal source.

---

### 🗂 Example of Usage

```csharp
//Assume we have a instance of ISignal
ISignal<T1, T2> signal = ...

//Subscribe on the signal
Subscription<T1, T2> subscription = signal.Subscribe<T1, T2>(lambda);

// Later, dispose to unsubscribe
subscription.Dispose();
```

</details>

---

<details>
  <summary>
    <h2 id="subscriptiont1-t2-t3">🧩 Subscription&lt;T1, T2, T3&gt;</h2>
    <br> Represents a subscription to a <b>signal emitting three values</b>.
  </summary>

<br>

```csharp
public readonly struct Subscription<T1, T2, T3> : IDisposable
```

- **Type parameters:**
    - `T1` — The type of the first emitted value.
    - `T2` — The type of the second emitted value.
    - `T3` — The type of the third emitted value.

---

### 🏗️ Constructors

#### `Subscription(ISignal<T1, T2, T3>, Action<T1, T2, T3>)`

```csharp
public Subscription(ISignal<T1, T2, T3> signal, Action<T1, T2, T3> action)
```

- **Description:** Initializes a new subscription for a signal emitting three values.
- **Parameters:**
    - `signal` — The signal source.
    - `action` — The delegate to unsubscribe on disposal.

---

### 🏹 Methods

#### `Dispose()`

```csharp
public void Dispose()
```

- **Description:** Unsubscribes the associated action from the signal source.

---

### 🗂 Example of Usage

```csharp
//Assume we have a instance of ISignal
ISignal<T1, T2, T3> signal = ...
    
//Subscribe on the signal
Subscription<T1, T2, T3> subscription = signal.Subscribe<T1, T2, T3>(lambda);

// Later, dispose to unsubscribe
subscription.Dispose();
```

</details>

---

<details>
  <summary>
    <h2 id="subscriptiont1-t2-t3-t4">🧩 Subscription&lt;T1, T2, T3, T4&gt;</h2>
    <br> Represents a subscription to a <b>signal emitting four values</b>.
  </summary>

<br>

```csharp
public readonly struct Subscription<T1, T2, T3, T4> : IDisposable
```

- **Type parameters:**
    - `T1` — The type of the first emitted value.
    - `T2` — The type of the second emitted value.
    - `T3` — The type of the third emitted value.
    - `T4` — The type of the fourth emitted value.

---

### 🏗️ Constructors

#### `Subscription(ISignal<T1, T2, T3, T4>, Action<T1, T2, T3, T4>)`

```csharp
public Subscription(ISignal<T1, T2, T3, T4> signal, Action<T1, T2, T3, T4> action)
```

- **Description:** Initializes a new subscription for a signal emitting four values.
- **Parameters:**
    - `signal` — The signal source.
    - `action` — The delegate to unsubscribe on disposal.

---

### 🏹 Methods

#### `Dispose()`

```csharp
public void Dispose()
```

- **Description:** Unsubscribes the associated action from the signal source.

---

### 🗂 Example of Usage

```csharp
//Assume we have a instance of ISignal
ISignal<T1, T2, T3, T4> signal = ...
    
//Subscribe on the signal
Subscription<T1, T2, T3, T4> subscription = signal.Subscribe<T1, T2, T3, T4>(lambda);

// Later, dispose to unsubscribe
subscription.Dispose();
```

</details>

