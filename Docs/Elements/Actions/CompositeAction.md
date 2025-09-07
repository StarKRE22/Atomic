# 🧩 CompositeAction

`CompositeAction` represents a **group of actions** that implement the `IAction` interface.  
When invoked, it executes all contained actions **sequentially** in the order they were added.

---

## Features

- Groups multiple `IAction` instances into a single composite.
- Invokes all actions in the sequence when `Invoke()` is called.
- Useful for triggering multiple behaviors with a single event.
- Supports up to **four generic parameters** (`CompositeAction<T1, T2, T3, T4>`).

---

## Constructors

```csharp
public CompositeAction(params IAction[] actions)
public CompositeAction(IEnumerable<IAction> actions)
```
- CompositeAction(params IAction[] actions) – initializes with a variable number of actions.
- CompositeAction(IEnumerable<IAction> actions) – initializes with a collection of actions.

## Methods
```csharp
public void Invoke()
```
- Description: Executes all contained actions in order.
- Returns: Nothing.

## Example Usage
```csharp
var action1 = new InlineAction(() => Console.WriteLine("Action 1"));
var action2 = new InlineAction(() => Console.WriteLine("Action 2"));
var action3 = new InlineAction(() => Console.WriteLine("Action 3"));

// Create a composite action
var composite = new CompositeAction(action1, action2, action3);

// Invoke all actions at once
composite.Invoke();
// Output:
// Action 1
// Action 2
// Action 3
```