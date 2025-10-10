# The Problem

When a developer use [entities](../Entities/Manual.md) and works with tags and values, they typically need to reference
them via `int` keys or `string` names.

For example:

```csharp
// Define tag keys
const int PlayerTag = 1;
const int NPCTag = 2;

// Define value keys
const int Health = 1;
const int Speed = 2;

Entity entity = new Entity();

entity.AddTag(PlayerTag);
entity.AddTag(NPCTag);

// Add health property
entity.AddValue(Health, 100);

// Get a value
int health = entity.GetValue<int>(Health);
```

or something like this:

```csharp
Entity entity = new Entity();

entity.AddTag("Player");
entity.AddTag("NPCTag");

// Add health property
entity.AddValue("Health", 100);

// Get a value
int health = entity.GetValue<int>("Health");
```

This approach results in **hardcoded strings** and **magic constants** tightly coupled with the code. Maintaining and
refactoring such code becomes difficult. Additionally, the developer must know the type of each value in advance, which
can lead to runtime errors. Entity API solves this problem with **code generation**, ensuring type safety and removing hardcoded constants.

---
Next: [Entity API Generation](ApiGeneration.md)
