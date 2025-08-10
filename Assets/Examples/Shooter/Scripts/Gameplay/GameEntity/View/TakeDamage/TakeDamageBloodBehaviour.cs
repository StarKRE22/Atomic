using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class TakeDamageBloodBehaviour : IEntitySpawn<IGameEntity>, IEntityDespawn
    {
        private readonly ParticleSystem _bloodVfx;

        private ISignal<DamageArgs> _damageEvent;
        private TeamCatalog _teamCatalog;
        private IValue<TeamType> _teamType;

        public TakeDamageBloodBehaviour(ParticleSystem bloodVfx) => _bloodVfx = bloodVfx;

        public void OnSpawn(IGameEntity entity)
        {
            _teamType = entity.GetTeamType();
            _damageEvent = entity.GetTakeDamageEvent();
            _teamCatalog = GameContext.Instance.GetTeamCatalog();
            _damageEvent.Subscribe(this.OnDamageTaken);
        }

        public void OnDespawn(IEntity entity)
        {
            _damageEvent.Unsubscribe(this.OnDamageTaken);
        }

        private void OnDamageTaken(DamageArgs obj)
        {
            Color color = _teamCatalog.GetInfo(_teamType.Value).Material.color;
            foreach (ParticleSystem particle in _bloodVfx.GetComponentsInChildren<ParticleSystem>())
            {
                ParticleSystem.MainModule particleMain = particle.main;
                particleMain.startColor = color;
            }

            _bloodVfx.Play(withChildren: true);
        }
    }
}