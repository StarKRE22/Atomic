using UnityEngine;

namespace Atomic.Entities
{
    [AddComponentMenu("Atomic/Entities/Entity Collection View")]
    [DisallowMultipleComponent]
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/UI/EntityCollectionView.md")]
    public class EntityCollectionView : EntityCollectionView<string, IEntity, EntityView>
    {
        protected override string GetKey(IEntity entity) => entity.Name;
    }
}