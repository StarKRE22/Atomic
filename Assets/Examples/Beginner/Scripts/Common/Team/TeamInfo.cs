using System;
using UnityEngine;

namespace BeginnerGame
{
    [Serializable]
    public sealed class TeamInfo
    {
        [field: SerializeField]
        public TeamType Type;

        [field: SerializeField]
        public Material Material;
    }
}