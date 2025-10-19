using Atomic.Entities;
using UnityEngine;

namespace _DEV
{
    [CreateAssetMenu(
        fileName = "GnomeEntityFactory",
        menuName = "Example/New GnomeEntityFactory"
    )]
    public class GnomeEntityFactory : ScriptableEntityFactory
    {
        protected override void Install(IEntity entity)
        {
            entity.AddTag("Gnome");
        }
    }
}