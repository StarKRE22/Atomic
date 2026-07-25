using System;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame.Debugging
{
    public class GUIEntityCounter : MonoBehaviour
    {
        [SerializeField]
        private GameRunner _gameRunner;
        
        private GUIStyle style;

        private void Awake()
        {
            _gameRunner = FindAnyObjectByType<GameRunner>();
        }

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

            IGameContext gameContext = _gameRunner.GameContext;
            if (gameContext.TryGetEntityWorld(out EntityWorld<IGameEntity> world))
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