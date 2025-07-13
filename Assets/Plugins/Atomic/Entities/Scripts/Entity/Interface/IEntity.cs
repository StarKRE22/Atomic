namespace Atomic.Entities
{
    public partial interface IEntity
    {
        int Id { get; set; }
        
        string Name { get; set; }
        
        void Clear();
    }
}