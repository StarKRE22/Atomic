# 🧩 EntityUtils

```csharp
public static class EntityUtils
`````

- **Description:** Provides **utility methods** for checking the current Unity application mode.  
  Useful for conditional logic that depends on whether the game is running in **Play Mode** or **Edit Mode**.

---

## 🏹 Methods

#### `IsPlayMode()`

```csharp
public static bool IsPlayMode()
````

- **Description:** Determines whether the application is currently in **Play Mode**.
- **Returns:**
    - `true` if the application is in Play Mode.
    - `false` otherwise.
    - In builds (outside the editor), always returns `true`.

#### `IsEditMode()`

```csharp
public static bool IsEditMode()
````

- **Description:** Determines whether the application is currently in **Edit Mode** and not compiling.
- **Returns:**
    - `true` if the application is in Edit Mode and not compiling.
    - `false` otherwise.
    - In builds (outside the editor), always returns `false`.

---

## 🗂 Examples of Usage

#### `IsPlayMode()`

```csharp
if (EntityUtils.IsPlayMode())
    Console.WriteLine("Application is running in Play Mode.");
````

#### `IsEditMode()`


```csharp
if (EntityUtils.IsEditMode())
    Console.WriteLine("Application is in Edit Mode and not compiling.");
````
