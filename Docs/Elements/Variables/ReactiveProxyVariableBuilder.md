### 👷‍♂️ Builder

`ReactiveProxyVariable<T>` provides a **fluent builder** for convenience

```csharp
IReactiveVariable<int> variable = ReactiveProxyVariable<int>
    .StartBuild()
    .WithGetter(() => someInt)
    .WithSetter(v => someInt = v)
    .WithSubscribe(cb => myEvent += cb)
    .WithUnsubscribe(cb => myEvent -= cb)
    .Build();
```
