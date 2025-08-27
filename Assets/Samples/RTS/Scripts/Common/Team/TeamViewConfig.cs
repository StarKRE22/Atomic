using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace RTSGame
{
    [CreateAssetMenu(
        fileName = "TeamViewConfig",
        menuName = "RTSGame/Common/New TeamViewConfig"
    )]
    public sealed class TeamViewConfig : ScriptableObject
    {
        [SerializeField]
        private TeamInfo[] _teams;

        public TeamInfo GetTeam(TeamType teamType)
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
        }
    }
}