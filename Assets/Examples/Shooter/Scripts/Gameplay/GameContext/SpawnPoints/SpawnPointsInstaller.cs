// using System;
// using System.Collections.Generic;
// using Atomic.Contexts;
// using UnityEngine;
//
// namespace ShooterGame.Gameplay
// {
//     [Serializable]
//     public sealed class SpawnPointsInstaller : IContextInstaller<IGameContext>
//     {
//         [SerializeField]
//         private Transform[] _spawnPoints;
//         
//         public void Install(IGameContext context)
//         {
//             context.AddFreeSpawnPoints(new List<Transform>(_spawnPoints));
//             context.AddAllSpawnPoints(_spawnPoints);
//         }
//     }
// }