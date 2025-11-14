using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpatialHash<T>
{
    private readonly float cellSize;
    private readonly Dictionary<Vector3Int, List<T>> grid;
    private readonly Queue<List<T>> freeLists;
    private readonly IAdapter adapter;

    public SpatialHash(
        float cellSize,
        IAdapter adapter,
        int initialCapacity = 32,
        int freeListCapacity = 16
    )
    {
        this.cellSize = cellSize;
        this.adapter = adapter;
        this.grid = new Dictionary<Vector3Int, List<T>>(initialCapacity);
        this.freeLists = new Queue<List<T>>(freeListCapacity);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Vector3Int Hash(float x, float y, float z) => new(
        (int) (x / cellSize),
        (int) (y / cellSize),
        (int) (z / cellSize)
    );

    private List<T> GetListFromPool()
    {
        return freeLists.Count > 0
            ? freeLists.Dequeue()
            : new List<T>(32);
    }

    private void ReturnList(List<T> list)
    {
        list.Clear();
        freeLists.Enqueue(list);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector3Int HashPublic(Vector3 p)
    {
        return new Vector3Int(
            (int) (p.x / cellSize),
            (int) (p.y / cellSize),
            (int) (p.z / cellSize)
        );
    }

    public void FastMove(T obj, Vector3Int oldKey, Vector3Int newKey)
    {
        // Удаляем из старой клетки
        if (grid.TryGetValue(oldKey, out var list))
        {
            list.Remove(obj);
            if (list.Count == 0)
            {
                grid.Remove(oldKey);
                ReturnList(list);
            }
        }

        // Добавляем в новую клетку
        if (!grid.TryGetValue(newKey, out var newList))
        {
            newList = GetListFromPool();
            grid[newKey] = newList;
        }

        newList.Add(obj);
    }


    // Добавить объект
    public void Insert(T obj, out Vector3Int key)
    {
        var pos = adapter.GetPosition(obj);
        key = Hash(pos.x, pos.y, pos.z);

        if (!grid.TryGetValue(key, out var list))
        {
            list = GetListFromPool();
            grid[key] = list;
        }

        list.Add(obj);
    }

    // Добавить объект (без out)
    public void Insert(T obj)
    {
        Vector3 pos = adapter.GetPosition(obj);
        Vector3Int key = Hash(pos.x, pos.y, pos.z);

        if (!grid.TryGetValue(key, out var list))
        {
            list = GetListFromPool();
            grid[key] = list;
        }

        list.Add(obj);
    }

    // Обновить позицию (передаётся старая позиция)
    public void Update(T obj, Vector3 oldPosition)
    {
        Vector3 newPos = adapter.GetPosition(obj);

        Vector3Int oldKey = Hash(oldPosition.x, oldPosition.y, oldPosition.z);
        Vector3Int newKey = Hash(newPos.x, newPos.y, newPos.z);

        if (oldKey == newKey)
            return;

        if (grid.TryGetValue(oldKey, out var list))
        {
            list.Remove(obj);

            if (list.Count == 0)
            {
                grid.Remove(oldKey);
                ReturnList(list);
            }
        }

        Insert(obj);
    }

    /// <summary>
    /// Быстрый поиск объектов в кубе вокруг точки с использованием заранее выделенного массива.
    /// </summary>
    /// <param name="center">Центр поиска.</param>
    /// <param name="radius">Радиус поиска.</param>
    /// <param name="buffer">Предварительно выделенный массив для результатов.</param>
    /// <returns>Количество найденных объектов.</returns>
    public int Query(Vector3 center, float radius, T[] buffer)
    {
        if (buffer == null)
            throw new ArgumentNullException(nameof(buffer));

        int count = 0;

        int minX = (int) ((center.x - radius) / cellSize);
        int maxX = (int) ((center.x + radius) / cellSize);
        int minY = (int) ((center.y - radius) / cellSize);
        int maxY = (int) ((center.y + radius) / cellSize);
        int minZ = (int) ((center.z - radius) / cellSize);
        int maxZ = (int) ((center.z + radius) / cellSize);

        for (int cx = minX; cx <= maxX; cx++)
        for (int cy = minY; cy <= maxY; cy++)
        for (int cz = minZ; cz <= maxZ; cz++)
            if (grid.TryGetValue(new Vector3Int(cx, cy, cz), out var list))
                for (int i = 0, length = list.Count; i < length; i++)
                    if (count < buffer.Length)
                        buffer[count++] = list[i];
                    else
                        break; // массив переполнен, остальное игнорируем

        return count;
    }

    public bool Remove(T obj)
    {
        Vector3 pos = adapter.GetPosition(obj);
        Vector3Int key = Hash(pos.x, pos.y, pos.z);

        if (!grid.TryGetValue(key, out var list))
            return false;

        bool removed = list.Remove(obj);
        if (!removed)
            return false;

        if (list.Count == 0)
        {
            grid.Remove(key);
            ReturnList(list);
        }

        return true;
    }

    /// <summary>
    /// Находит все объекты в радиусе и складывает их в массив.
    /// </summary>
    /// <param name="center">Центр сферы поиска.</param>
    /// <param name="radius">Радиус поиска.</param>
    /// <param name="buffer">Предварительно выделенный массив для результатов.</param>
    /// <returns>Количество найденных объектов.</returns>
    public int QueryRadius(Vector3 center, float radius, T[] buffer)
    {
        int count = 0;

        int minX = (int) ((center.x - radius) / cellSize);
        int maxX = (int) ((center.x + radius) / cellSize);
        int minY = (int) ((center.y - radius) / cellSize);
        int maxY = (int) ((center.y + radius) / cellSize);
        int minZ = (int) ((center.z - radius) / cellSize);
        int maxZ = (int) ((center.z + radius) / cellSize);

        float radiusSqr = radius * radius;

        for (int cx = minX; cx <= maxX; cx++)
        for (int cy = minY; cy <= maxY; cy++)
        for (int cz = minZ; cz <= maxZ; cz++)
            if (grid.TryGetValue(new Vector3Int(cx, cy, cz), out List<T> list))
                for (int i = 0, length = list.Count; i < length; i++)
                    if ((adapter.GetPosition(list[i]) - center).sqrMagnitude <= radiusSqr)
                        if (count < buffer.Length)
                            buffer[count++] = list[i];
                        else
                            break; // массив переполнен, остальное игнорируем

        return count;
    }


    // Интерфейс адаптера
    public interface IAdapter
    {
        Vector3 GetPosition(T item);
    }
}