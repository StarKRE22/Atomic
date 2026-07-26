using Atomic.Entities;
using UnityEngine;

namespace Game.UI
{
    public sealed class CameraBillboard : MonoBehaviour
    {
        private Transform _camera;

        private void Start()
        {
            Camera camera = UIContext.Instance.GetValue(UIContextAPI.Camera);
            _camera = camera.transform;
        }

        private void LateUpdate()
        {
            if (_camera != null) 
                this.transform.LookAt(this.transform.position + _camera.forward);
        }
    }
}