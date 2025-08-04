using System;
using UnityEngine;

namespace SampleGame
{
    public static partial class CollectCoinUseCase
    {
        public static void TryCollectCoin(IGameContext context, IGameEntity character, Collider other)
        {
            if (other.TryGetComponent(out IGameEntity entity) && entity.HasCoinTag()) 
                CollectCoinUseCase.CollectCoin(context, character, entity);
        }
        
        public static void CollectCoin(IGameContext context, IGameEntity character, IGameEntity coin)
        {
            throw new NotImplementedException();
        }
    }
}