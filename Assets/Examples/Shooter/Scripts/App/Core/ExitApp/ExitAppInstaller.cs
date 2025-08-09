using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.App
{
    [CreateAssetMenu(
        fileName = "ExitAppInstaller",
        menuName = "ShooterGame/New ExitAppInstaller"
    )]
    public sealed class ExitAppInstaller : ScriptableEntityInstaller<IAppContext>
    {
        [SerializeField]
        private KeyCode _exitKey = KeyCode.Escape;

        protected override void Install(IAppContext context)
        {
            context.AddExitKeyCode(new Const<KeyCode>(_exitKey));
            context.AddBehaviour<ExitAppController>();
        }
    }
}