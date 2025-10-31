using UnityEngine;

namespace BeginnerGame
{
    public sealed class CameraBillboard : MonoBehaviour
    {
        private void LateUpdate()
        {
            Transform camera = Camera.main!.transform;
            Vector3 dir = this.transform.position - camera.position;
            this.transform.rotation = Quaternion.LookRotation(dir, camera.up);
        }
    }
}