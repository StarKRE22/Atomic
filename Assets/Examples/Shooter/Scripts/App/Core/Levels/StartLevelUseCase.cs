using UnityEngine.SceneManagement;

namespace ShooterGame.App
{
    public static class StartLevelUseCase
    {
        private const string LEVEL_NAME_FORMAT = "ShooterGame (Level{0})";
        
        public static void StartCurrentLevel(IAppContext context) => 
            StartLevel(context.GetCurrentLevel().Value);

        public static void StartLevel(int level)
        {
            string sceneName = string.Format(LEVEL_NAME_FORMAT, level);
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
}