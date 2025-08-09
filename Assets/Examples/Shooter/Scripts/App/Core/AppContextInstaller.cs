using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.App
{
    [CreateAssetMenu(
        fileName = "AppContextInstaller",
        menuName = "ShooterGame/New AppContextInstaller"
    )]
    public sealed class AppContextInstaller : SceneEntityInstaller<IAppContext>
    {
        [SerializeField]
        private ExitAppInstaller _exitAppInstaller;

        [SerializeField]
        private LevelsInstaller _levelsInstaller;

        [SerializeField]
        private LoadGameInstaller _loadGameInstaller;
        
        protected override void Install(IAppContext context)
        {
            _exitAppInstaller.Install(context);
            _levelsInstaller.Install(context);
            _loadGameInstaller.Install(context);
            
            context.WhenActivate(() =>
            {
                Debug.Log("ACTIVATE APP");
                LoadMenuUseCase.LoadMenu();
            });
        }
    }
}