using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    //BFS
    public sealed class GameBoardPathFinder
    {
        private static readonly Vector2Int[] s_directions =
        {
            new(0, 1),
            new(1, 0),
            new(0, -1),
            new(-1, 0),
            new(1, 1),
            new(-1, 1),
            new(-1, -1),
            new(1, -1),
        };

        private readonly GameEntityBoard _board;
        private readonly int _width;
        private readonly int _height;

        private readonly bool[,] _visited;
        private readonly Vector2Int[,] _previous;

        private readonly Queue<Vector2Int> _queue = new();

        public GameBoardPathFinder(GameEntityBoard board)
        {
            _board = board;
            _width = board.Width;
            _height = board.Height;

            _visited = new bool[_width, _height];
            _previous = new Vector2Int[_width, _height];
        }
        
        public List<Vector2Int> FindPath(IGameEntity entity, IGameEntity target)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (target == null)
                throw new ArgumentNullException(nameof(target));

            Vector2Int startPosition = _board.GetPosition(entity);
            Vector2Int endPosition = _board.GetPosition(target);
            return FindPath(startPosition, endPosition);
        }

        public List<Vector2Int> FindPath(Vector2Int start, Vector2Int end)
        {
            Reset();

            _queue.Enqueue(start);
            _visited[start.x, start.y] = true;

            while (_queue.Count > 0)
            {
                var current = _queue.Dequeue();

                if (current == end)
                    break;

                foreach (var dir in s_directions)
                {
                    var next = current + dir;

                    if (!IsInBounds(next))
                        continue;

                    if (_visited[next.x, next.y])
                        continue;

                    if (!_board.IsFreePosition(next) && next != end)
                        continue;

                    _visited[next.x, next.y] = true;
                    _previous[next.x, next.y] = current;

                    _queue.Enqueue(next);
                }
            }

            if (!_visited[end.x, end.y])
                return null;

            return ReconstructPath(start, end);
        }

        private List<Vector2Int> ReconstructPath(Vector2Int start, Vector2Int end)
        {
            var path = new List<Vector2Int>();
            var current = end;

            while (current != start)
            {
                path.Add(current);
                current = _previous[current.x, current.y];
            }

            path.Reverse();
            return path;
        }

        private void Reset()
        {
            Array.Clear(_visited, 0, _visited.Length);
            _queue.Clear();
        }

        private bool IsInBounds(Vector2Int pos) =>
            pos.x >= 0 && pos.x < _width &&
            pos.y >= 0 && pos.y < _height;
    }
}