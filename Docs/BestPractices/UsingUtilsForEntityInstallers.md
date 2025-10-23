# 📌 PlayMode & EditMode for EntityInstallers

Entity installers can be configured to behave differently depending on whether Unity is in **PlayMode** or **EditMode**.
Using [AtomicUtils.IsPlayMode()](../Elements/Utils/AtomicUtils.md#isplaymode), you can ensure that certain
runtime-specific logic runs only during play, while editor-only configuration or setup remains safe for editing.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [Conclusion](#-conclusion)
- [Benefits](#-benefits)

---

## 🗂 Example of Usage

This example demonstrates an entity installer that spawns a character prefab **only in PlayMode**, while still
configuring entity behaviors regardless of mode:

```csharp
[Serializable]
public sealed class CharacterSystemInstaller : IEntityInstaller<IPlayerContext>
{
    [SerializeField]
    private GameEntity _characterPrefab;

    public void Install(IPlayerContext context)
    {
        if (AtomicUtils.IsPlayMode())
        {
            GameContext gameContext = GameContext.Instance;
            GameEntity character = CharacterUseCase.Spawn(context, gameContext, _characterPrefab);
            context.AddCharacter(character);
            gameContext.WhenDisable(character.Disable);
        }

        context.AddBehaviour<CharacterMoveController>();
        context.AddBehaviour<CharacterFireController>();
        context.AddBehaviour<CharacterMoveController>();
        context.AddBehaviour<CharacterRespawnController>();
    }
}
```

> [!TIP]
> Use `AtomicUtils.IsPlayMode()` to **guard runtime-specific operations**, such as spawning prefabs or registering
> callbacks, while keeping installers safe to use in the editor.

---

## 🏁 Conclusion

- Checking [AtomicUtils.IsPlayMode()](../Elements/Utils/AtomicUtils.md#isplaymode) allows installers to **separate runtime behavior from editor setup**.
- Ensures that **PlayMode-specific logic** (like spawning characters or initializing runtime-only systems) does not
  execute in EditMode.
- Supports **modular entity configuration**, where behaviors are always added, but runtime objects are instantiated
  conditionally.
- Promotes **safe, predictable, and maintainable entity installers** in both PlayMode and EditMode.

---

## ✅ Benefits

- Prevents **runtime-only operations from executing in EditMode**, avoiding errors and unwanted side effects.
- Supports **hybrid installers** that work in both PlayMode and EditMode.
- Keeps **entity configuration modular** and predictable.
- Enables **safe scene setup** while preserving runtime initialization.
- Enhances **maintainability** by clearly separating editor logic from play logic.