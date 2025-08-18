using UnityEngine;

namespace RTSGame
{
    public static class LevelUseCase
    {
        public static void LoadLevel(IGameContext context)
        {
            for (int i = 0; i < 100; i++)
            {
                SpawnUnits(context, TeamType.BLUE, new Vector3(10 * i, 0, -10), Quaternion.Euler(0, 0, 0));
                SpawnUnits(context, TeamType.RED, new Vector3(10 * i, 0, 10), Quaternion.Euler(0, 180, 0));
            }
        }

        private static void SpawnUnits(IGameContext context, TeamType player, Vector3 position, Quaternion rotation)
        {
            //Spawn base:
            GameEntitiesUseCase.Spawn(context, "Headquarters", position, rotation, player);

            //Spawn warriors:
            position.x += 5;
            GameEntitiesUseCase.Spawn(context, "Warrior", position, rotation, player);

            position.x += 2;
            GameEntitiesUseCase.Spawn(context, "Warrior", position, rotation, player);

            position.x += 2;
            GameEntitiesUseCase.Spawn(context, "Warrior", position, rotation, player);

            //Spawn tanks:
            position.x += 5;
            GameEntitiesUseCase.Spawn(context, "Tank", position, rotation, player);

            position.x += 5;
            GameEntitiesUseCase.Spawn(context, "Tank", position, rotation, player);

            position.x += 5;
            GameEntitiesUseCase.Spawn(context, "Tank", position, rotation, player);
        }
    }
}