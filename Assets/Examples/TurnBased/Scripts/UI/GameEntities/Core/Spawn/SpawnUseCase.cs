using Atomic.Entities;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Gameplay;
using UnityEngine;

namespace Game.UI
{
    public static class SpawnUseCase
    {
        public static async UniTask SpawnEntityView(this IUIContext context, IGameEntity entity, Vector2Int position)
        {
            GameEntityCollectionView collectionView = context.GetValue(UIContextAPI.EntityCollectionView);
            GameEntityView view = collectionView.Add(entity);
            
            Transform transform = view.GetValue(GameEntityViewAPI.Transform);
            transform.position = context.GetWorldPosition(position);
            
            await transform
                .DOScale(Vector3.one, 0.5f)
                .ChangeStartValue(Vector3.zero)
                .SetLink(transform.gameObject)
                .AsyncWaitForCompletion();
        }
    }
}