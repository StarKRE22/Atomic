# 🧩 Collection Delegates

Define contracts for handling changes to items, key-value pairs, and state updates. They provide a lightweight abstraction for event-driven systems or reactive programming.

---

## 🧩 ChangeItemHandler&lt;T&gt;

```csharp
public delegate void ChangeItemHandler<in T>(int index, T value);
```
- **Description:** Handles a change to an item at a specific index.
- **Type parameter:** `T` — the type of the item being changed.
- **Parameters:**
    - `index` — the index of the item.
    - `value` — the new value for the item.
---

## 🧩 InsertItemHandler&lt;T&gt;

```csharp
public delegate void InsertItemHandler<in T>(int index, T value);
```
- **Description:** Handles the insertion of an item at a specific index.
- **Type parameter:** `T` — the type of the item being inserted.
- **Parameters:**
    - `index` — the index where the item is inserted.
    - `value` — the item being inserted.

---

## 🧩 DeleteItemHandler&lt;T&gt;

```csharp
public delegate void DeleteItemHandler<in T>(int index, T value);
```
- **Description:** Handles the deletion of an item at a specific index.
- **Type parameter:** `T` — the type of the item being deleted.
- **Parameters:**
    - `index` — the index from which the item is deleted.
    - `value` — the item that was deleted.

---

## 🧩 SetItemHandler<K, V>

```csharp
public delegate void SetItemHandler<in K, in V>(K key, V value);
```
- **Description:** Handles setting a value in a key-value collection.
- **Type parameters:**
    - `K` — the type of the key.
    - `V` — the type of the value.
- **Parameters:**
    - `key` — the key to set.
    - `value` — the value to assign to the key.

---

## 🧩 AddItemHandler<K, V>

```csharp
public delegate void AddItemHandler<in K, in V>(K key, V value);
```

- **Description:** Handles adding a key-value pair to a collection.
- **Type parameters:**
    - `K` — the type of the key.
    - `V` — the type of the value.
- **Parameters:**
    - `key` — the key of the item being added.
    - `value` — the value of the item being added.

---

## 🧩 RemoveItemHandler<K, V>

```csharp
public delegate void RemoveItemHandler<in K, in V>(K key, V value);
```

- **Description:** Handles removing a key-value pair from a collection.
- **Type parameters:**
    - `K` — the type of the key.
    - `V` — the type of the value.
- **Parameters:**
    - `key` — the key of the item being removed.
    - `value` — the value of the item being removed.

---

## 🧩 AddItemHandler&lt;T&gt;

```csharp
public delegate void AddItemHandler<in T>(T value);
```

- **Description:** Handles adding a single item to a collection.
- **Type parameter:** `T` — the type of the item.
- **Parameters:**
    - `value` — the item to add.

---

## 🧩 RemoveItemHandler&lt;T&gt;

```csharp
public delegate void RemoveItemHandler<in T>(T value);
```

- **Description:** Handles removing a single item from a collection.
- **Type parameter:** `T` — the type of the item.
- **Parameters:**
    - `value` — the item to remove.

---

## 🧩 StateChangedHandler

```csharp
public delegate void StateChangedHandler();
```

- **Description:** Signals that a state change has occurred.

---

## 🧩 ClearHandler

```csharp
public delegate void ClearHandler();
```

- **Description:** Signals that a collection or state should be cleared.
