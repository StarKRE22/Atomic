# 🧩 InlineSignals

Provides a wrapper for reactive event source. Implements the corresponding [ISignal](ISignals.md) interface and allow
entities to **subscribe / unsubscribe** from events, optionally with parameters. When subscribing to a signal, the
method returns a [Subscription](Subscriptions.md) struct.

There are several inline signals, depending on the number of arguments they take:

- [InlineSignal](InlineSignal.md) — Signal without parameters.
- [InlineSignal&lt;T&gt;](InlineSignal%601.md) — Signal that takes one argument.
- [InlineSignal&lt;T1, T2&gt;](InlineSignal%602.md) — Signal that takes two arguments.
- [InlineSignal&lt;T1, T2, T3&gt;](InlineSignal%603.md) — Signal that takes two arguments.
- [InlineSignal&lt;T1, T2, T3, T4&gt;](InlineSignal%604.md) — Signal that takes two arguments.
