# 🔥 ReactiveList Performance

The performance comparison below was measured on a **MacBook with Apple M1** and for collections containing **1000
elements of type `object`**. The table shows median execution times of key operations, illustrating the overhead of the
reactive wrapper.

| Operation  | List (Median μs) | ReactiveList (Median μs) |
|------------|------------------|--------------------------|
| Add        | 30.15            | 31.20                    |
| Clear      | 0.40             | 1.20                     |
| Contains   | 1821.35          | 33455.75                 |
| CopyTo     | 0.40             | 0.40                     |
| Enumerator | 29.35            | 28.80                    |
| For        | 1.70             | 1.70                     |
| Get        | 1.50             | 1.75                     |
| Set        | 30.40            | 42.00                    |
| Remove     | 307.40           | 254.25                   |
| Remove At  | 29.55            | 3.00                     |
| Insert At  | 242.85           | 245.85                   |

`ReactiveList` shows slightly higher latency when setting elements (`Indexer Set`) due to event invocation, but is
faster in some removal operations (`RemoveAt`) thanks to internal optimizations.