# 🔥 ReactiveLinkedList Performance

The performance comparison below was measured on a **MacBook with Apple M1** for collections containing **1000 elements
of type `object`**. The table shows median execution times of key operations.

| Operation  | List <br/>(Median μs) | ReactiveList <br/>(Median μs) | ReactiveLinkedList <br/>(Median μs) |
|------------|-----------------------|-------------------------------|-------------------------------------|
| Add        | 30.15                 | 31.20                         | 60.00                               |
| Clear      | 0.40                  | 1.20                          | 2.65                                |
| Contains   | 1821.35               | 33455.75                      | 33605.80                            |
| CopyTo     | 0.40                  | 0.40                          | 30.65 μs                            |
| Enumerator | 29.35                 | 28.80                         | 28.50 μs                            |
| For        | 1.70                  | 1.70                          | 1273.55                             |
| Get        | 1.50                  | 1.75                          | 1277.70                             |
| Set        | 30.40                 | 42.00                         | 1304.00                             |
| Remove     | 307.40                | 254.25                        | 43.50                               |
| Remove At  | 29.55                 | 3.00                          | 2546.10                             |
| Insert At  | 242.85                | 245.85                        | 60.80                               |

### Explanation

- **Best for frequent insertions / removals** at any position, especially head or tail.
- **Index-based access is slow** (`Get`/`Set`) due to traversal from the head.
- **Always use `foreach` for iteration**; avoid `for` loops.
- **Event notifications** add minor overhead but give real-time updates.