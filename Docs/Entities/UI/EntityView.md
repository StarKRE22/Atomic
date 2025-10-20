# 🧩 EntityView

```csharp
public class EntityView : EntityView<IEntity>
```

- **Description:** Default entity view component.  
  A **non-generic wrapper** around `EntityView<E>` fixed to [`IEntity`](../Entities/IEntity.md).
- **Inheritance:** [EntityView\<E>](EntityView%601.md), `MonoBehaviour`
- **Usage:** Useful when the exact entity type is unknown or irrelevant (e.g., working with heterogeneous entities).

---

## 🏹 Static Methods

### `Create(CreateArgs)`

```csharp
public static EntityView Create(in CreateArgs args = default);
````

- **Description:** Creates a new `EntityView` `GameObject` and sets up its installers.
- **Parameter:** `args` — Arguments for configuring the new view (see [`CreateArgs`](EntityViewE.md#createargs)).
- **Returns:** A new `EntityView` instance.
- **Details:**
  - Calls the generic factory method `Create<EntityView>(args)`.
  - Automatically applies provided settings (`name`, `installers`, `controlGameObject`, gizmo options).

---

## 🗂 Example of Usage

```csharp
// Create a default entity view without knowing the specific entity type
var args = new EntityView.CreateArgs
{
    name = "GenericEntityView",
    controlGameObject = true,
    installers = new List<SceneEntityInstaller>()
};

EntityView view = EntityView.Create(args);

// Show any IEntity instance
IEntity entity = new SomeEntity();
view.Show(entity);

// Later, hide or destroy
view.Hide();
EntityView.Destroy(view);
```


