namespace Atomic.Entities
{
    public interface IEntityCollectionView : IReadOnlyEntityCollectionView
    {
        void AddView(IEntity entity);

        void RemoveView(IEntity entity);

        void ClearViews();
    }
}
