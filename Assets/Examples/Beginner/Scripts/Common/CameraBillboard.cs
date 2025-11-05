using UnityEngine;

namespace BeginnerGame
{
    /// <summary>
    /// Makes the GameObject always face the main camera.
    /// </summary>
    /// <remarks>
    /// This component should be attached to any object that needs to 
    /// always face the camera (e.g., floating health bars, name tags, icons).
    /// The rotation is updated in <see cref="LateUpdate"/> to ensure correct
    /// alignment after all camera movements.
    /// </remarks>
    public sealed class CameraBillboard : MonoBehaviour
    {
        /// <summary>
        /// Rotates the object to face the main camera each frame.
        /// </summary>
        /// <remarks>
        /// Uses <see cref="Camera.main"/> to determine the active camera.
        /// The object's forward vector is aligned with the direction 
        /// from the camera to the object.
        /// </remarks>
        private void LateUpdate()
        {
            Transform camera = Camera.main!.transform;
            Vector3 dir = this.transform.position - camera.position;
            this.transform.rotation = Quaternion.LookRotation(dir, camera.up);
        }
    }
}