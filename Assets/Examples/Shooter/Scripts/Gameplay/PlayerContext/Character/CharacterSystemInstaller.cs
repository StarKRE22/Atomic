// using System;
// using Atomic.Contexts;
// using Atomic.Entities;
// using ShooterGame.Gameplay;
// using UnityEngine;
//
// namespace ShooterGame.Gameplay
// {
//     [Serializable]
//     public sealed class CharacterSystemInstaller : IContextInstaller<IPlayerContext>
//     {
//         [SerializeField]
//         private SceneEntity _character;
//
//         public void Install(IPlayerContext context)
//         {
//             context.AddCharacter(_character);
//             context.AddController<CharacterFireController>();
//             context.AddController<CharacterMoveController>();
//             context.AddController<CharacterRespawnController>();
//         }
//     }
// }