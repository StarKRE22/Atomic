using Atomic.Elements;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class TakeDamageBloodBehaviour : IGameEntityInit, IGameEntityDispose
    {
        private readonly IGameContext _gameContext;
        private readonly ParticleSystem _bloodVfx;

        private ISignal<DamageArgs> _damageEvent;
        private TeamCatalog _teamCatalog;
        private IValue<TeamType> _teamType;

        public TakeDamageBloodBehaviour(IGameContext gameContext, ParticleSystem bloodVfx)
        {
            _gameContext = gameContext;
            _bloodVfx = bloodVfx;
        }

        public void Init(IGameEntity entity)
        {
            _teamType = entity.GetTeamType();
            _teamCatalog = _gameContext.GetTeamCatalog();
            _damageEvent = entity.GetTakeDamageEvent();
            _damageEvent.OnEvent += this.OnDamageTaken;
        }

        public void Dispose(IGameEntity entity)
        {
            _damageEvent.OnEvent -= this.OnDamageTaken;
        }

        private void OnDamageTaken(DamageArgs obj)
        {
            Color color = _teamCatalog.GetInfo(_teamType.Value).Material.color;
            ParticleSystem[] particles = _bloodVfx.GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem particle in particles)
            {
                ParticleSystem.MainModule particleMain = particle.main;
                particleMain.startColor = color;
            }

            _bloodVfx.Play(withChildren: true);
        }
    }
}