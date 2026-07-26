using Atomic.Entities;
using Game.Gameplay;
using UnityEngine;

namespace Game.UI
{
    public sealed class CharacterViewInstaller : MonoEntityInstaller<IGameEntity>
    {
        [SerializeField] private ProgressBarPro _healthBar;
        [SerializeField] private Animator _animator;
        [SerializeField] private HealthBarInitializer _healthPresenter;
        [SerializeField] private CharacterRenderer _characterRenderer;

        public override void Install(IGameEntity entity)
        {
            entity.AddValue(GameEntityViewAPI.Transform, this.transform);
            entity.AddValue(GameEntityViewAPI.Animator, _animator);
            entity.AddValue(GameEntityViewAPI.CharacterRenderer, _characterRenderer);
            entity.AddValue(GameEntityViewAPI.HealthBar, _healthBar);
            entity.AddBehaviour(_healthPresenter);
        }

        public override void Uninstall(IGameEntity entity)
        {
            entity.DelBehaviour(_healthPresenter);
            entity.DelValue(GameEntityViewAPI.Animator);
            entity.DelValue(GameEntityViewAPI.CharacterRenderer);
            entity.DelValue(GameEntityViewAPI.HealthBar);
            entity.DelValue(GameEntityViewAPI.Transform);
        }
    }
}