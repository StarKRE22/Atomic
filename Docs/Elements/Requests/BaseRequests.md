# 🧩 BaseRequests

The **BaseRequest** classes provide **concrete implementations** of the [IRequest](IRequests.md) interfaces. They are
designed to store request state and optionally one to four arguments. These classes **track whether a request is
required** and allow **deferred consumption**.

> [!IMPORTANT]
> Unlike regular actions, requests are meant for **deferred execution**. You can call `Invoke` to create a request, and
> process it later using `Consume`. Repeated `Invoke` calls before `Consume` do not create duplicates.


There are several implementations of requests, depending on the number of arguments they take:

- [BaseRequest](BaseRequest.md) — Non-generic version; works without parameters.
- [BaseRequest&lt;T&gt;](BaseRequest%601.md) — Request that takes one argument.
- [BaseRequest&lt;T1, T2&gt;](BaseRequest%602.md) — Request that takes two arguments.
- [BaseRequest&lt;T1, T2, T3&gt;](BaseRequest%603.md) — Request that takes three arguments.
- [BaseRequest&lt;T1, T2, T3, T4&gt;](BaseRequest%604.md) — Request that takes four arguments.
