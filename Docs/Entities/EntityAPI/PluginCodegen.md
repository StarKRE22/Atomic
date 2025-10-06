# Generating API through Rider Plugin

This section describes how to use code generation of extension methods for entities through
the [Rider Plugin](https://plugins.jetbrains.com/plugin/28321-atomic). Also, you can install plugin
from [GitHub](https://github.com/Prylor/atomic-rider-plugin).
---

## Usage

1. Right-click on your project folder
2. Select `New` → `Atomic File`
3. Configure your entity API
4. **Press `Ctrl+Shift+G` to generate the C# file for the first time**
5. After initial generation, changes will auto-update the file (if enabled)

```yaml
entityType: IEntity
namespace: MyGame.Components
className: EntityExtensions
directory: Assets/Scripts
aggressiveInlining: true
unsafe: false

imports:
  System
  UnityEngine
  Atomic.Entities

tags:
  Player
  Enemy
  Projectile

values:
  Health: int
  Position: Vector3
  Damage: float
```

### Generating Code

- **Manual Generation**: Press `Ctrl+Shift+G` while in an `.atomic` file
    - **Required for first-time generation** - Creates the initial C# file
    - Can be used anytime to force regeneration
- **Automatic Regeneration**: Updates existing C# files automatically when you save changes
    - **Only works for existing files** - Won't create new files
    - Enable/disable in plugin settings

### Generated Methods

For **Tags**:

- `HasPlayerTag()` - Check if entity has tag
- `AddPlayerTag()` - Add tag to entity
- `DelPlayerTag()` - Remove tag from entity

For **Values**:

- `GetHealth()` - Get value
- `SetHealth(int value)` - Set value
- `AddHealth()` - Add component
- `HasHealth()` - Check if component exists
- `DelHealth()` - Remove component
- `TryGetHealth(out int value)` - Try get value
- `RefHealth()` - Get reference (if unsafe enabled)

---

## Configuration

### Plugin Settings

Access settings via `File` → `Settings` → `Tools` → `Atomic Plugin`

- **Auto-generate**: Enable/disable automatic regeneration
- **Debounce delay**: Set delay before auto-generation (ms)
- **Show notifications**: Toggle generation notifications

### Atomic File Properties

| Property             | Description                                 | Required |
|----------------------|---------------------------------------------|----------|
| `entityType`         | Base entity type (e.g., Entity, GameObject) | Yes      |
| `namespace`          | C# namespace for generated code             | Yes      |
| `className`          | Name of the generated static class          | Yes      |
| `directory`          | Output directory (relative to project)      | No       |
| `aggressiveInlining` | Enable aggressive inlining optimization     | No       |
| `unsafe`             | Enable unsafe code for ref returns          | No       |