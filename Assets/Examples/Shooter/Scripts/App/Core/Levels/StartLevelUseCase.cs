using UnityEngine.SceneManagement;

namespace ShooterGame.App
{
    public static class StartLevelUseCase
    {
        private const string LEVEL_NAME = "Level";

        public static void StartLevel(int level)
        {
            SceneManager.LoadScene($"{LEVEL_NAME} {level}", LoadSceneMode.Single);
        }
    }
}