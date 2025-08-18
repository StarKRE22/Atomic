// using Atomic.Entities;
// using Modules.Gameplay;
//
// namespace RTSGame
// {
//     public sealed class ProjectileLifetimeBehaviour : IEntityInit, IEntityFixedUpdate
//     {
//         private Cooldown _lifetime;
//         private GameContext _gameContext;
//
//         public void Init(in IEntity entity)
//         {
//             _lifetime = entity.GetLifetime();
//             _lifetime.Reset();
//             _gameContext = GameContext.Instance;
//         }
//
//         public void OnFixedUpdate(in IEntity entity, in float deltaTime)
//         {
//             _lifetime.Tick(deltaTime);
//             if (_lifetime.IsExpired())
//                 EntitiesUseCase.UnspawnEntity(_gameContext, entity);
//         }
//     }
// }