// using Atomic.Elements;
// using Atomic.Entities;
// using UnityEngine;
//
// namespace SampleGame
// {
//     public sealed class DeathViewBehaviour : IEntityInit, IEntityDispose
//     {
//         private IReactiveValue<int> _health;
//         private GameObject _gameObject;
//         
//         public void Init(in IEntity entity)
//         {
//             _gameObject = entity.GetGameObject();
//             _health = entity.GetHealth();
//             _health.Subscribe(this.OnHealthChanged);
//         }
//         
//         public void Dispose(in IEntity entity)
//         {
//             _health.Unsubscribe(this.OnHealthChanged);
//         }
//     
//         private void OnHealthChanged(int health)
//         {
//             if (health <= 0) 
//                 _gameObject.SetActive(false);
//         }
//     }
// }