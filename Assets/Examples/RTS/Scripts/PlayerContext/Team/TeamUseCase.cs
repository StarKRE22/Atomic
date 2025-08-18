// using Atomic.Entities;
//
// namespace RTSGame
// {
//     public static partial class TeamUseCase
//     {
//         public static bool IsEnemy(in PlayerContext context, in IEntity entity)
//         {
//             return context.Team != entity.GetTeam().Value;
//         }
//         
//         public static bool IsAlly(in PlayerContext context, in IEntity entity)
//         {
//             return context.Team == entity.GetTeam().Value;
//         }
//     }
// }