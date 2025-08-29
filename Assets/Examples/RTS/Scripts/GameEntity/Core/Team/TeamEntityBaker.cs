using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class TeamEntityBaker : IEntityInstaller<IGameEntity>
    {
        [SerializeField]
        private Optional<TeamType> _teamType;

        [SerializeField]
        private TeamViewConfig _viewConfig;
        
        [SerializeField]
        private Renderer[] _renderers;
        
        public void Install(IGameEntity entity)
        {
            if (_teamType) entity.GetTeam().Value = _teamType;
        }

        public void OnValidate()
        {
            RendererUseCase.SetMaterial(_renderers, _viewConfig.GetTeam(_teamType).Material);
        }
    }
}