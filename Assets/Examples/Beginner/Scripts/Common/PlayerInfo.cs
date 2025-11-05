using System;
using Atomic.Entities;

namespace BeginnerGame
{
    /// <summary>
    /// Represents information about a player in the game.
    /// </summary>
    /// <remarks>
    /// Contains the player's team affiliation and a reference to their 
    /// in-scene character entity.
    /// </remarks>
    [Serializable]
    public class PlayerInfo
    {
        /// <summary>
        /// The team to which this player belongs.
        /// </summary>
        public TeamType team;
        
        /// <summary>
        /// Reference to the player's character entity in the scene.
        /// </summary>
        public SceneEntity character;
    }
}