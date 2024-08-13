namespace Atomic.Extensions
{
    public interface IEntityConditionAsset : IEntityFunctionAsset<bool>
    {
    }
    
    public interface IEntityConditionAsset<in T> : IEntityFunctionAsset<T, bool>
    {
    }
    
    public interface IEntityConditionAsset<in T1, in T2> : IEntityFunctionAsset<T1, T2, bool>
    {
    }
    
    public interface IEntityConditionAsset<in T1, in T2, in T3> : IEntityFunctionAsset<T1, T2, T3, bool>
    {
    }
}
