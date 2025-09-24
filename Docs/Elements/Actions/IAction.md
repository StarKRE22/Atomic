# 🧩 IAction

```csharp
public interface IAction
```
- **Description:** Represents a <b>parameterless executable action</b>.
---

## 🏹 Methods

#### `Invoke()`

```csharp
public void Invoke();
```

- **Description:** Executes the action logic

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
action.Invoke(); // Output: Hello World!
```
