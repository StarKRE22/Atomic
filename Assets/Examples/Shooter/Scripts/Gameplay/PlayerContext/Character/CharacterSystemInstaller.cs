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
        private Actor _characterPrefab;

        public void Install(IPlayerContext context)
        {
            if (AtomicUtils.IsPlayMode())
            {
                GameContext gameContext = GameContext.Instance;
                Actor character = CharacterUseCase.Spawn(context, gameContext, _characterPrefab);
                context.AddCharacter(character);
                gameContext.WhenDisable(character.Disable);
            }

            context.AddBehaviour<CharacterMoveController>();
            context.AddBehaviour<CharacterFireController>();
            context.AddBehaviour<CharacterRespawnController>();
        }
    }
}