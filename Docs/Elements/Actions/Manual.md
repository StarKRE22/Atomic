# 🧩 Actions

Provide a set of abstractions for defining and invoking logic with varying numbers of input parameters. These action
types are lightweight and flexible, making them ideal for use in **command patterns and object-oriented design**. They
allow developers to encapsulate behavior, combine multiple actions, or reference scene-specific
logic in a clean, reusable way.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [IAction](#iaction)
    - [InlineAction](#inlineaction)
    - [CompositeAction](#compositeaction)
    - [SceneActionAbstract](#sceneactionabstractt)
- [API Reference](#-api-reference)
- [Best Practices](#-best-practices)

---

## 🗂 Examples of Usage

Below are examples of using different action types depending on scenario:

#### `IAction`

```csharp
public sealed class HelloWorldAction : IAction
{
    public void Invoke() 
    {
        Console.WriteLine("Hello World!");  
    } 
}
```

#### `InlineAction`

```csharp
IAction action = new InlineAction(() => Console.WriteLine("Hello World!"));
action.Invoke(); // Output: Hello World!
```

#### `CompositeAction`

```csharp
IAction composite = new CompositeAction(
    new InlineAction(() => Console.WriteLine("Action 1")),
    new InlineAction(() => Console.WriteLine("Action 2"))
);

composite.Invoke();

// Output:
// Action 1
// Action 2
```

#### `SceneActionAbstract<T>`

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

## 🔍 API Reference

There are several abstractions of actions, depending on the number of arguments the actions take:

<ul>
 <li>
<details>
  <summary><a href="IActions.md">IActions</a></summary>
  <ul>
    <li><a href="IAction.md">IAction</a></li>
    <li><a href="IAction%601.md">IAction&lt;T&gt;</a></li>
    <li><a href="IAction%602.md">IAction&lt;T1, T2&gt;</a></li>
    <li><a href="IAction%603.md">IAction&lt;T1, T2, T3&gt;</a></li>
    <li><a href="IAction%604.md">IAction&lt;T1, T2, T3, T4&gt;</a></li>
  </ul>
</details>
 </li>
 <li>
<details>
  <summary><a href="InlineActions.md">InlineActions</a></summary>
  <ul>
    <li><a href="InlineAction.md">InlineAction</a></li>
    <li><a href="InlineAction%601.md">InlineAction&lt;T&gt;</a></li>
    <li><a href="InlineAction%602.md">InlineAction&lt;T1, T2&gt;</a></li>
    <li><a href="InlineAction%603.md">InlineAction&lt;T1, T2, T3&gt;</a></li>
    <li><a href="InlineAction%604.md">InlineAction&lt;T1, T2, T3, T4&gt;</a></li>
  </ul>
</details>
</li>

<li>
<details>
  <summary><a href="CompositeActions.md">CompositeActions</a></summary>
  <ul>
    <li><a href="CompositeAction.md">CompositeAction</a></li>
    <li><a href="CompositeAction%601.md">CompositeAction&lt;T&gt;</a></li>
    <li><a href="CompositeAction%602.md">CompositeAction&lt;T1, T2&gt;</a></li>
    <li><a href="CompositeAction%603.md">CompositeAction&lt;T1, T2, T3&gt;</a></li>
    <li><a href="CompositeAction%604.md">CompositeAction&lt;T1, T2, T3, T4&gt;</a></li>
  </ul>
</details>
</li>

<li>
<details>
  <summary><a href="SceneActionsAbstract.md">SceneActionsAbstract</a></summary>
  <ul>
    <li><a href="SceneActionAbstract.md">SceneActionAbstract</a></li>
    <li><a href="SceneActionAbstract%601.md">SceneActionAbstract&lt;T&gt;</a></li>
    <li><a href="SceneActionAbstract%602.md">SceneActionAbstract&lt;T1, T2&gt;</a></li>
    <li><a href="SceneActionAbstract%603.md">SceneActionAbstract&lt;T1, T2, T3&gt;</a></li>
    <li><a href="SceneActionAbstract%604.md">SceneActionAbstract&lt;T1, T2, T3, T4&gt;</a></li>
  </ul>
</details>
</li>

<li>
<details>
  <summary><a href="SceneActionsDefault.md">SceneActionsDefault</a></summary>
  <ul>
    <li><a href="SceneActionDefault.md">SceneActionDefault</a></li>
    <li><a href="SceneActionDefault%601.md">SceneActionDefault&lt;T&gt;</a></li>
    <li><a href="SceneActionDefault%602.md">SceneActionDefault&lt;T1, T2&gt;</a></li>
    <li><a href="SceneActionDefault%603.md">SceneActionDefault&lt;T1, T2, T3&gt;</a></li>
    <li><a href="SceneActionDefault%604.md">SceneActionDefault&lt;T1, T2, T3, T4&gt;</a></li>
  </ul>
</details>
</li>

<li>
<details>
  <summary><a href="SceneActionsComposite.md">SceneActionsComposite</a></summary>
  <ul>
    <li><a href="SceneActionComposite.md">SceneActionComposite</a></li>
    <li><a href="SceneActionComposite%601.md">SceneActionComposite&lt;T&gt;</a></li>
    <li><a href="SceneActionComposite%602.md">SceneActionComposite&lt;T1, T2&gt;</a></li>
    <li><a href="SceneActionComposite%603.md">SceneActionComposite&lt;T1, T2, T3&gt;</a></li>
    <li><a href="SceneActionComposite%604.md">SceneActionComposite&lt;T1, T2, T3, T4&gt;</a></li>
  </ul>
</details>
</li>

<li>

<details>
  <summary><a href="SceneActionsReference.md">SceneActionsReference</a></summary>
  <ul>
    <li><a href="SceneActionReference.md">SceneActionReference</a></li>
    <li><a href="SceneActionReference%601.md">SceneActionReference&lt;T&gt;</a></li>
    <li><a href="SceneActionReference%602.md">SceneActionReference&lt;T1, T2&gt;</a></li>
    <li><a href="SceneActionReference%603.md">SceneActionReference&lt;T1, T2, T3&gt;</a></li>
    <li><a href="SceneActionReference%604.md">SceneActionReference&lt;T1, T2, T3, T4&gt;</a></li>
  </ul>
</details> 
</li>


  <li><a href="PrintAction.md">LogAction</a></li>
  <li><a href="ExtensionsInvokeRange.md">Extensions</a></li>
</ul>

---

## 📌 Best Practices

- [Using Inline Actions](../../BestPractices/UsingInlineActions.md)
- [Using SerializeReference for CompositeActions](../../BestPractices/UsingSerializeReferenceForCompositeActions.md)
- [Using SerializeReference for LogAction](../../BestPractices/UsingSerializeReferenceForPrintActions.md)
- [Actions vs Requests](../../BestPractices/RequestsVsActions.md)