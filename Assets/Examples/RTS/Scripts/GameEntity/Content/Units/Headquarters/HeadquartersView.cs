using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public sealed class HeadquartersView : EntityView
    {
        [SerializeField]
        private Transform _root;
        
        

        protected override void OnInit()
        {
            
        }

        protected override void OnShow(IEntity entity)
        {
            entity.AddBehaviour(new TakeDamageViewBehaviour(_root));
            entity.AddBehaviour(new PositionViewBehaviour(_root));
            entity.AddBehaviour(new RotationViewBehaviour(_root));
            entity.AddBehaviour(new TeamColorBehaviour(_renderers, GameContext.Instance));
        }

        protected override void OnHide(IEntity entity)
        {
            entity.DelBehaviour<TakeDamageViewBehaviour>();
        }

        protected override void OnDispose()
        {
            
        }
    }
}