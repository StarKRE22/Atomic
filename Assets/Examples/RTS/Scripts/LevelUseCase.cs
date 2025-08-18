// using UnityEngine;
//
// namespace RTSGame
// {
//     public static class LevelUseCase
//     {
//         public static void LoadLevel(in GameContext context)
//         {
//             for (int i = 0; i < 100; i++)
//             {
//                 SpawnUnits(context, TeamType.BLUE, new Vector3(10 * i, 0, -10), Quaternion.Euler(0, 0, 0));
//                 SpawnUnits(context, TeamType.RED, new Vector3(10 * i, 0, 10), Quaternion.Euler(0, 180, 0));
//             }
//         }
//
//         private static void SpawnUnits(in GameContext context, TeamType player, Vector3 position, Quaternion rotation)
//         {
//             //Spawn base:
//             EntitiesUseCase.SpawnEntity(context, "Headquarters", position, rotation, player);
//
//             //Spawn warriors:
//             position.x += 5;
//             EntitiesUseCase.SpawnEntity(context, "Warrior", position, rotation, player);
//
//             position.x += 2;
//             EntitiesUseCase.SpawnEntity(context, "Warrior", position, rotation, player);
//
//             position.x += 2;
//             EntitiesUseCase.SpawnEntity(context, "Warrior", position, rotation, player);
//
//             //Spawn tanks:
//             position.x += 5;
//             EntitiesUseCase.SpawnEntity(context, "Tank", position, rotation, player);
//
//             position.x += 5;
//             EntitiesUseCase.SpawnEntity(context, "Tank", position, rotation, player);
//
//             position.x += 5;
//             EntitiesUseCase.SpawnEntity(context, "Tank", position, rotation, player);
//         }
//     }
// }