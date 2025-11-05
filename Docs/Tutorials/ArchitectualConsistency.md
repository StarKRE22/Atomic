# 📖 Architectural Consistency

Entities are not limited to game objects — they can also serve as game systems, user interface elements, or even the
global application context. To define an entity’s domain in Unity, you simply inherit from the base
class [SceneEntity](../Entities/Entities/SceneEntity.md)
and fill it with the necessary data and logic.

It looks like this:

```csharp
public class GameContext : SceneEntity
{  
}
```

For example, in a complete prototype with a menu and levels, you can define the following domains:

- **Gameplay**
    - `GameEntity` — the base game entity that moves around the scene.
    - `GameContext` — stores the core game state and rules.
    - `PlayerContext` — holds player state and attributes (useful in multiplayer scenarios).
- **Application**
    - `AppContext` — manages application-level logic such as saving and loading data, handling level progression between
      sessions, quitting the game, and other global mechanics.
- **User Interface**
    - `GameUI` — game interface elements like the HUD and pop-up windows.
    - `MenuUI` — menu elements such as the main menu, loading screens, settings, and level selection.

Regardless of the domain, the process of populating entities with data and logic remains the same. Below are some
examples:

```csharp
// Game Context
public sealed class GameContextInstaller : SceneEntityInstaller<GameContext>
{
    [SerializeField] private Transform _worldTransform;
    [SerializeField] private TeamCatalog _teamCatalog;
    [SerializeField] private EntityPool _bulletPool;

    public override void Install(GameContext context)
    {
        context.AddPlayers(new Dictionary<TeamType, IPlayerContext>());
        context.AddWorldTransform(_worldTransform);
        context.AddTeamCatalog(_teamCatalog);
        context.AddBulletPool(_bulletPool);
        context.AddGameOverEvent(new BaseEvent());
    }
}
```

```csharp
// Game UI
public sealed class GameUIInstaller : EntityInstaller<GameUI>
{
    [SerializeField] private CountdownView _countdown;
    [SerializeField] private ScoreView _score;

    public override void Install(GameUI ui)
    {   
        // Countdown
        ui.AddCountdownView(_countdown);
        ui.AddBehaviour<CountdownPresenter>();

        // Score
        ui.AddScoreView(_score);
        ui.AddBehaviour<ScorePresenter>();
    }
}
```

```csharp
// Application Context
public sealed class AppContextInstaller : SceneEntityInstaller<AppContext>
{
    [Header("Quit")]
    [SerializeField] private KeyCode _exitKey = KeyCode.Escape;

    [Header("Levels")]
    [SerializeField] private Const<int> _startLevel;
    [SerializeField] private Const<int> _maxLevel;
    [SerializeField] private ReactiveVariable<int> _currentLevel = 1;

    public override void Install(AppContext context)
    {
        // Quit
        context.AddExitKeyCode(new Const<KeyCode>(_exitKey));
        context.AddBehaviour<QuitController>();

        // Level System
        context.AddStartLevel(_startLevel);
        context.AddMaxLevel(_maxLevel);
        context.AddCurrentLevel(_currentLevel);
        context.AddBehaviour<LevelSaveLoadController>();

        // Menu
        context.AddBehaviour<MenuLoadController>();
    }
}
```

This approach allows you to describe all layers of the project using a unified architecture, regardless of their role. The developer no longer needs to worry about organizing “managers” or constantly refactoring the code.

---

<p align="center">
<a href="InteractionsBetweenEntities.md">Move Next</a> •
<a href="https://github.com/StarKRE22/Atomic/issues">Report Issue</a> •
<a href="https://github.com/StarKRE22/Atomic/discussions">Join Discussion</a>
</p>