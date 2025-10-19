# 🧩️ SceneEntityBakers


### 2️⃣ SceneEntityBaker

```csharp
public class EnemyBaker : SceneEntityBaker<EnemyEntity>
{
    public override EnemyEntity Create()
    {
        var enemy = new EnemyEntity();
        enemy.AddValue<int>("Health", 150);
        enemy.AddValue<int>("Damage", 25);
        return enemy;
    }
}
```