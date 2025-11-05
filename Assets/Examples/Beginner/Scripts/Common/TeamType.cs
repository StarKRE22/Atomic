namespace BeginnerGame
{
    /// <summary>
    /// Defines available team affiliations for entities in the game.
    /// </summary>
    /// <remarks>
    /// Used to differentiate players, NPCs, or other entities belonging 
    /// to different factions or sides within gameplay logic.
    /// </remarks>
    public enum TeamType
    {
        /// <summary>
        /// Represents the blue team.
        /// </summary>
        BLUE = 0,
    
        /// <summary>
        /// Represents the red team.
        /// </summary>
        RED = 1,
    
        /// <summary>
        /// Represents a neutral or unassigned team.
        /// </summary>
        NONE = 2
    }
}