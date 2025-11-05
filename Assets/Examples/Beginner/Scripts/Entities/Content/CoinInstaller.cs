using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    /// <summary>
    /// Installs all required data and tags for a coin entity in the scene.
    /// </summary>
    /// <remarks>
    /// This installer defines a collectible coin entity by attaching identifying tags and 
    /// value components necessary for gameplay interaction.
    ///
    /// <para>
    /// The coin provides a <see cref="ReactiveVariable{T}"/> value representing its worth in money units, 
    /// which is read by other entities (e.g., player characters) upon trigger collisions.
    /// </para>
    /// </remarks>
    public sealed class CoinInstaller : SceneEntityInstaller
    {
        [SerializeField]
        private ReactiveVariable<int> _money = 1;

        public override void Install(IEntity entity)
        {
            entity.AddCoinTag();
            entity.AddTransform(this.transform);
            entity.AddMoney(_money);
        }
    }
}