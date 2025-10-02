# 🧩 Lifecycle

**Lifecycle** defines standardized interfaces for different stages of an entity or system lifecycle, such as
initialization, enabling, and ticking. **Subscriptions** provide a declarative mechanism to register actions that
execute automatically at specific lifecycle events.

- **Contracts**
    - [IInitLifecycle](Sources/IInitLifecycle.md) <!-- + -->
    - [IEnableLifecycle](Sources/IEnableLifecycle.md) <!-- + -->
    - [ITIckLifecycle](Sources/ITickLifecycle.md) <!-- + -->
- **Subscriptions**
    - [InitSubscription](Subscriptions/InitSubscription.md) <!-- + -->
    - [EnableSubscription](Subscriptions/EnableSubscription.md) <!-- + -->
    - [DisableSubscription](Subscriptions/DisableSubscription.md)
    - [DisposeSubscription](Subscriptions/DisposeSubscription.md) <!-- + -->
    - [TickSubscription](Subscriptions/TickSubscription.md)
    - [FixedTickSubscription](Subscriptions/FixedTickSubscription.md)
    - [LateTickSubscription](Subscriptions/LateTickSubscription.md)
- [Extensions](Extensions.md)