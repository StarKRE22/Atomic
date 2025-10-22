# 📌 File System Organization

In this guide, I’ll share how I organize my project’s file system when working with the **Atomic Framework**.
This approach keeps the project **scalable**, **clean**, and **intuitive**, even as it grows in complexity.

---

## 📑 Table of Contents

- [Organize Assets Folder](#-organizing-the-assets-folder)
- [Organize Game Folder](#-organizing-the-game-folder)
- [Organize Scripts Folder](#-organizing-the-scripts-folder)
- [Organize GameEntities Folder](#-organizing-the-gameentities-folder)
- [Organize GameContext & PlayerContext Folders](#-organizing-the-gamecontext--playercontext-folders)
- [Organize Modules Folders](#-organizing-the-gamecontext--playercontext-folders)
- [Summary](#-summary)

---

## 📂 Organizing the Assets Folder

At the root of the project, inside the `Assets` folder, create a separate **Game** directory.  
This keeps the main `Assets` folder tidy and prevents clutter when adding plugins or tools.

```
Assets/
│
├─ 3rdParty/ # Third-party libraries and plugins                 
├─ Game/ # Game-specific scenes, scripts, and assets             
├─ Modules/ # Reusable systems shared across projects
├─ Plugins/ # DLLs and external plugins
├─ Settings/ # Project settings, ScriptableObjects, configurations
├─ Tools/ # Editor tools and internal utilities
```

---

## 📂 Organizing the Game Folder

Inside the `Game` folder, group assets by type — `Scripts`, `Prefabs`, `Materials`, `Animations`, etc.
This ensures that artists and developers can intuitively find where each type of asset belongs. For tests, we recommend
creating a separate folder.

```
Game/
│
├─ Animations/            
├─ Audio/                 
├─ Configs/               
├─ Materials/             
├─ Prefabs/               
├─ Scenes/                
├─ Scripts/
├─ Tests/ 
```

This clear separation helps teams work together without confusion about where to place or find assets.

---

## 📂 Organizing the Scripts Folder

Inside the `Scripts` folder, create an **Assembly Definition (`.asmdef`)** file to make your code modular and easily testable. 
Then, organize your scripts into three main areas: `App`, `Gameplay`, and `UI`.

```
Assets/
│
├─ Game/
│ ├─ Scripts/
│ │ ├─ App/                    # Application-level systems
│ │ │ ├─ Bootstrap/            # Initialization and startup logic
│ │ │ ├─ SaveSystem/           # Save/load game data
│ │ │ ├─ LevelSystem/          # Level management and progression
│ │ │ ├─ Authorization/        # Login, authentication, permissions
│ │ │ └─ Quit/                 # Application quit/exit logic
│ │
│ │ ├─ Gameplay/               # All gameplay systems and mechanics
│ │ │ ├─ Common/               # Shared utilities and gameplay components
│ │ │ ├─ GameEntities/         # NPCs, items, objects, and other entities
│ │ │ ├─ GameContext/          # Core game rules and state management
│ │ │ ├─ PlayerContext/        # Player-related systems and mechanics
│ │ │ ├─ Weapons/              # Weapon scripts and logic
│ │ │ ├─ Abilities/            # Player and entity abilities
│ │ │ └─ Level/                # Level scripts, triggers, props, and interactive elements
│ │
│ │ └─ UI/                     # All UI elements for the project
│ │   ├─ MenuUI/               # Main menu, loading screens, settings
│ │   ├─ GameplayUI/           # In-game HUD, pop-ups, and in-game menus
│ │   └─ Common/               # Reusable UI elements (buttons, panels, fonts)
```

### Folder Roles

- **App** – Handles application-level systems such as bootstrapping, menu navigation, save/load, and other global services.
- **Gameplay** – Contains all gameplay-related logic, including entities, player systems, abilities, weapons, levels, and shared utilities.
- **UI** – Keeps all user interface elements in one place, separating menu screens from in-game HUD and common reusable components.

Structuring `Gameplay` this way ensures each domain (e.g., `PlayerContext`, `GameEntities`, `GameContext`) has its own folder, making it easier to locate and maintain specific systems or mechanics.

### Why This Structure Works

1. **Clear separation of concerns** – App, Gameplay, and UI are fully separated.
2. **Modular and scalable** – Easy to add new mechanics, weapons, levels, or UI without clutter.
3. **Supports testing and assembly definitions** – `.asmdef` files allow modular builds and unit tests.
4. **Reusability** – Common utilities and UI components can be reused across systems.

---

## 📂 Organizing the GameEntities Folder

For **game entities**, organize them into three main domains: `Core`, `View`, and `Content`. This separation ensures clean architecture, scalability, and easier maintenance.

- **Core** → Contains reusable gameplay mechanics and logic, such as movement, rotation, and jumping. This is independent of visuals.
- **View** → Handles all visual representation, including animations, effects, and UI elements that reflect Core mechanics.
- **Content** → Holds concrete game entities like characters, enemies, weapons, items, and projectiles, combining Core logic with View components.

```
GameEntities/
│
├─ Core/                   # Core gameplay mechanics
│ ├─ Move/                 # Movement logic
│ ├─ Rotation/             # Rotation logic
│ └─ Jump/                 # Jump logic
│
├─ View/                   # Visuals and presentation
│ ├─ Move/                 # Movement visuals
│ ├─ Rotation/             # Rotation visuals
│ └─ Jump/                 # Jump visuals
│
└─ Content/                # Concrete game entities
  ├─ Character/            # Player characters
  ├─ PickUps/              # Pick-up items
  ├─ Enemies/              # Enemy units
  ├─ Projectiles/          # Bullets, missiles, etc.
  └─ Weapons/              # Weapon implementations
```

### Benefits of This Structure

1. **Separation of concerns** – Core logic is independent of visuals, and Content only combines them.
2. **Scalable** – New mechanics, visual effects, or entities can be added without breaking existing systems.
3. **Easy to maintain** – Each layer has a clear responsibility, making debugging and testing simpler.
4. **Reusable** – Core mechanics can be reused across different entities or projects without duplication.

## 📂 Organizing the GameContext & PlayerContext Folders

Both `GameContext` and `PlayerContext` should be organized by **feature**. 
This makes it easier to locate, maintain, and expand individual systems.

Examples of features you might include:

- **Inventory** – Handles player items, pickups, equipment, and item management.
- **Score** – Tracks player progress, points, achievements, and leaderboards.
- **Controllers/** – Manages inputs of movement, jumping, rotation, and physics interactions.
- **Abilities/** – Manages player abilities and their cooldowns.

This **feature-based organization** ensures each system has a clear responsibility and makes the codebase modular and scalable.

---

## 📂 Organizing the Modules Folder

The **Modules** folder stores universal features and systems that can be reused across multiple projects. 
This prevents code duplication and speeds up future development.

```
Assets/
│
├─ Modules/              
│ ├─ DialogueSystem/ # Dialogue and conversation system
│ ├─ UpgradeFeature/ # Character/item upgrade system
│ ├─ BehaviourTree/ # AI behavior trees
│ ├─ GridInventory/ # Grid-based inventory system
```

Modules are designed to be **independent of specific game logic**,  
allowing you to easily plug them into any new project built on the same framework.

---

## ✅ Summary

| Folder      | Purpose                            |
|-------------|------------------------------------|
| **3rdParty** | External libraries and assets      |
| **Game**    | Project-specific assets and code   |
| **Modules** | Reusable, game-agnostic systems    |
| **Plugins** | DLLs and third-party plugins       |
| **Settings** | Configurations and global settings |
| **Tools**  | Internal and editor utilities      |

---

> [!NOTE]
> 🧩 Keep a simple rule in mind:  
> *A mechanic should always live in the domain it belongs to.* 
> The folder name should directly reflect the **use case** or **system** it represents.