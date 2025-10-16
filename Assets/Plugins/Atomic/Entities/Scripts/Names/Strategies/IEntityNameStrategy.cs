namespace Atomic.Entities
{
    public interface IEntityNameStrategy
    {
        int NameToId(string name);
        
        string IdToName(int id);
        
        void Reset();
    }
}