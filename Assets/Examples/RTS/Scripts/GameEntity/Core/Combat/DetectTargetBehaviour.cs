// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using Atomic.Elements;
// using Atomic.Entities;
// using Modules.Gameplay;
// using UnityEngine;
//
// namespace RTSGame
// {
//     public sealed class DetectTargetBehaviour : IEntityInit, IEntityFixedUpdate
//     {
//         private readonly RandomCooldown _period;
//
//         private IVariable<IEntity> _target;
//         private IValue<Vector3> _position;
//
//         private IEntity _entity;
//         private GameContext _gameContext;
//
//         public DetectTargetBehaviour(RandomCooldown period)
//         {
//             _period = period;
//         }
//
//         public void Init(in IEntity entity)
//         {
//             _entity = entity;
//             _target = entity.GetTarget();
//             _position = entity.GetPosition();
//             _gameContext = GameContext.Instance;
//         }
//
//         public void OnFixedUpdate(in IEntity entity, in float deltaTime)
//         {
//             _period.Tick(deltaTime);
//             if (!_period.IsExpired())
//                 return;
//
//             _target.Value = this.FindClosestEnemy();
//             _period.Reset();
//         }
//
//         private IEntity FindClosestEnemy()
//         {
//             PlayerContext playerContext = PlayersUseCase.GetPlayerContext(_gameContext, _entity);
//             IEntityFilter enemyFilter = playerContext.GetEnemiesFilter();
//             return EntitiesUseCase.FindClosest(enemyFilter, _position.Value);
//         }
//     }
// }
//
// // private static readonly int _limit = 50;
// // private static int _current = 0;
// // private static float time;