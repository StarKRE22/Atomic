using System;
using UnityEngine;

namespace SampleGame
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