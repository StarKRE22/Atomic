using UnityEngine;

namespace RTSGame
{
    public static class InitGameCase
    {
        private const string HEADQUARTERS_NAME = "Headquarters";
        private const string WARRIOR_NAME = "Warrior";
        private const string TANK_NAME = "Tank";

        public static void SpawnUnits(IGameContext context, int count = 100)
        {
            for (int i = 0; i < count; i++)
            {
                SpawnUnits(context, TeamType.BLUE, new Vector3(10 * i, 0, -10), Quaternion.Euler(0, 0, 0));
                SpawnUnits(context, TeamType.RED, new Vector3(10 * i, 0, 10), Quaternion.Euler(0, 180, 0));
            }
        }

        private static void SpawnUnits(IGameContext context, TeamType player, Vector3 position, Quaternion rotation)
        {
            //Spawn headquarters:
            GameEntityUseCase.Spawn(context, HEADQUARTERS_NAME, position, rotation, player);

            //Spawn warriors:
            position.x += 5;
            GameEntityUseCase.Spawn(context, WARRIOR_NAME, position, rotation, player);

            position.x += 2;
            GameEntityUseCase.Spawn(context, WARRIOR_NAME, position, rotation, player);

            position.x += 2;
            GameEntityUseCase.Spawn(context, WARRIOR_NAME, position, rotation, player);

            //Spawn tanks:
            position.x += 5;
            GameEntityUseCase.Spawn(context, TANK_NAME, position, rotation, player);

            position.x += 5;
            GameEntityUseCase.Spawn(context, TANK_NAME, position, rotation, player);

            position.x += 5;
            GameEntityUseCase.Spawn(context, TANK_NAME, position, rotation, player);
        }
    }
}