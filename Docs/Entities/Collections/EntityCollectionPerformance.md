# 🔥 Entity Collection Performance

[EntityCollection\<E>](EntityCollection%601.md) is a **hybrid data structure** that combines the strengths of a
**Dictionary** and a **LinkedList**. It maintains **fast lookups** while preserving **insertion order**, making it ideal
for systems that require both efficiency and predictable iteration.

---

The performance measurements below were conducted on a <b>MacBook with Apple M1</b>,
using <b>1,000 elements</b> for each container type. All times are <b>median execution times</b>,
in microseconds (μs).

| Operation      | EntityCollection (μs) | HashSet (μs) |
|----------------|-----------------------|--------------|
| **Add**        | 18.91                 | 77.53        |
| **Clear**      | 13.24                 | 1.09         |
| **Contains**   | 6.00                  | 76.96        |
| **Enumerator** | 7.57                  | 26.44        |
| **Remove**     | 11.50                 | 64.53        |

---

### 🧩 Summary

While `HashSet` excels at bulk operations like `Clear`, `EntityCollection` offers **significantly faster Add, Contains,
and Remove** operations on average — making it a strong choice when **iteration order** and **low allocation overhead**
are essential.
