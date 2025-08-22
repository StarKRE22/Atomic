using Cysharp.Threading.Tasks;
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
            ScreenUseCase.ShowScreen<StartScreenView>(MenuUIContext.Instance);
        }

        public static bool InMenu() => 
            SceneManager.GetActiveScene().name == MENU_NAME;
    }
}