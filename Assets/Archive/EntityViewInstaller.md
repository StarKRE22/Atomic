# 🧩 EntityViewInstaller

Base class for components that handle the **installation of `EntityView` instances**.

> [!NOTE]  
> Inherit from this class to implement custom logic that configures `EntityView` when it is shown.

---

## EntityViewInstaller
```csharp
public abstract class EntityViewInstaller : MonoBehaviour
```
- Serves as a base for all components that install behaviours or other configurations onto `EntityView`.
- Must be attached to a `GameObject` in the scene to function.

---

## Methods

### Install
```csharp
public abstract void Install(EntityView view)
```
- Performs the installation logic for a specific `EntityView`.
- Must be **implemented in derived classes**.
- **Parameters:**
    - `view` — The `EntityView` instance to install.
- Called automatically by `EntityView` during `OnShow`, but only once per view.

---

## Example Usage

```csharp
public class SoldierInstaller : EntityViewInstaller
{
    [Serializable] private Animator _animator;
    [Serializable] private AudioSource _audioSource;
    [Serializable] private Image _healthBar;
    
    public override void Install(EntityView view)
    {
        view.AddBehaviour(new MovementAnimBehaviour(_animator));
        view.AddBehaviour(new FireAudioBehaviour(_audioSource);
        view.AddBehaviour(new HealthBarBehaviour(_healthBar));
    }
}
```