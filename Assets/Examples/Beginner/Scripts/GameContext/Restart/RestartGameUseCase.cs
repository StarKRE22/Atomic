using UnityEngine;
using UnityEngine.SceneManagement;

namespace BeginnerGame
{
    public static class RestartGameUseCase
    {
        private const string SCENE_NAME = "BeginnerGame";
        
        public static void RestartGame()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SCENE_NAME, LoadSceneMode.Single);
        }
    }
}