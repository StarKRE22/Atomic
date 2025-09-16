// namespace Atomic.Elements
// {
//     /// <summary>
//     /// Represents a method that handles an event when a value at a specific index in a collection changes.
//     /// </summary>
//     /// <typeparam name="T">The type of the value in the collection.</typeparam>
//     /// <param name="value">The value that was changed or added.</param>
//     /// <param name="index">The zero-based index of the value in the collection.</param>
//     public delegate void ValueIndexEventHandler<in T>(T value, int index);
//
//     /// <summary>
//     /// Represents a method that handles an event for a key-value pair in a dictionary or map.
//     /// </summary>
//     /// <typeparam name="K">The type of the key in the key-value pair.</typeparam>
//     /// <typeparam name="V">The type of the value in the key-value pair.</typeparam>
//     /// <param name="key">The key of the item involved in the event.</param>
//     /// <param name="value">The value associated with the key.</param>
//     public delegate void KeyValueEventHandler<in K, in V>(K key, V value);
//
//     /// <summary>
//     /// Represents a method that handles an event for a single value in a collection.
//     /// </summary>
//     /// <typeparam name="T">The type of the value.</typeparam>
//     /// <param name="value">The value involved in the event.</param>
//     public delegate void ValueEventHandler<in T>(T value);
// }