using UnityEngine;

namespace RTSGame.Debugging
{
    public class CameraController : MonoBehaviour
    {
        [Header("Input")]
        [SerializeField] private bool useKeyboard = true;
        [SerializeField] private bool useMouse = false;

        [Header("Movement Settings")]
        public float panSpeed = 20f;
        public float panBorderThickness = 10f;

        [Header("Zoom Settings")]
        public float scrollSpeed = 20f;
        public float minY = 10f;
        public float maxY = 80f;

        [Header("Boundary Limits")]
        public float minX = -50f;
        public float maxX = 50f;
        public float minZ = -50f;
        public float maxZ = 50f;

        void Update()
        {
            Vector3 pos = transform.position;

            if (useKeyboard)
            {
                if (Input.GetKey(KeyCode.W))
                    pos.z += panSpeed * Time.deltaTime;

                if (Input.GetKey(KeyCode.S))
                    pos.z -= panSpeed * Time.deltaTime;

                if (Input.GetKey(KeyCode.D))
                    pos.x += panSpeed * Time.deltaTime;

                if (Input.GetKey(KeyCode.A))
                    pos.x -= panSpeed * Time.deltaTime;
            }

            if (useMouse)
            {
                if (Input.mousePosition.y >= Screen.height - panBorderThickness)
                    pos.z += panSpeed * Time.deltaTime;

                if (Input.mousePosition.y <= panBorderThickness)
                    pos.z -= panSpeed * Time.deltaTime;

                if (Input.mousePosition.x >= Screen.width - panBorderThickness)
                    pos.x += panSpeed * Time.deltaTime;

                if (Input.mousePosition.x <= panBorderThickness)
                    pos.x -= panSpeed * Time.deltaTime;
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;

            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);
            pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

            transform.position = pos;
        }
    }
}