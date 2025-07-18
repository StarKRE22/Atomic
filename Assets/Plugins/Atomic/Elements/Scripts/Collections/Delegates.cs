namespace Atomic.Elements
{
    /// <summary>
    /// Delegate for handling a change to an item at a specific index.
    /// </summary>
    /// <typeparam name="T">The type of the item being changed.</typeparam>
    /// <param name="index">The index of the item.</param>
    /// <param name="value">The new value for the item.</param>
    public delegate void ChangeItemHandler<in T>(int index, T value);

    /// <summary>
    /// Delegate for handling the insertion of an item at a specific index.
    /// </summary>
    /// <typeparam name="T">The type of the item being inserted.</typeparam>
    /// <param name="index">The index where the item is inserted.</param>
    /// <param name="value">The item being inserted.</param>
    public delegate void InsertItemHandler<in T>(int index, T value);

    /// <summary>
    /// Delegate for handling the deletion of an item at a specific index.
    /// </summary>
    /// <typeparam name="T">The type of the item being deleted.</typeparam>
    /// <param name="index">The index from which the item is deleted.</param>
    /// <param name="value">The item that was deleted.</param>
    public delegate void DeleteItemHandler<in T>(int index, T value);

    /// <summary>
    /// Delegate for setting a value in a key-value collection.
    /// </summary>
    /// <typeparam name="K">The type of the key.</typeparam>
    /// <typeparam name="V">The type of the value.</typeparam>
    /// <param name="key">The key to set.</param>
    /// <param name="value">The value to assign to the key.</param>
    public delegate void SetItemHandler<in K, in V>(K key, V value);

    /// <summary>
    /// Delegate for adding a key-value pair to a collection.
    /// </summary>
    /// <typeparam name="K">The type of the key.</typeparam>
    /// <typeparam name="V">The type of the value.</typeparam>
    /// <param name="key">The key of the item being added.</param>
    /// <param name="value">The value of the item being added.</param>
    public delegate void AddItemHandler<in K, in V>(K key, V value);

    /// <summary>
    /// Delegate for removing a key-value pair from a collection.
    /// </summary>
    /// <typeparam name="K">The type of the key.</typeparam>
    /// <typeparam name="V">The type of the value.</typeparam>
    /// <param name="key">The key of the item being removed.</param>
    /// <param name="value">The value of the item being removed.</param>
    public delegate void RemoveItemHandler<in K, in V>(K key, V value);

    /// <summary>
    /// Delegate for adding a single item to a collection.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="value">The item to add.</param>
    public delegate void AddItemHandler<in T>(T value);

    /// <summary>
    /// Delegate for removing a single item from a collection.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="value">The item to remove.</param>
    public delegate void RemoveItemHandler<in T>(T value);

    /// <summary>
    /// Delegate for signaling that a state change has occurred.
    /// </summary>
    public delegate void StateChangedHandler();

    /// <summary>
    /// Delegate for clearing a collection or state.
    /// </summary>
    public delegate void ClearHandler();
}