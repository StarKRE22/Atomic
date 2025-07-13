namespace Atomic.Elements
{
    public delegate void ChangeItemHandler<in T>(int index, T value);
    public delegate void InsertItemHandler<in T>(int index, T value);
    public delegate void DeleteItemHandler<in T>(int index, T value);
    
    public delegate void SetItemHandler<in K, in V>(K key, V value);
    public delegate void AddItemHandler<in K, in V>(K key, V value);
    public delegate void RemoveItemHandler<in K, in V>(K key, V value);

    public delegate void AddItemHandler<in T>(T value);
    public delegate void RemoveItemHandler<in T>(T value);
    
    public delegate void StateChangedHandler();
    public delegate void ClearHandler();
}