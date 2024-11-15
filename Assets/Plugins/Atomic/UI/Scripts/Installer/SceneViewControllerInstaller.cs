using System.Collections.Generic;
using UnityEngine;

namespace Atomic.UI.Installer
{
    [RequireComponent(typeof(SceneViewController))]
    public abstract class SceneViewControllerInstaller : MonoBehaviour
    {
        private void Awake()
        {
            SceneViewController sceneController = this.GetComponent<SceneViewController>();
            sceneController.AddControllers(this.GetControllers());
        }

        protected abstract IEnumerable<IViewController> GetControllers();
    }
}