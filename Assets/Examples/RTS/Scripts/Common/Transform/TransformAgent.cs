using UnityEngine;

namespace RTSGame
{
    public sealed class TransformAgent : MonoBehaviour
    {
        private void Awake()
        {
            TransformJobManager manager = TransformJobManager.Instance;
            if (manager)
                manager.Register(this.transform);
        }

        private void OnDestroy()
        {
            TransformJobManager manager = TransformJobManager.Instance;
            if (manager)
                manager.Unregister(this.transform);
        }

        public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
        {
            TransformJobManager manager = TransformJobManager.Instance;
            if (manager)

                manager.RequestTransform(this.transform, position, rotation);
        }
    }
}