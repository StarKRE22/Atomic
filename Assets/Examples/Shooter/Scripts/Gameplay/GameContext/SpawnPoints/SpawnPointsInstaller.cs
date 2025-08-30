using System;
using System.Collections.Generic;
using System.Linq;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    [Serializable]
    public sealed class SpawnPointsInstaller : IEntityInstaller<IGameContext>
    {
        private const string SPAWN_POINT_TAG = "Respawn";

        public void Install(IGameContext context)
        {
            Transform[] spawnPoints = GameObject
                .FindGameObjectsWithTag(SPAWN_POINT_TAG)
                .Select(it => it.transform)
                .ToArray();

            context.AddFreeSpawnPoints(new List<Transform>(spawnPoints));
            context.AddAllSpawnPoints(spawnPoints);
        }
    }
}