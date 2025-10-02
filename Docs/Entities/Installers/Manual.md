# 🧩 Entity Installers

**Entity Installer** — это компонент, который выполняет установку тэгов, значений и поведений в экземпляр сущности. Он
представляет собой декларативный механизм настройки сущностей на этапе инициализации или во время выполнения.

---

Ниже описаны все типы инсталлеров в зависимости от необходимости использования

- [IEntityInstaller](IEntityInstaller.md)
- [IEntityInstaller&lt;E&gt;](IEntityInstaller%601.md)
- [SceneEntityInstaller](SceneEntityInstaller.md)
- [SceneEntityInstaller&lt;E&gt;](SceneEntityInstaller%601.md)
- [ScriptableEntityInstaller](ScriptableEntityInstaller.md)
- [ScriptableEntityInstaller&lt;E&gt;](ScriptableEntityInstaller%601.md)

---

## 🗂 Example of Usage

```csharp
public sealed class CharacterInstaller : SceneEntityInstaller
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _moveSpeed = 5.0f;

    public override void Install(IEntity entity)
    {
        entity.AddTag("Character");
        entity.AddTag("Moveable");

        entity.AddValue("Transform", _transform);
        entity.AddValue("MoveSpeed", _moveSpeed);

        entity.AddBehaviour<MoveBehaviour>();
    }

    public override void Uninstall(IEntity entity)
    {
        // Очистка или отписка от событий при уничтожении
    }

}
```
!!!

!!!

---

## 📝 Notes


- **Installer** = декларативный способ конфигурации сущностей.
- **SceneEntityInstaller** = настройка через `MonoBehaviour`, привязка к сцене.
- **ScriptableEntityInstaller** = настройка через `ScriptableObject`, переиспользуемая логика.
- **Generic Installers** = строго типизированный вариант для повышения безопасности и читаемости кода.
- Старайтесь описывать в инсталлере только **настройку** сущности, избегая бизнес-логики.
- Для корректного освобождения ресурсов всегда переопределяйте метод `Uninstall`, если есть подписки или
  IDisposable-объекты.
