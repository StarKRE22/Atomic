// using System;
// using System.Collections.Generic;
// using Atomic.Entities;
//
// namespace RTSGame
// {
//     [Serializable]
//     public sealed class PlayerSystemInstaller : IEntityInstaller<GameContext>
//     {
//         public void Install(GameContext context) => 
//             context.AddPlayers(new Dictionary<TeamType, PlayerContext>());
//     }
// }