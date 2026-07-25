using UnityEngine;

namespace RTSGame
{
    public static class InitGameCase
    {
        public static void SpawnInitialUnits(this IGameContext context, int columns = 10)
        {
            const float stepX = 10f;

            const float blueBaseZ = 5f;     // нижняя часть карты
            const float redBaseZ = 50f;     // верхняя часть карты

            for (int i = 0; i < columns; i++)
            {
                float x = i * stepX;

                context.SpawnRow(TeamType.BLUE, x, blueBaseZ, Quaternion.identity);
                context.SpawnRow(TeamType.RED, x, redBaseZ, Quaternion.Euler(0, 180, 0));
            }
        }

        private static void SpawnRow(
            this IGameContext context,
            TeamType team,
            float x,
            float baseZ,
            Quaternion rotation
        )
        {
            const float rowSpacing = 3f;

            // 🔥 направление "вперёд"
            float dir = team == TeamType.BLUE ? +1f : -1f;

            // 👉 формируем линию от базы назад → вперёд
            float tankZ = baseZ + dir * rowSpacing;
            float infantryZ = baseZ + dir * rowSpacing * 2f;

            // 🔥 порядок одинаковый для обеих команд!
    
            // 🏠 Штаб (самый сзади)
            context.SpawnSingle(GameEntityType.Headquarters, team, x, baseZ, rotation);

            // 🚜 Танки (середина)
            context.SpawnLine(GameEntityType.Tank, team, x, tankZ, 3f, 3, rotation);

            // ⚔️ Пехота (впереди)
            context.SpawnLine(GameEntityType.Warrior, team, x, infantryZ, 2f, 3, rotation);
        }

        // 🔥 универсальный спавн линии
        private static void SpawnLine(
            this IGameContext context,
            GameEntityType unitType,
            TeamType team,
            float startX,
            float z,
            float step,
            int count,
            Quaternion rotation
        )
        {
            float x = startX;

            for (int i = 0; i < count; i++)
            {
                Vector3 pos = new Vector3(x, 0f, z);
                context.Spawn(unitType, pos, rotation, team);

                x += step;
            }
        }

        // 🔥 одиночный объект
        private static void SpawnSingle(
            this IGameContext context,
            GameEntityType unitType,
            TeamType team,
            float x,
            float z,
            Quaternion rotation
        )
        {
            Vector3 pos = new Vector3(x, 0f, z);
            context.Spawn(unitType, pos, rotation, team);
        }
    }
}