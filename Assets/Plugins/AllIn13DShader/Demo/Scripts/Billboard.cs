using UnityEditor;
using UnityEngine;

namespace AllIn13DShader
{
    /// <summary>
    /// Handles billboard behavior to face camera either on Y-axis only or all axes
    /// Can follow either editor scene view camera or main camera with cached reference
    /// </summary>
    [ExecuteAlways]
    public class Billboard : MonoBehaviour
    {
        [SerializeField] 
        private bool yAxisOnly = true;
    
        [SerializeField, Tooltip("If true, will update rotation every frame. If false, only on camera movement")]
        private bool continuousUpdate = true;

        [SerializeField, Tooltip("If true, follows editor scene view camera. If false, follows main camera")]
        private bool useEditorCamera = false;

        private Transform targetCameraTransform;
        private Vector3 previousCameraPosition;
        private Quaternion initialRotation;
        private bool wasUsingEditorCamera;
    
        // Static cached reference to main camera
        private static Camera mainCameraCache;
        private static bool mainCameraSearched = false;

        private void OnEnable()
        {
            // Reset camera search if re-enabled
            if (!useEditorCamera && !mainCameraSearched)
            {
                UpdateCameraReference();
            }
        }

        private void Update()
        {
            bool shouldUpdateCamera = targetCameraTransform == null || wasUsingEditorCamera != useEditorCamera;
        
            if(shouldUpdateCamera)
            {
                UpdateCameraReference();
            }

            if(targetCameraTransform == null) return;

            if(!continuousUpdate && previousCameraPosition == targetCameraTransform.position)
            {
                return;
            }

            if(yAxisOnly)
            {
                // Only rotate around Y axis
                Vector3 directionToCamera = targetCameraTransform.position - transform.position;
                directionToCamera.y = 0;

                if(directionToCamera != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(directionToCamera) * initialRotation;
                }
            }
            else
            {
                // Full billboard - face camera completely
                transform.LookAt(transform.position + targetCameraTransform.rotation * Vector3.forward,
                    targetCameraTransform.rotation * Vector3.up);
            }

            previousCameraPosition = targetCameraTransform.position;
        }

        private void UpdateCameraReference()
        {
#if UNITY_EDITOR
            if(useEditorCamera)
            {
                SceneView sceneView = SceneView.lastActiveSceneView;
                if(sceneView == null || sceneView.camera == null) return;
                
                targetCameraTransform = sceneView.camera.transform;
                wasUsingEditorCamera = true;
            }
            else
#endif
            {
                // Use cached camera reference if available
                if (mainCameraCache == null || !mainCameraCache.isActiveAndEnabled)
                {
                    mainCameraCache = Camera.main;
                    mainCameraSearched = true;
                }
            
                if(mainCameraCache == null) return;
                targetCameraTransform = mainCameraCache.transform;
                wasUsingEditorCamera = false;
            }

            initialRotation = Quaternion.identity;
        }

        // Handle scene changes or camera destruction
        private void OnValidate()
        {
            // Force update of camera reference when properties change in inspector
            targetCameraTransform = null;
        }
    }
}