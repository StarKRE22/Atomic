# 🧩 SceneActions Abstract

Define **scene-based actions** in Unity that implement the corresponding [IAction](IActions.md) interfaces.
These abstract classes inherit from `MonoBehaviour`, allowing actions to be attached to GameObjects in a scene.
They serve as a base for **custom scene logic** and are designed to be subclassed to implement specific behavior.

> [!TIP]
> Extremely useful for cutscenes, trigger-based actions, level initialization, and similar scene-driven logic.

There are several classes of abstract scene actions, depending on the number of arguments the actions take:

- [SceneActionAbstract](SceneActionAbstract.md) — Non-generic version; works without parameters.
- [SceneActionAbstract&lt;T&gt;](SceneActionAbstract%601.md) — Action that takes one argument.
- [SceneActionAbstract&lt;T1, T2&gt;](SceneActionAbstract%602.md) — Action that takes two arguments.
- [SceneActionAbstract&lt;T1, T2, T3&gt;](SceneActionAbstract%603.md) — Action that takes three arguments.
- [SceneActionAbstract&lt;T1, T2, T3, T4&gt;](SceneActionAbstract%604.md) — Action that takes four arguments.

---


## 🗂 Examples of Usage

### Non-generic action

```csharp
public sealed class HelloWorldAction : SceneActionAbstract
{
    public override void Invoke() 
    {
        Console.WriteLine("Hello World!");  
    } 
}
```

---

### Action with one parameter

```csharp
public sealed class DestroyGameObjectAction : SceneActionAbstract<GameObject>
{
    public override void Invoke(GameObject go) 
    {
        GameObject.Destroy(go);  
    } 
}
```

---

### Action with two parameters

```csharp
public sealed class DealDamageAction : SceneActionAbstract<Character, int>
{
    public override void Invoke(Character character, int damage) 
    {
        character.TakeDamage(damage);
    } 
}
```