using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.App
{
    public sealed class AppContextInstaller : SceneEntityInstaller<IAppContext>
    {
        [SerializeField]
        private LevelsInstaller _levelInstaller;

        [SerializeField]
        private ExitAppInstaller exitAppInstaller;

        protected override void Install(IAppContext context)
        {
            _levelInstaller.Install(context);
            exitAppInstaller.Install(context);
        }
    }
}