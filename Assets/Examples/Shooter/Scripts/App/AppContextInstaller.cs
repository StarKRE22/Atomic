using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.App
{
    public sealed class AppContextInstaller : SceneEntityInstaller<IAppContext>
    {
        [SerializeField]
        private QuitInstaller _exitAppInstaller;

        [SerializeField]
        private LevelsInstaller _levelsInstaller;

        [SerializeField]
        private GameLoadingInstaller _loadGameInstaller;

        public override void Install(IAppContext context)
        {
            _exitAppInstaller.Install(context);
            _levelsInstaller.Install(context);
            _loadGameInstaller.Install(context);
            context.AddBehaviour<MenuLoadController>();
        }
    }
}