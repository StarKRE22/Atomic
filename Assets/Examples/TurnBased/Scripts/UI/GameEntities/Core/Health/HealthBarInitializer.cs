using System;
using Atomic.Entities;
using Game.Gameplay;

namespace Game.UI
{
    [Serializable]
    public sealed class HealthBarInitializer : IEntityInit<IGameEntity>
    {
        public void Init(IGameEntity entity)
        {
            ProgressBarPro healthBar = entity.GetHealthBar();
            healthBar.Value = entity.GetHealthPercent();
        }
    }
}