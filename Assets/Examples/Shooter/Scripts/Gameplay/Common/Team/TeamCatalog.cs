using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShooterGame.Gameplay
{
    [CreateAssetMenu(
        fileName = "TeamConfig",
        menuName = "ShooterGame/New TeamConfig"
    )]
    public sealed class TeamCatalog : ScriptableObject
    {
        [SerializeField]
        private TeamInfo[] _teams;

        public TeamInfo GetInfo(TeamType teamType)
        {
            for (int i = 0, count = _teams.Length; i < count; i++)
            {
                TeamInfo info = _teams[i];
                if (info.Type == teamType)
                    return info;
            }

            throw new KeyNotFoundException($"Team of type {teamType} is not found!");
        }

        [Serializable]
        public sealed class TeamInfo
        {
            [FormerlySerializedAs("_team")]
            [SerializeField]
            private TeamType type;

            [SerializeField]
            private Material material;

            public Material Material => this.material;

            public TeamType Type => type;

            public int CameraDisplay => (int) this.type - 1;

            public int PhysicsLayer
            {
                get => type switch
                {
                    TeamType.NEUTRAL => 0,
                    TeamType.BLUE => 6,
                    TeamType.RED => 7,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }
    }
}