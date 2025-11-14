using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    [Serializable]
    public sealed class CharacterSystemInstaller : IEntityInstaller<IPlayerContext>
    {
        [SerializeField]
        private GameEntity _characterPrefab;

        public void Install(IPlayerContext context)
        {
            GameContext.TryGetInstance(out GameContext gameContext);
            
            if (AtomicUtils.IsPlayMode())
            {
                GameEntity character = CharacterUseCase.Spawn(context, gameContext, _characterPrefab);
                context.AddCharacter(character);
                gameContext.WhenDisable(character.Disable);
            }

            context.AddBehaviour(new CharacterMoveController(gameContext));
            context.AddBehaviour(new CharacterFireController(gameContext));
            context.AddBehaviour(new CharacterRespawnController(gameContext));
        }
    }
}