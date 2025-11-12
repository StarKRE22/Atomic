using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.App
{
    [Serializable]
    public sealed class QuitInstaller : IEntityInstaller<IAppContext>
    {
        [SerializeField]
        private KeyCode _exitKey = KeyCode.Escape;

        public void Install(IAppContext context)
        {
            context.AddExitKeyCode(new Const<KeyCode>(_exitKey));
            context.AddBehaviour<QuitController>();
        }
    }
}