-
### 👷‍♂️ Builder

`ProxyVariable<T>` also includes a **fluent builder** to simplify creation:

```csharp
IVariable<int> variable = ProxyVariable<int>
    .StartBuild()
    .WithGetter(() => myField)
    .WithSetter(v => myField = v)
    .Build();
```