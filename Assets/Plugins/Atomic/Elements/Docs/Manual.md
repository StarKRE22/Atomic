# 📘 Atomic.Elements Manual

`Atomic.Elements` is a modular and reusable component library for Unity and C#, designed to simplify and accelerate game development. It provides a set of reactive components and data structures that can be easily integrated into different projects, offering flexibility and scalability.

## 🔍 Table of Contents

- **Values**
  - [IValue](Values/IValue.md)
  - [IReactiveValue](Values/IReactiveValue.md)
  - [Const](Values/Const.md)

- [Variable]
  - [IVariable] 
  - [BaseVariable]
  - [ReactiveVariable]
  - [ProxyVariable]
  - [PlayerPrefsVariables]

- [Actions]
  - [IAction] 
  - [InlineAction]
  - [CompositeAction]

- [Functions]
  - [IFunction] 
  - [InlineFunction]
  - [InlinePredicate]

- [Events]
  - [IEvent]
  - [BaseEvent]
  
- [Requests]
  - [IRequest]
  - [BaseRequest]

- [Signals]
  - [ISignal]
  - [InlineSignal]

- [Setters]
  - [ISetter]
  - [InlineSetter]

- [Expressions]
  - [AndExpression]
  - [OrExpression]
  - [IntMulExpression]
  - [IntSumExpression]
  - [FloatMulExpression]
  - [FloatSumExpression]
  - [InlineExpression]

- [Collections]
  - [ReactiveArray]
  - [ReactiveDictionary]
  - [ReactiveHashSet]
  - [ReactiveList]

- [Time]
  - [Cooldown]
  - [Timer]
  - [Countdown]
  - [Stopwatch]
  - [Period]
  - [Timestamp]

- [Unity Components]
  - [TriggerEvents]
  - [TriggerEvents2D]
  - [CollisionEvents]
  - [CollisionEvents2D]
  - [AnimationEvents]

- [Utils]
  - [DisposableComposite]
  - [EqualityComparer]
  - [Optional]
  - [Reference]




[//]: # ()
[//]: # ()
[//]: # (1. [Overview]&#40;#overview&#41;)

[//]: # (2. [Core Components]&#40;#core-components&#41;)

[//]: # (3. [Usage]&#40;#usage&#41;)

[//]: # (4. [Best Practices]&#40;#best-practices&#41;)

[//]: # (5. [References]&#40;#references&#41;)

[//]: # ()
[//]: # (## 🧩 Overview)

[//]: # ()
[//]: # (`Atomic.Elements` provides a collection of reactive components and data structures for creating complex entities in game development. Each component is designed to be **independent and highly configurable**, allowing developers to quickly adapt them to their project needs.)

[//]: # ()












[//]: # ()
[//]: # ()
[//]: # (## 🔧 Core Components)

[//]: # ()
[//]: # (- **Component 1**: Description of component 1.)

[//]: # (- **Component 2**: Description of component 2.)

[//]: # (- **Component 3**: Description of component 3.)

[//]: # ()
[//]: # (*&#40;Add real component names and descriptions based on the repository content&#41;*)

[//]: # ()
[//]: # (## 🚀 Usage)

[//]: # ()
[//]: # (To get started with `Atomic.Elements`, follow these steps:)

[//]: # ()
[//]: # (1. Clone or download the repository.)

[//]: # (2. Import the required components into your Unity project.)

[//]: # (3. Configure components according to the documentation.)

[//]: # ()
[//]: # (Example usage:)

[//]: # (```csharp)

[//]: # (using Atomic.Elements;)

[//]: # (using UnityEngine;)

[//]: # ()
[//]: # (public class Example : MonoBehaviour)

[//]: # ({)

[//]: # (    void Start&#40;&#41;)

[//]: # (    {)

[//]: # (        var component = new Component1&#40;&#41;;)

[//]: # (        component.Initialize&#40;&#41;;)

[//]: # (    })

[//]: # (})