# 🔥 ReactiveDictionary Performance

The performance comparison below was measured on a **MacBook with Apple M1** for collections containing **1000 elements
of type `object`**. The table shows median execution times of key operations, illustrating the overhead of the reactive
wrapper compared to a standard `Dictionary`.

| Operation   | Dictionary (Median μs) | ReactiveDictionary (Median μs) |
|-------------|------------------------|--------------------------------|
| Add         | 34.10                  | 64.40                          |
| Clear       | 7.10                   | 2.40                           |
| ContainsKey | 7.10                   | 5.70                           |
| Enumerator  | 56.60                  | 58.60                          |
| Get         | 7.40                   | 5.80                           |
| Set         | 35.50                  | 10.10                          |
| Remove      | 7.40                   | 6.80                           |
| TryGetValue | 34.20                  | 32.90                          |

Thus, `ReactiveDictionary` introduces minimal overhead for common operations like `Add` and `Clear`. In some operations
like `Indexer Get` and `TryGetValue`, it can even be slightly faster due to internal optimizations. `Remove` and
`Indexer Set` may have slightly higher latency compared to a standard `Dictionary` because of event invocation and
reactive state management.