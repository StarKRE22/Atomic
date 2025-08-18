// using Atomic.Elements;
// using Atomic.Entities;
// using Modules.Gameplay;
//
// namespace RTSGame
// {
//     public sealed class LifetimeBehaviour : IEntityInit, IEntityFixedUpdate
//     {
//         private Cooldown _lifetime;
//         private IAction _destroyAction;
//
//         public void Init(in IEntity entity)
//         {
//             _destroyAction = entity.GetDestroyAction();
//             _lifetime = entity.GetLifetime();
//         }
//
//         public void OnFixedUpdate(in IEntity entity, in float deltaTime)
//         {
//             _lifetime.Tick(deltaTime);
//             
//             if (_lifetime.IsExpired()) 
//                 _destroyAction.Invoke();
//         }
//     }
// }