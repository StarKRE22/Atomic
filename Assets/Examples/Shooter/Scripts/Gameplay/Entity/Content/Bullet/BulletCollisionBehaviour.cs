// using Atomic.Elements;
// using Atomic.Entities;
// using UnityEngine;
//
// namespace ShooterGame.Gameplay
// {
//     public sealed class BulletCollisionBehaviour : IEntityInit, IEntityDispose
//     {
//         private IEntity _entity;
//         private TriggerEventReceiver _trigger;
//         private IValue<int> _damage;
//         private IAction _destroyAction;
//         private IGameContext _gameContext;
//
//         public void Init(in IEntity entity)
//         {
//             _entity = entity;
//             _destroyAction = entity.GetDestroyAction();
//             _damage = entity.GetDamage();
//             _trigger = entity.GetTrigger();
//             _gameContext = GameContext.Instance;
//
//             _trigger.OnEntered += this.OnTriggerEntered;
//         }
//
//         public void Dispose(in IEntity entity)
//         {
//             _trigger.OnEntered -= this.OnTriggerEntered;
//         }
//
//         private void OnTriggerEntered(Collider collider)
//         {
//             DamageArgs args = new DamageArgs
//             {
//                 source = _entity,
//                 damage = _damage.Value
//             };
//             
//             if (TakeDamageUseCase.TakeDamage(collider, args, _gameContext))
//                 _destroyAction.Invoke();
//         }
//     }
// }