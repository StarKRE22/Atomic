using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    /// <summary>
    /// Contains configuration data for spawning entities in the scene.
    /// </summary>
    /// <remarks>
    /// Defines the prefab to spawn, the container for spawned objects, 
    /// the spawn area boundaries, and the cooldown period between spawns.  
    /// Commonly used by spawner systems to control entity generation frequency and location.
    /// </remarks>
    [Serializable]
    public sealed class SpawnInfo
    {
        /// <summary>
        /// The entity prefab that will be instantiated.
        /// </summary>
        public SceneEntity prefab;
        
        /// <summary>
        /// The parent transform under which spawned entities will be placed.
        /// </summary>
        public Transform container;
        
        /// <summary>
        /// Defines the rectangular area within which entities can be spawned.
        /// </summary>
        public Bounds area = new(Vector3.zero, new Vector3(5, 0, 5));
        
        /// <summary>
        /// The cooldown period controlling how often new entities can be spawned.
        /// </summary>
        public Cooldown period = 2;
    }
}