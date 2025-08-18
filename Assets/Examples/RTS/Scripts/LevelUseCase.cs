using UnityEngine;

namespace RTSGame
{
    public static class LevelUseCase
    {
        private const string HEADQUARTERS_NAME = "Headquarters";
        private const string WARRIOR_NAME = "Warrior";
        private const string TANK_NAME = "Tank";

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
            //Spawn headquarters:
            GameEntitiesUseCase.Spawn(context, HEADQUARTERS_NAME, position, rotation, player);

            //Spawn warriors:
            position.x += 5;
            GameEntitiesUseCase.Spawn(context, WARRIOR_NAME, position, rotation, player);

            position.x += 2;
            GameEntitiesUseCase.Spawn(context, WARRIOR_NAME, position, rotation, player);

            position.x += 2;
            GameEntitiesUseCase.Spawn(context, WARRIOR_NAME, position, rotation, player);

            //Spawn tanks:
            position.x += 5;
            GameEntitiesUseCase.Spawn(context, TANK_NAME, position, rotation, player);

            position.x += 5;
            GameEntitiesUseCase.Spawn(context, TANK_NAME, position, rotation, player);

            position.x += 5;
            GameEntitiesUseCase.Spawn(context, TANK_NAME, position, rotation, player);
        }
    }
}