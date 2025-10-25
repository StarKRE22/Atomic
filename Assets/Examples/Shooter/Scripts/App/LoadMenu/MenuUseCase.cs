using Cysharp.Threading.Tasks;
using ShooterGame.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShooterGame.App
{
    public static class MenuUseCase
    {
        private const string MENU_NAME = "ShooterGame (Menu)";
        
        public static async UniTaskVoid LoadMenu()
        {
            await SceneManager.LoadSceneAsync(MENU_NAME);
            ScreenUseCase.ShowScreen<StartScreenView>(MenuUI.Instance);
        }

        public static bool InMenu()
        {
            return SceneManager.GetActiveScene().name == MENU_NAME;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Initialize()
        {
            Scene activeScene = SceneManager.GetActiveScene();
            if (activeScene.name == MENU_NAME) 
                ScreenUseCase.ShowScreen<StartScreenView>(MenuUI.Instance);
        }
    }
}