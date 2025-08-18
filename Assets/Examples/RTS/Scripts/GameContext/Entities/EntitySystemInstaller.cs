// using System;
// using Atomic.Contexts;
// using Atomic.Entities;
// using UnityEngine;
//
// namespace RTSGame
// {
//     [Serializable]
//     public sealed class EntitySystemInstaller : IContextInstaller<GameContext>
//     {
//         [SerializeField]
//         private ScriptableEntityCatalog _catalog;
//
//         public void Install(GameContext context)
//         {
//             this.InstallPool(context);
//             this.InstallWorld(context);
//         }
//
//         private void InstallPool(GameContext context)
//         {
//             context.AddEntityPool(new GenericEntityPool(new GenericEntityFactory(_catalog)));
//         }
//
//         private void InstallWorld(GameContext context)
//         {
//             EntityWorld entityWorld = new EntityWorld("Entities");
//             context.AddEntityWorld(entityWorld);
//             context.WhenInit(entityWorld.Init);
//             context.WhenEnable(entityWorld.Enable);
//             context.WhenUpdate(entityWorld.OnUpdate);
//             context.WhenFixedUpdate(entityWorld.OnFixedUpdate);
//             context.WhenLateUpdate(entityWorld.OnLateUpdate);
//             context.WhenDisable(entityWorld.Disable);
//             context.WhenDispose(entityWorld.Dispose);
//         }
//     }
// }