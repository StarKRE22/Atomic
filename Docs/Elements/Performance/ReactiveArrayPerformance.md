# 🔥 ReactiveArray Performance

The performance comparison below was measured on a **MacBook with Apple M1** and for collections containing **1000
elements of type `object`**. The table shows median execution times of key operations, illustrating the overhead of the
reactive wrapper.

| Operation  | Array (Median μs) | ReactiveArray (Median μs) |
|------------|-------------------|---------------------------|
| Get        | 1.10              | 1.20                      |
| Set        | 28.00             | 76.90                     |
| Copy       | 0.70              | 0.50                      |
| Enumerator | 0.80              | 28.50                     |
| For        | 0.70              | 0.70                      |
| Clear      | 0.40              | 41.50                     |

Thus, `ReactiveArray` performs almost as fast as a regular array for reading operations. It is well-suited for scenarios
where element change notifications are needed. However, **iterating** with `foreach` or **writing** to an element is *
*noticeably** **slower** due to event invocations.  