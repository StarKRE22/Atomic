using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public sealed class GameEntityBoard : IEnumerable<Vector2Int>
    {
        private readonly IGameEntity[,] _matrix;
        private readonly Dictionary<IGameEntity, Vector2Int> _lookup;

        public IReadOnlyDictionary<IGameEntity, Vector2Int> Entities => _lookup;

        public int Width => _matrix.GetLength(0);
        public int Height => _matrix.GetLength(1);

        public GameEntityBoard(int width, int height)
        {
            _matrix = new IGameEntity[width, height];
            _lookup = new Dictionary<IGameEntity, Vector2Int>();
        }

        public bool IsFreePosition(Vector2Int position) =>
            InBounds(position) && _matrix[position.x, position.y] == null;

        public bool ContainsEntity(IGameEntity entity) => _lookup.ContainsKey(entity);

        public IGameEntity GetEntity(Vector2Int position) => 
            _matrix[position.x, position.y];

        public bool TryGetEntity(Vector2Int position, out IGameEntity entity)
        {
            if (this.InBounds(position))
            {
                entity = _matrix[position.x, position.y];
                return entity != null;
            }

            entity = null;
            return false;
        }

        public bool TryGetCellPosition(IGameEntity entity, out Vector2Int position) =>
            _lookup.TryGetValue(entity, out position);

        public Vector2Int GetPosition(IGameEntity entity) =>
            _lookup.TryGetValue(entity, out var pos) ? pos : Vector2Int.zero;

        public bool PlaceEntity(IGameEntity entity, Vector2Int position)
        {
            if (!InBounds(position) || !IsFreePosition(position))
                return false;

            if (_lookup.TryGetValue(entity, out var oldPos))
                _matrix[oldPos.x, oldPos.y] = null;

            _matrix[position.x, position.y] = entity;
            _lookup[entity] = position;

            return true;
        }

        public bool RemoveEntity(IGameEntity entity)
        {
            if (!_lookup.TryGetValue(entity, out var pos))
                return false;

            _matrix[pos.x, pos.y] = null;
            _lookup.Remove(entity);
            return true;
        }

        public bool InBounds(Vector2Int position) =>
            position.x >= 0 && position.x < Width &&
            position.y >= 0 && position.y < Height;


        public IEnumerator<Vector2Int> GetEnumerator()
        {
            for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
                yield return new Vector2Int(x, y);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Vector3 GetScreenPosition(Vector2Int target)
        {
            throw new NotImplementedException();
        }
    }
}