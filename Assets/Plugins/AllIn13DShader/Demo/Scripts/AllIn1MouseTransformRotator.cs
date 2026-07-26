using UnityEngine;
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
using UnityEngine.InputSystem;
#endif

namespace AllIn13DShader
{
    public class AllIn1MouseTransformRotator : MonoBehaviour
    {
        [SerializeField] private float rotationSpeedHorizontal = 10f;
        [SerializeField] private float verticalSpeed = 5f;
        [SerializeField] private bool rightClickRequired = true;
        
        [Header("Movement Constraints")]
        [SerializeField] private float maxVerticalAngle = 80f;
        [SerializeField] private float minVerticalAngle = -60f;

        [Space, Header("Zoom")]
        [SerializeField] private bool enableZoom = true;
        [SerializeField] private float zoomSpeed = 5f;
        [SerializeField] private float maxZoomDistance = 20f;
        [SerializeField] private float minZoomDistance = 2f;

        private Transform zoomTargetTransform;
        private float currentVerticalRotation = 0f;
        private float currentHorizontalRotation = 0f;
        private float currentZoomDistance = 10f;
        private float dt;

#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
        private Mouse mouse;
        
        private void Awake()
        {
            mouse = Mouse.current;
            Initialize();
        }
#else
        private void Awake()
        {
            Initialize();
        }
#endif

        private void Initialize()
        {
            Vector3 currentRotation = transform.eulerAngles;
            currentHorizontalRotation = currentRotation.y;
            currentVerticalRotation = currentRotation.x;
            zoomTargetTransform = transform.GetChild(0);
            currentZoomDistance = zoomTargetTransform.localPosition.z;
        }

        private void Update()
        {
            dt = Time.deltaTime;
            HandleRotation();
            if(enableZoom) HandleZoom();
        }
    
        private void HandleRotation()
        {
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
            if(mouse == null) return;
            float mouseX = mouse.delta.ReadValue().x * dt * rotationSpeedHorizontal;
            float mouseY = mouse.delta.ReadValue().y * dt * verticalSpeed;
            bool isRightMousePressed = mouse.rightButton.isPressed;
#else
            float mouseX = Input.GetAxis("Mouse X") * dt * rotationSpeedHorizontal;
            float mouseY = Input.GetAxis("Mouse Y") * dt * verticalSpeed;
            bool isRightMousePressed = Input.GetMouseButton(1);
#endif
            if((!rightClickRequired && !isRightMousePressed) || (rightClickRequired && isRightMousePressed))
            {
                // Update rotations
                currentHorizontalRotation += mouseX;
                currentVerticalRotation += mouseY;
                
                // Clamp vertical rotation
                currentVerticalRotation = Mathf.Clamp(currentVerticalRotation, minVerticalAngle, maxVerticalAngle);
                
                // Apply rotations
                transform.rotation = Quaternion.Euler(currentVerticalRotation, currentHorizontalRotation, 0f);
            }
        }

        private void HandleZoom()
        {
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
            if(mouse == null) return;
            float scrollDelta = mouse.scroll.ReadValue().y * dt * zoomSpeed;
#else
            float scrollDelta = Input.GetAxis("Mouse ScrollWheel") * dt * zoomSpeed * 100f;
#endif
            // Update zoom distance
            currentZoomDistance = Mathf.Clamp(currentZoomDistance + scrollDelta, minZoomDistance, maxZoomDistance);
            
            // Apply new position
            Vector3 localPos = zoomTargetTransform.localPosition;
            localPos.z = currentZoomDistance;
            zoomTargetTransform.localPosition = localPos;
        }
    }
}