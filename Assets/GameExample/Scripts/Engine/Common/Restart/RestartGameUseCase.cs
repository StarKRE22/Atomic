using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameExample.Engine
{
    public sealed class RestartGameUseCase
    {
        private const string SCENE_NAME = "GameExample";
        
        public static void RestartGame()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SCENE_NAME, LoadSceneMode.Single);
        }
    }
}