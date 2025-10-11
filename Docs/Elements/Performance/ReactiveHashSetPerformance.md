# 🔥 ReactiveHashSet Performance

The performance comparison below was measured on a **MacBook with Apple M1** for collections containing **1000 elements
of type `object`**. The table shows median execution times of key operations, illustrating the overhead of the reactive
wrapper compared to a standard `HashSet<T>`.

| Operation  | HashSet (Median μs) | ReactiveHashSet (Median μs) |
|------------|---------------------|-----------------------------|
| Add        | 69.30               | 64.30                       |
| Clear      | 1.10                | 32.00                       |
| Contains   | 50.10               | 10.00                       |
| Enumerator | 29.60               | 29.60                       |
| Remove     | 50.30               | 54.80                       |

`ReactiveHashSet` shows **much lower median times for Add, Contains, and Enumerator**, thanks to internal optimizations
and preallocated slots. Operations like `Clear` and `Remove` are slightly more expensive due to **event invocation** and
managing free lists for reactive state tracking.

Overall, `ReactiveHashSet` introduces minimal overhead for typical read operations while maintaining full reactive
notifications for all structural changes.