using UnityEngine;

namespace ShooterGame.App
{
    public sealed class MenuLoader : MonoBehaviour
    {
        private void Start()
        {
            LoadMenuUseCase.LoadMenu().Forget();
        }
    }
}