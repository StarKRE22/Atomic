using UnityEngine;

namespace RTSGame
{
    public static class InitGameCase
    {
        private const string HEADQUARTERS_NAME = "Headquarters";
        private const string WARRIOR_NAME = "Warrior";
        private const string TANK_NAME = "Tank";

        public static void SpawnUnits(IGameContext context, int columns = 10)
        {
            const float step = 10f;
            const float offsetBetweenTeams = -50f; 

            for (int i = 0; i < columns; i++)
            {
                float x = i * step;

                // Синие — пехота, танки, штабы
                SpawnRow(context, TeamType.BLUE, x, 0f, Quaternion.identity);

                // Красные — штабы, танки, пехота
                SpawnRow(context, TeamType.RED, x, offsetBetweenTeams, Quaternion.Euler(0, 180, 0));

            }
        }

        private static void SpawnRow(IGameContext context, TeamType team, float x, float zOffset, Quaternion rotation)
        {
            float infantryZ = zOffset; // ближе всего
            float tankZ = zOffset + 3f; // чуть дальше
            float hqZ = zOffset + 6f; // ещё дальше

            if (team == TeamType.BLUE)
            {
                // Пехота
                Vector3 infantryPos = new Vector3(x, 0, -infantryZ);
                UnitsUseCase.Spawn(context, WARRIOR_NAME, infantryPos, rotation, team);
                infantryPos.x += 2;
                UnitsUseCase.Spawn(context, WARRIOR_NAME, infantryPos, rotation, team);
                infantryPos.x += 2;
                UnitsUseCase.Spawn(context, WARRIOR_NAME, infantryPos, rotation, team);

                // Танки
                Vector3 tankPos = new Vector3(x, 0, -tankZ);
                UnitsUseCase.Spawn(context, TANK_NAME, tankPos, rotation, team);
                tankPos.x += 3;
                UnitsUseCase.Spawn(context, TANK_NAME, tankPos, rotation, team);
                tankPos.x += 3;
                UnitsUseCase.Spawn(context, TANK_NAME, tankPos, rotation, team);

                // Штаб
                Vector3 hqPos = new Vector3(x, 0, -hqZ);
                UnitsUseCase.Spawn(context, HEADQUARTERS_NAME, hqPos, rotation, team);
            }
            else
            {
                // Красные зеркально: штабы ближе, потом танки, потом пехота
                Vector3 hqPos = new Vector3(x, 0, -infantryZ);
                UnitsUseCase.Spawn(context, HEADQUARTERS_NAME, hqPos, rotation, team);

                Vector3 tankPos = new Vector3(x, 0, -tankZ);
                UnitsUseCase.Spawn(context, TANK_NAME, tankPos, rotation, team);
                tankPos.x += 3;
                UnitsUseCase.Spawn(context, TANK_NAME, tankPos, rotation, team);
                tankPos.x += 3;
                UnitsUseCase.Spawn(context, TANK_NAME, tankPos, rotation, team);

                Vector3 infantryPos = new Vector3(x, 0, -hqZ);
                UnitsUseCase.Spawn(context, WARRIOR_NAME, infantryPos, rotation, team);
                infantryPos.x += 2;
                UnitsUseCase.Spawn(context, WARRIOR_NAME, infantryPos, rotation, team);
                infantryPos.x += 2;
                UnitsUseCase.Spawn(context, WARRIOR_NAME, infantryPos, rotation, team);
            }
        }
    }
}