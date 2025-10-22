# 📌 Using Constants for Expressions

When you need to add **constant `true` or `false` conditions** to an [
AndExpression](../Elements/Expressions/AndExpression.md) or [
OrExpression](../Elements/Expressions/OrExpression.md) **without creating new allocations**,
you can use pre-defined boolean constants from [DefaultConstants](../Elements/Values/Manual.md#-boolean-constants).

---

## 📑 Table of Contents

- [Why Use This](#-why-use-this)
- [Example of Usage](#-example-of-usage)
- [Conclusion](#-conclusion)

---

## ❓ Why Use This

- Avoids unnecessary object creation for simple `true` / `false` checks.
- Keeps expression logic **lightweight** and **garbage-free**.
- Makes it easy to **temporarily disable** or **reactivate** logic conditions.

---

## 🗂 Example of Usage

```csharp
IExpression<bool> condition = new AndExpression(cond1, cond2, cond3);

// Fully disable the AND condition
condition.Add(Constants.False);

// Reactivate (remove the lock)
condition.Remove(Constants.False);
```

---

## 🏁 Conclusion

Use `Constants.True` and `Constants.False` whenever you need fixed boolean values in your logic graph — 
they are **shared**, **allocation-free**, and perfect for use inside expression systems.
