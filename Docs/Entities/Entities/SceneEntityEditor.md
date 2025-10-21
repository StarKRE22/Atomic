# 🧩 SceneEntity Editor

Provides editor-time lifecycle support, including optimization, auto-refresh, edit-mode installation, and simulated
lifecycle events. Provides a simple workflow for <b>precomputing entity capacities</b> in the Unity Editor.
You can optimize your entity’s size by precomputing the capacity of <b>tags</b>, <b>values</b>, and <b>behaviours</b>.

> [!TIP]
> After adding installers and configuring your entity, you can use the `Compile` option in the context menu. This will
> initialize your entity in **Edit Mode** and determine the exact memory requirements. To reset the entity state, use
> the
> `Reset` button in the context menu.

---


## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [Inspector Settings](#-inspector-settings)
  - [Parameters](#-parameters)
  - [Context Menu](#-context-menu)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
      - [Reset](#reset)


---

## 🗂 Examples of Usage

Below are screenshoots of `Compile` and `Reset` options in the Unity inspector of `SceneEntity`:

<img width="144" height="" alt="изображение" src="../../Images/Scene%20Entity%20Reset%20and%20Compile.png" />
<img width="320" height="" alt="изображение" src="../../Images/SceneEntity%20Optimization.png" />


---

## 🛠 Inspector Settings

### 🎛 Parameters

| Parameters    | Description                                                                                                                                                                                                                            | 
|---------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `autoCompile` | If enabled, `Install()` is called every time `OnValidate` is invoked in Edit Mode. Default is `false`. <br/>**Warning:** If you create Unity objects or other heavy objects in `Install()`, turn this off to avoid performance issues. |

### ⚙️ Context Menu

| Option    | Description                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             |
|-----------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Compile` | Fully compiles the entity state.<br><br>**Steps:**<br>1. Disables and disposes the entity in Edit mode if the GameObject is not a prefab. *(Applies only to behaviours with [RunInEditModeAttribute](../Attributes/RunInEditModeAttribute.md))*<br>2. Uninstalls the previous entity state.<br>3. Installs the new entity state.<br>4. Precomputes **capacity**, **tags**, **values**, and **behaviours** of the entity.<br>5. Initializes and enables the entity in Edit mode if the GameObject is not a prefab. *(Applies only to behaviours with [RunInEditModeAttribute](../Attributes/RunInEditModeAttribute.md))* |
| `Reset`   | Fully resets the entity state.<br><br>**Steps:**<br>1. Disables and disposes the entity in Edit mode if the GameObject is not a prefab. *(Applies only to behaviours with [RunInEditModeAttribute](../Attributes/RunInEditModeAttribute.md))*<br>2. Uninstalls the previous entity state.<br>3. Resets all parameters to default.<br>4. Gathers all `SceneEntityInstallers` and child entities.                                                                                                                                                                                                                         |

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public partial class SceneEntity
```

---

### 🏹 Methods

#### `Reset()`

```csharp
private void Reset();
```

- **Description:** Fully resets entity state
- **Behaviour**:
    1. Disable and Dispose entity in Edit mode if gameObject is not prefab. Only for behaviours
       with [RunInEditModeAttribute](../Attributes/RunInEditModeAttribute.md)
    2. Uninstall previous entity state
    3. Resets all parameters to default
    4. Gathers all SceneEntityInstallers and child Entities