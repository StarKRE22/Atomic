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
            entity.AddTransform( this.transform);
            entity.AddAnimator( _animator);
            entity.AddCharacterRenderer( _characterRenderer);
            entity.AddHealthBar( _healthBar);
            entity.AddBehaviour(_healthPresenter);
        }

        public override void Uninstall(IGameEntity entity)
        {
            entity.DelBehaviour(_healthPresenter);
            entity.DelAnimator();
            entity.DelCharacterRenderer();
            entity.DelHealthBar();
            entity.DelTransform();
        }
    }
}