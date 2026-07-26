using UnityEngine;

namespace Game.UI
{
    public static class RaycastUseCase
    {
        private const string GROUND_TAG = "Ground";
        
        public static bool RaycastGround(this Camera camera, Vector2 screenPosition, out Vector3 point)
        {
            Ray ray = camera.ScreenPointToRay(screenPosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.CompareTag(GROUND_TAG))
            {
                point = hit.point;
                return true;
            }

            point = default;
            return false;
        }

        public static bool RaycastTarget<T>(this Camera camera, Vector2 screenPosition, out T target)
        {
            Ray ray = camera.ScreenPointToRay(screenPosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.TryGetComponent(out target))
                return true;

            target = default;
            return false;
        }

        public static Collider[] ScanTargets(Vector3 position, float radius)
        {
            return Physics.OverlapSphere(position, radius, Physics.AllLayers, QueryTriggerInteraction.UseGlobal);
        }
    }
}