# 🧩 IAction Interfaces

The **IAction** interfaces define a family of contracts for executing parameterized actions. They provide a lightweight abstraction for invoking logic, often used in event systems, command patterns, or reactive programming.

---
## 🧩 IAction
```csharp
public interface IAction
```
- **Description:** Represents a **parameterless executable action**.

### Methods

#### `Invoke()`
```csharp
void Invoke();
```
- **Description:** Executes the action logic

---

## 🧩 IAction&lt;T&gt;
```csharp
public interface IAction<in T>
```
- **Description:** Represents an executable action that takes one argument.
- **Type parameter** `T` — the input parameter

### Methods
#### `Invoke(T)`
```csharp
void Invoke(T arg);
```
- **Description:** Executes the action with the specified argument
- **Parameter:** `arg` — the input parameter

---

## 🧩 IAction<T1, T2>
```csharp
public interface IAction<in T1, in T2>
```
- **Description:** Represents an executable action that takes two arguments.
- **Type parameters** 
  - `T1` — the first argument
  - `T2` — the second argument

### Methods
#### `Invoke(T1, T2)`
```csharp
void Invoke(T1 arg1, T2 arg2);
```
- **Description:** Executes the action with the specified arguments
- **Parameters:** 
  - `arg1` — the first argument
  - `arg2` — the second argument

---

## 🧩 IAction<T1, T2, T3>
```csharp
public interface IAction<in T1, in T2, in T3>
```
- **Description:** Represents an executable action that takes three arguments.
- **Type parameters**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument

### Methods
#### `Invoke(T1, T2, T3)`
```csharp
void Invoke(T1 arg1, T2 arg2, T3 arg3);
```
- **Description:** Executes the action with the specified arguments
- **Parameters:**
    - `arg1` — the first argument
    - `arg2` — the second argument
    - `arg3` — the third argument

---

## 🧩 IAction<T1, T2, T3, T4>
```csharp
public interface IAction<in T1, in T2, in T3, in T4>
```
- **Description:** Represents an executable action that takes four arguments.
- **Type parameters**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument
    - `T4` — the fourth argument

### Methods
#### `Invoke(T1, T2, T3, T4)`
```csharp
void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
```
- **Description:** Executes the action with the specified arguments
- **Parameters:**
    - `arg1` — the first argument
    - `arg2` — the second argument
    - `arg3` — the third argument
    - `arg4` — the fourth argument