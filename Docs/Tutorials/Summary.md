# 📖 Summary

---

## 📑 Table of Contents

- [Benefits](#benefits)
- [Limitations](#limitations)
- [Comparison with OOP and ECS](#comparison-with-oop-and-ecs)
- [Conclusion](#conclusion)

---

## Benefits

### 1. Low Entry Barrier

Getting started with Atomic is extremely simple. You no longer need to spend time organizing classes, selecting
patterns, or building inheritance hierarchies. You just create an entity, fill it with atomic data elements, and
describe interaction mechanics in separate controllers. The main rule: always maintain a clear separation between data
and logic.

### 2. Unified Architecture

The architecture follows a single ESB pattern, making all systems consistent, predictable, and easy to read. This
approach enforces a clear design and prevents unpredictable or chaotic solutions in the code.

### 3. Ready-to-Use Atomic Elements

Instead of writing traditional components for game objects, the framework already relies on universal atomic elements.
This speeds up development, improves reliability, minimizes code duplication, and reduces the number of files in the
project.

### 4. Reusable Mechanics

A key feature of the atomic approach is mechanic reusability. For example, a movement mechanic implemented once can be
used for both a character and a bullet, as long as both entities contain the required data.

### 5. Dynamic Mechanics

Players love responsive and changing worlds — and the framework supports exactly that. The architecture allows an
entity’s structure to change at runtime by adding or removing data and logic. With this approach, you can, for instance,
“turn a warrior into a sheep” just by changing its data and behaviours — without recreating the object or introducing
complex dependencies.

### 6. High Performance

Optimized data access enables efficient entity processing every frame, ensuring stability even under heavy load.
Additionally, avoiding heavy MonoBehaviours saves memory and reduces Unity’s object management overhead.

### 7. Compatibility with OOP

If you already have a game built with an OOP architecture, Atomic can be integrated seamlessly without major
refactoring. You can simply start creating entities without modifying your existing codebase.

### 8. Minimizing Unity Dependency

Although the framework was designed for Unity, it can also be used to create games in pure C#. This simplifies testing,
reduces dependency on Unity’s Inspector, and allows developers to focus entirely on writing code.

> It’s also worth mentioning that there is a version of the framework that works entirely without Unity, making it a
> cross-platform tool suitable for a wide variety of projects. You can find it in the framework’s release section.

---

## Limitations

The framework isn’t perfect, so below are its main limitations that developers should be aware of before starting to
work with it.

### 1. Lack of Encapsulation

All entity data is shared, meaning any developer can modify it directly from anywhere in the program. This increases the
risk of bugs and makes error tracking more difficult, especially in team-based projects.

### 2. Centralized Data

Unlike decentralized OOP architectures, in Atomic, all developers work with entities that act as centralized data
registries. As a result, many business logic components may reference the same atomic element. Removing such an element
from an entity can break the entire project. In team development, this also increases the risk of conflicts and requires
strict discipline.

### 3. Hybrid Approach

The atomic approach combines concepts from OOP and ECS, giving developers great flexibility in how they write code.
However, without strict adherence to the ESB pattern, a project can quickly become chaotic, complicating maintenance and
further development.

> To prevent the project from turning into “spaghetti code,” it’s recommended to follow this key principle: State —
> modular data objects, Behaviour — the business logic that processes them.

---

## Comparison with OOP and ECS

Below is a comparison between the Atomic approach and the concepts of ECS and OOP:

| **Aspect**         | **Atomic Approach (Entity–State–Behaviour)**                            | **ECS (Entity–Component–System)**                                       | **OOP (Object-Oriented Programming)**                                          |
|--------------------|-------------------------------------------------------------------------|-------------------------------------------------------------------------|--------------------------------------------------------------------------------|
| **Structure**      | Entity acts as a container for data (State) and logic (Behaviour).      | Entity is an identifier; Components store data; Systems contain logic.  | Classes and objects contain both fields and methods.                           |
| **Data**           | Split into atomic elements — constants, variables, events, and actions. | Stored in components, separate from logic.                              | Data and methods are often tightly coupled within the same object.             |
| **Logic**          | Behaviour operates on State and can be reused across entities.          | Systems process components and are often optimized for CPU performance. | Class methods operate on their own data; logic is distributed among classes.   |
| **Reusability**    | High — mechanics can be easily “plugged” into different entities.       | High — logic is separated from data; polymorphism through composition.  | Depends on design; often relies on inheritance or polymorphism.                |
| **Testing**        | Fully in C#, independent of Unity — easy to write unit tests.           | System testing possible but depends on the specific ECS framework.      | Usually tied to MonoBehaviour; DI frameworks can simplify testing.             |
| **Performance**    | Above average — optimized entity and world management.                  | Very high — especially with a large number of entities.                 | Moderate — depends on the number of MonoBehaviour components and update calls. |
| **Flexibility**    | High — easy to add atomic elements and behaviours.                      | High — components can be easily added or removed.                       | Limited — changes often require rewriting classes or methods.                  |
| **Learning Curve** | Medium — requires understanding of atomic elements and the ESB pattern. | High — requires a shift toward data pipelines and system-based logic.   | Low — familiar to most developers.                                             |

---

## Conclusion

The Atomic approach to game development offers a modern and flexible architecture that combines the best aspects of OOP
and ECS, while simplifying the developer’s workflow. It preserves object-oriented polymorphism and, at the same time,
ensures modularity of game mechanics through a clear separation of data and logic.

A unified architecture allows developers to assemble game systems “like building blocks” without extensive upfront
design, letting them focus on implementing business logic.

Atomic is already being used in mobile and web projects. However, this approach requires discipline when scaling a
project and strict adherence to the ESB (Entity–State–Behaviour) pattern to keep the codebase predictable and
maintainable.

---

<p align="center">
<a href="https://github.com/StarKRE22/Atomic/issues">Report Issue</a> •
<a href="https://github.com/StarKRE22/Atomic/discussions">Join Discussion</a>
</p>