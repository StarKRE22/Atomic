# 🧩 IAction

Represents a <b>parameterless executable action</b>.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Invoke()](#invoke)

---

## 🗂 Example of Usage

Below is an example of using an `IAction` interface:

```csharp
public sealed class HelloWorldAction : IAction
{
    public void Invoke() 
    {
        Console.WriteLine("Hello World!");  
    } 
}
```

Usage:

```csharp
IAction action = new HelloWorldAction();
action.Invoke(); 

// Output: Hello World!
```

---

## 🔍 API Reference

Below is full information about `IAction` interface

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IAction
```

- **Description:** Represents a <b>parameterless executable action</b>.

---

### 🏹 Methods

#### `Invoke()`

```csharp
public void Invoke();
```

- **Description:** Executes the action logic