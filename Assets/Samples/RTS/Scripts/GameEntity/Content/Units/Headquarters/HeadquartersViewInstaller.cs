using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public sealed class HeadquartersViewInstaller : EntityViewInstaller
    {
        [SerializeField]
        private Transform _root;
        
        public override void Install(EntityView view)
        {
            view.AddBehaviour(new TakeDamageViewBehaviour(_root));
        }
    }
}