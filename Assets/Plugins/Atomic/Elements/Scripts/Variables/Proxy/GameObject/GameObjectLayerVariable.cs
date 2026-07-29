using UnityEngine;

namespace Atomic.Elements
{
    public sealed class GameObjectLayerVariable : IVariable<int>
    {
        private readonly GameObject _gameObject;

        public GameObjectLayerVariable(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

        public int Value
        {
            get => _gameObject.layer;
            set => _gameObject.layer = value;
        }
    }
}