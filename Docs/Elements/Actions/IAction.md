# 🧩 IAction

Represents a <b>parameterless executable action</b>.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)

---

## 🗂 Example of Usage

```csharp
public sealed class HelloWorldAction : IAction
{
    public void Invoke() 
    {
        Console.WriteLine("Hello World!");  
    } 
}
```

```csharp
// Usage
IAction action = new HelloWorldAction();
action.Invoke(); 

// Output: Hello World!
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
public interface IAction
```

---

### 🏹 Methods

#### `Invoke()`

```csharp
public void Invoke();
```

- **Description:** Executes the action logic