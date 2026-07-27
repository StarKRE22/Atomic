# 📣 Atomic.Events.Bus

The **Atomic.Events.Bus** namespace contains event bus implementations used to publish and subscribe to events.

---

## 🔍 Types

- [IEventBus](IEventBus.md) — core event bus interface
- [EventBus](EventBus.md) — default implementation
- [ThreadSafeEventBus](ThreadSafeEventBus.md) — thread-safe wrapper that queues invokes for main-thread flushing
- [MonoEventBus](MonoEventBus.md) — Unity `MonoBehaviour` event bus
- [MonoEventBusSingleton](MonoEventBusSingleton.md) — singleton scene/global bus

## 🔍 Related

- [Event Keys](../Keys/Manual.md)
- [Subscriptions](../Subscriptions/Manual.md)
- [Atomic.Events Manual](../Manual.md)
