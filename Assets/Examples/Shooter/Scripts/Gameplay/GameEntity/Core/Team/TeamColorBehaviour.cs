// using Atomic.Contexts;
// using Atomic.Elements;
// using Atomic.Entities;
// using UnityEngine;
//
// namespace ShooterGame.Gameplay
// {
//     [EditModeBehaviour]
//     public sealed class TeamColorBehaviour : IEntityInit, IEntityDispose
//     {
//         private Renderer _meshRenderer;
//         private TeamConfig _teamConfig;
//         private IReactiveValue<TeamType> _team;
//
//         public void Init(in IEntity entity)
//         {
//             _meshRenderer = entity.GetMeshRenderer();
//             _teamConfig = GameContext.Instance.GetTeamConfig();
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
//             _meshRenderer.material = _teamConfig.GetTeam(teamType).Material;
//         }
//     }
// }