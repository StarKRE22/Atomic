using System;
using Atomic.Entities;

namespace BeginnerGame
{
    [Serializable]
    public class PlayerInfo
    {
        public TeamType team;
        public SceneEntity character;
    }
}