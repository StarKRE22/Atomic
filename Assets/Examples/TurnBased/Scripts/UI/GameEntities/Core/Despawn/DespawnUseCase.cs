using Atomic.Entities;

namespace Game.UI
{
    public static class DespawnUseCase
    {
        public static void Despawn(this GameEntityView entityView, IUIContext uiContext)
        {
            GameEntityCollectionView collectionView = uiContext.GetValue(UIContextAPI.EntityCollectionView);
            collectionView.Remove(entityView.Entity);
        }
    }
}