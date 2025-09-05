using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public sealed class WarriorViewInstaller : EntityViewInstaller
    {
        [SerializeField]
        private Transform _root;

        [SerializeField]
        private Transform _weapon;
        
        public override void Install(BehaviourEntityView view)
        {
            view.AddBehaviour(new TakeDamageViewBehaviour(_root));
            view.AddBehaviour(new WeaponRecoilViewBehaviour(_weapon)); 
        }
    }
}