using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShooterGame.Gameplay
{
    [CreateAssetMenu(
        fileName = "TeamConfig",
        menuName = "SampleGame/New TeamConfig"
    )]
    public sealed class TeamConfig : ScriptableObject
    {
        [SerializeField]
        private Team[] _teams;

        public Team GetTeam(TeamType teamType)
        {
            for (int i = 0, count = _teams.Length; i < count; i++)
            {
                Team info = _teams[i];
                if (info.Type == teamType)
                    return info;
            }

            throw new KeyNotFoundException($"Team of type {teamType} is not found!");
        }

        [Serializable]
        public sealed class Team
        {
            [FormerlySerializedAs("_team")]
            [SerializeField]
            private TeamType type;

            [SerializeField]
            private Material material;

            public Material Material
            {
                get { return this.material; }
            }

            public TeamType Type
            {
                get { return type; }
            }

            public int CameraDisplay
            {
                get { return (int) this.type - 1; }
            }

            public int PhysicsLayer
            {
                get
                {
                    return type switch
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
}