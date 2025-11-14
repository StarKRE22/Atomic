using Atomic.Entities;
using UnityEngine;

namespace RTSGame.UI
{
    public class GUIEntityCounter : MonoBehaviour
    {
        private GUIStyle style;

        private void Start()
        {
            style = new GUIStyle();
            style.fontSize = 32;
            style.normal.textColor = Color.black;
        }

        private void OnGUI()
        {
            int count = 0;
            bool available = false;

            GameContext gameContext = GameContext.Instance;
            if (gameContext.TryGetEntityWorld(out EntityWorld<IUnit> world))
            {
                count = world.Count;
                available = true;
            }

            GUI.Label(
                new Rect(10, 10, 300, 30),
                available ? $"Active Entities: {count}" : "Active Entities: N/A",
                style
            );
        }
    }
}