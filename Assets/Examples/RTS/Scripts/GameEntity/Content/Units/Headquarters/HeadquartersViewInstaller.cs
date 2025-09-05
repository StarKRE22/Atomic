using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public sealed class HeadquartersViewInstaller : EntityViewInstaller
    {
        [SerializeField]
        private Transform _root;
        
        public override void Install(BehaviourEntityView view)
        {
            view.AddBehaviour(new TakeDamageViewBehaviour(_root));
        }
    }
}