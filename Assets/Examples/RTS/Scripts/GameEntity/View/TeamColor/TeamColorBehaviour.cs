// using System.Collections.Generic;
// using Atomic.Elements;
// using Atomic.Entities;
// using UnityEngine;
//
// namespace RTSGame
// {
//     public sealed class TeamColorBehaviour : IEntityInit, IEntityDispose
//     {
//         private readonly IEnumerable<Renderer> _renderers;
//
//         private IReactiveValue<TeamType> _team;
//         private TeamViewConfig _viewConfig;
//
//         public TeamColorBehaviour(IEnumerable<Renderer> renderers)
//         {
//             _renderers = renderers;
//         }
//
//         public void Init(in IEntity entity)
//         {
//             _viewConfig = GameContext.Instance.GetTeamViewConfig();
//             _team = entity.GetTeam();
//             _team.Observe(this.OnTeamChanged);
//         }
//
//         public void Dispose(in IEntity entity)
//         {
//             _team.Unsubscribe(this.OnTeamChanged);
//         }
//
//         private void OnTeamChanged(TeamType teamType)
//         {
//             TeamViewConfig.TeamInfo team = _viewConfig.GetTeam(teamType);
//             Material material = team.Material;
//             foreach (Renderer renderer in _renderers) 
//                 renderer.material = material;
//         }
//     }
// }