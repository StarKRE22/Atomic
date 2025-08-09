using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace ShooterGame.App
{
    public static class LoadMenuUseCase
    {
        private const string MENU_NAME = "ShooterGame (Menu)";
        
        public static UniTask LoadMenu() => 
            SceneManager.LoadSceneAsync(MENU_NAME).ToUniTask();
    }
}