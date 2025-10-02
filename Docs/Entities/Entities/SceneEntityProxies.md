# 🧩 SceneEntity Proxies

Represent family of Unity components those act as a proxy or reference to an existing [SceneEntity](SceneEntity.md).
It allows multiple `GameObjects` to share and reference the same entity instance, enabling flexible entity
architectures.

There are both generic and non-generic versions of proxies.

- [SceneEntityProxy](SceneEntityProxy.md)
- [SceneEntityProxy&lt;T&gt;](SceneEntityProxy%601.md)

---

## 📝 Notes

- **Entity Reference** – Points to an existing `SceneEntity`
- **Delegation** – Forwards `IEntity` interface calls to target
- **Proxy Pattern** – Multiple proxies can reference one entity
- **Inspector Configuration** – Set entity reference in Unity Editor