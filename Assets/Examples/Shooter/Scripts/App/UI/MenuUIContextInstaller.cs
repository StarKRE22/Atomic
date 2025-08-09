using Atomic.Entities;
using ShooterGame.UI;
using UnityEngine;

namespace ShooterGame.App
{
    public sealed class MenuUIContextInstaller : SceneEntityInstaller<IMenuUIContext>
    {
        [SerializeField]
        private MainMenuView _menuView;
        
        protected override void Install(IMenuUIContext entity)
        {
            //Main menu
            entity.AddMainMenuView(_menuView);
            entity.AddBehaviour<MainMenuPresenter>();
        }
    }
}