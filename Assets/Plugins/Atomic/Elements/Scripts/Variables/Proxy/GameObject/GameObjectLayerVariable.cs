using UnityEngine;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a variable that provides access to a <see cref="GameObject"/>'s layer.
    /// </summary>
    public sealed class GameObjectLayerVariable : IVariable<int>
    {
        private readonly GameObject _gameObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObjectLayerVariable"/> class.
        /// </summary>
        /// <param name="gameObject">
        /// The <see cref="GameObject"/> whose layer is exposed as a variable.
        /// </param>
        public GameObjectLayerVariable(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

        /// <summary>
        /// Gets or sets the layer of the associated <see cref="GameObject"/>.
        /// </summary>
        /// <value>
        /// The layer index assigned to the <see cref="GameObject"/>.
        /// </value>
        public int Value
        {
            get => _gameObject.layer;
            set => _gameObject.layer = value;
        }
    }
}
