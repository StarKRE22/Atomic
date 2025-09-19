# 📌 File System Organization
Here, I would like to share how I organize my project file system using the `Atomic Framework`, so they can be easily scaled, and the file structure remains as intuitive as possible.

> [!TIP]
> **As an example, you can check out the prototype game `Top Down Shooter` in the [Game Examples](../../README.md/#-game-examples) section**

## 📂 1. Organize Assets Folder

Usually, at the project level inside the `Assets` folder, I create a separate `Game` folder. This helps keep the `Assets` folder from becoming cluttered when adding various plugins.

```
Assets/
│
├─ 3rdParty/                 # Third-party libraries and plugins                 
├─ Game/                     # Game-specific scenes, scripts, and assets             
├─ Modules/                  # Reusable features not tied to a specific game
├─ Plugins/                  # DLLs and plugins
├─ Settings/                 # Project configurations, settings ScriptableObjects
├─ Tools/                    # Internal tools and editor scripts
```

## 📂 2. Organize Game Folder

Inside the `Game` folder, I create subfolders like `Scripts`, `Materials`, `Prefabs`, `Animations`, `Scenes`, `Configs`, and `Audio`. This is important because, when working in a team with artists, they can intuitively place their assets in the appropriate folders.

```
Game/
│
├─ Animations/            
├─ Configs/               
├─ Materials/             
├─ Prefabs/               
├─ Scenes/                
├─ Scripts/     
```

## 📂 3. Organize Scripts Folder

Regarding the `Scripts` folder, I usually create a `.asmdef` file so that the code can later be covered with tests. Inside `Scripts`, I create two subfolders: `App` and `Gameplay`.

```
 Assets/
│ 
├─ Game/                     # Game-specific scenes, scripts, and assets
│  ├─ Scripts/               
│  │  ├─ App/                
│  │  │  ├─ Core/            # Core application system features
│  │  │  └─ UI/              # Menu user interface
│  │  └─ Gameplay/           
│  │     ├─ Common/          # Common utilities and components for gameplay 
│  │     ├─ GameContext/     # Game system features
│  │     ├─ GameEntity/      # Game Objects (NPC, объекты)
│  │     ├─ PlayerContext/   # Player system features
│  │     ├─ UI/              # Game UI context features
```

The `App` folder contains the entire system, divided into `Core` and `UI` (for menus), while the `Gameplay` folder is organized by game features and mechanics.

Since the `Gameplay` folder contains different entity contexts—`GameContext`, `PlayerContext`, `GameEntity`, `GameUIContext`—it's better to create a separate folder for each type of entity. This makes it more intuitive to understand which layer a mechanic belongs to.

Within each folder, you can then organize content further by features.

## 📂 4. Organize Game Entity Folder

When it comes to **game entities**, I organize them into three main folders: `Core`, `View`, and `Content`.

- **Core** – contains reusable mechanics like `Move`, `Rotation`, and `Jump` that can be attached to any entity.
- **View** – holds visual mechanics and components, such as animations, effects, or UI related to the entity.
- **Content** – represents high-level implementations of specific game objects, such as `Character`, `PickUpItem`, `Enemy`, and so on.

This separation helps to keep **logic, visuals, and concrete entities** clearly organized and makes the system scalable and easy to maintain.

```
GameEntities/
│
├─ Core/                 # Core mechanics that can be attached to any entity
│  ├─ Move/              # Movement logic
│  ├─ Rotation/          # Rotation logic
│  └─ Jump/              # Jump logic
│
├─ View/                 # Visual mechanics and components
│  ├─ Move/              # Movement visualization
│  ├─ Rotation/          # Rotation visualization
│  └─ Jump/              # Jump visualization
│
└─ Content/              # High-level implementation of game objects
   ├─ Character/         # Specific character implementations
   ├─ PickUps/           # Pick-up items
   ├─ Enemies/           # Enemy entities
   ├─ Projectiles/       # Projectiles
   ├─ Weapons/           # Weapons
```

## 📂 5*. Organize Modules Folder

The `Modules` folder is where developers can place **universal features** that are not tightly tied to a specific project. These modules can be reused across multiple projects, making development more efficient and reducing duplicated work. 

Examples include dialogue systems, pathfinding, upgrade systems, grid-based inventories, and so on.

```
Assets/
│
├─ Modules/              
   ├─ DialogueSystem/    # Dialogue and conversation system
   ├─ UpgradeFeature/    # Character/item upgrade system
   ├─ BehaviourTree/     # AI behavior trees
   ├─ GridInventory/     # Grid-based inventory system

```

This approach ensures that **reusable systems are separated from project-specific content**, making it easier to maintain, test, and extend them across different games.

<!---
TODO: простые правила, что делает механика, в том домене она и лежит
TODO: Как папка называется, то такой же UseCase
--->