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
    - [MonoAction](#monoactiont)
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

#### `MonoAction<T>`

```csharp
public sealed class DestroyGameObjectAction : MonoAction<GameObject>
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
  <summary><a href="MonoActions.md">MonoActions</a></summary>
  <ul>
    <li><a href="MonoAction.md">MonoAction</a></li>
    <li><a href="MonoAction%601.md">MonoAction&lt;T&gt;</a></li>
    <li><a href="MonoAction%602.md">MonoAction&lt;T1, T2&gt;</a></li>
    <li><a href="MonoAction%603.md">MonoAction&lt;T1, T2, T3&gt;</a></li>
    <li><a href="MonoAction%604.md">MonoAction&lt;T1, T2, T3, T4&gt;</a></li>
  </ul>
</details>
</li>

<li>
<details>
  <summary><a href="MonoActionsConfigurable.md">MonoActionsConfigurable</a></summary>
  <ul>
    <li><a href="MonoActionConfigurable.md">MonoActionConfigurable</a></li>
    <li><a href="MonoActionConfigurable%601.md">MonoActionConfigurable&lt;T&gt;</a></li>
    <li><a href="MonoActionConfigurable%602.md">MonoActionConfigurable&lt;T1, T2&gt;</a></li>
    <li><a href="MonoActionConfigurable%603.md">MonoActionConfigurable&lt;T1, T2, T3&gt;</a></li>
    <li><a href="MonoActionConfigurable%604.md">MonoActionConfigurable&lt;T1, T2, T3, T4&gt;</a></li>
  </ul>
</details>
</li>

<li>
<details>
  <summary><a href="MonoActionsComposite.md">MonoActionsComposite</a></summary>
  <ul>
    <li><a href="MonoActionComposite.md">MonoActionComposite</a></li>
    <li><a href="MonoActionComposite%601.md">MonoActionComposite&lt;T&gt;</a></li>
    <li><a href="MonoActionComposite%602.md">MonoActionComposite&lt;T1, T2&gt;</a></li>
    <li><a href="MonoActionComposite%603.md">MonoActionComposite&lt;T1, T2, T3&gt;</a></li>
    <li><a href="MonoActionComposite%604.md">MonoActionComposite&lt;T1, T2, T3, T4&gt;</a></li>
  </ul>
</details>
</li>

<li>

<details>
  <summary><a href="MonoActionsReference.md">MonoActionsReference</a></summary>
  <ul>
    <li><a href="MonoActionReference.md">MonoActionReference</a></li>
    <li><a href="MonoActionReference%601.md">MonoActionReference&lt;T&gt;</a></li>
    <li><a href="MonoActionReference%602.md">MonoActionReference&lt;T1, T2&gt;</a></li>
    <li><a href="MonoActionReference%603.md">MonoActionReference&lt;T1, T2, T3&gt;</a></li>
    <li><a href="MonoActionReference%604.md">MonoActionReference&lt;T1, T2, T3, T4&gt;</a></li>
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
