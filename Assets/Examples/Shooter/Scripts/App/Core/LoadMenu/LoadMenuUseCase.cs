using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShooterGame.App
{
    public static class LoadMenuUseCase
    {
        private const string MENU_NAME = "ShooterGame (Menu)";
        
        public static async UniTask LoadMenu()
        {
            await SceneManager.LoadSceneAsync(MENU_NAME);
            ScreenUseCase.ShowScreen<StartScreenView>(MenuUIContext.Instance);
        }
    }
}