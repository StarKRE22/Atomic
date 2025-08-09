using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.App
{
    [CreateAssetMenu(
        fileName = "AppContextInstaller",
        menuName = "ShooterGame/New AppContextInstaller"
    )]
    public sealed class AppContextInstaller : ScriptableEntityInstaller<IAppContext>
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
            
            context.WhenActivate(() => LoadMenuUseCase.LoadMenu());
        }
    }
}