using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace BeginnerGame
{
    [CreateAssetMenu(fileName = "TeamCatalog", menuName = "Beginner/TeamCatalog")]
    public sealed class TeamCatalog : ScriptableObject
    {
        [FormerlySerializedAs("Teams")]
        [SerializeField]
        private TeamInfo[] _teams;

        public TeamInfo GetInfo(TeamType type)
        {
            for (int i = 0, count = _teams.Length; i < count; i++)
            {
                TeamInfo info = _teams[i];
                if (info.Type == type)
                    return info;
            }

            throw new Exception($"Team info of type is not found {type}");
        }
    }
}