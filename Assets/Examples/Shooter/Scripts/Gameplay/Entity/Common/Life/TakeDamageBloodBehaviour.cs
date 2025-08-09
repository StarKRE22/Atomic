// using Atomic.Elements;
// using Atomic.Entities;
// using UnityEngine;
//
// namespace ShooterGame.Gameplay
// {
//     public sealed class TakeDamageBloodBehaviour : IEntityInit, IEntityDispose
//     {
//         private readonly ParticleSystem _bloodVfx;
//
//         private IReactive<DamageArgs> _damageEvent;
//         private TeamConfig _teamConfig;
//         private IValue<TeamType> _teamType;
//
//         public TakeDamageBloodBehaviour(ParticleSystem bloodVfx)
//         {
//             _bloodVfx = bloodVfx;
//         }
//
//         public void Init(in IEntity entity)
//         {
//             _teamType = entity.GetTeam();
//             _damageEvent = entity.GetTakeDamageEvent();
//             _teamConfig = GameContext.Instance.GetTeamConfig();
//             _damageEvent.Subscribe(this.OnDamageTaken);
//         }
//
//         public void Dispose(in IEntity entity)
//         {
//             _damageEvent.Unsubscribe(this.OnDamageTaken);
//         }
//
//         private void OnDamageTaken(DamageArgs obj)
//         {
//             Color color = _teamConfig.GetTeam(_teamType.Value).Material.color;
//             foreach (ParticleSystem particle in _bloodVfx.GetComponentsInChildren<ParticleSystem>())
//             {
//                 ParticleSystem.MainModule particleMain = particle.main;
//                 particleMain.startColor = color;
//             }
//
//             _bloodVfx.Play(withChildren: true);
//         }
//     }
// }