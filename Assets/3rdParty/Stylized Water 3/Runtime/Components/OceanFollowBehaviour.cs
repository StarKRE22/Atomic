// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace StylizedWater3
{
    [ExecuteAlways]
    [AddComponentMenu("")] //Hide, only to be used with the prefab
    public class OceanFollowBehaviour : MonoBehaviour
    {
        const int LODCount = 7;
        private static readonly float[] gridSizes = new[]
        {
            0.5f,    //LOD0
            01f,     //LOD1
            02f,     //LOD2
            04f,     //LOD3
            08f,     //LOD4
            16f,     //LOD5
            32f,     //LOD6
            32f      //LOD7
        };

        private const float EXPECTED_WAVE_HEIGHT = 15f;
        
        public Material material;
        
        [Space]
        
        [Tooltip("Enable to executing the camera following behaviour when outside of Play mode")]
        public bool enableInEditMode = true;
        [Tooltip("Assign a specific transform to follow on the XZ axis." +
                 "\n\nIf left empty, the camera currently rendering will be targeted.")]
        public Transform followTarget;

        [Space]
        
        public static bool ShowWireFrame = true;

        [Serializable]
        public class LOD
        {
            public List<GameObject> gameObjects = new List<GameObject>();
            public float gridSize = 1f;
        }
        [SerializeField] 
        [HideInInspector]
        private LOD[] lods;
        
        //To ensure that each LOD renders behind the one before it, move each one down a tiny bit
        private const float heightOffset = 0.01f;

        private void Reset()
        {
            //BuildLODs();
        }
        
        #if SWS_DEV
        [ContextMenu("Build LODs")]
        #endif
        //Populates the LOD list and sets the gridsize for each
        void BuildLODs()
        {
            lods = new LOD[LODCount+1];
            for (int i = 0; i < lods.Length; i++)
            {
                lods[i] = new LOD();
                lods[i].gridSize = gridSizes[i];
            }

            MeshRenderer[] childs = this.gameObject.GetComponentsInChildren<MeshRenderer>(true);
            for (int i = 0; i < childs.Length; i++)
            {
                childs[i].gameObject.layer = WaterObject.WaterLayer;
                
                string objName = childs[i].name;
                int lodIndex = 0;
                
                if(objName.EndsWith("0")) lodIndex = 0;
                if(objName.EndsWith("1")) lodIndex = 1;
                if(objName.EndsWith("2")) lodIndex = 2;
                if(objName.EndsWith("3")) lodIndex = 3;
                if(objName.EndsWith("4")) lodIndex = 4;
                if(objName.EndsWith("5")) lodIndex = 5;
                if(objName.EndsWith("6")) lodIndex = 6;
                if(objName.EndsWith("7")) lodIndex = 7;
                
                #if UNITY_EDITOR
                childs[i].scaleInLightmap = Mathf.Lerp(1, 0.01f, (float)lodIndex / LODCount);
                #endif

                lods[lodIndex].gameObjects.Add(childs[i].gameObject);
            }
        }

        private Vector3 targetPosition;
        private void SetPosition(Transform target)
        {
            float height = this.transform.position.y;
            
            for (int i = 0; i < lods.Length; i++)
            {
                for (int j = 0; j < lods[i].gameObjects.Count; j++)
                {
                    targetPosition = WaterGrid.SnapToGrid(target.position, this.transform.lossyScale.x * lods[i].gridSize);
                    //targetPosition = target.position;
                    
                    //Allow the transform to be moved on the Y-axis freely
                    targetPosition.y = height - (heightOffset * i);
                    
                    lods[i].gameObjects[j].transform.position = targetPosition;
                }
            }
        }

        private void OnValidate()
        {
            if (material == null) return;

            ApplyMaterial(material);
        }

        public void ApplyMaterial(Material mat)
        {
            for (int i = 0; i < lods.Length; i++)
            {
                foreach (var lod in lods[i].gameObjects)
                {
                    MeshRenderer r = lod.GetComponent<MeshRenderer>();

                    if (r)
                    {
                        r.sharedMaterial = mat;

                        //Pad the bounds so that meshes don't get unintentionally culled when using high waves
                        r.localBounds = new Bounds(r.localBounds.center, new Vector3(r.localBounds.size.x, EXPECTED_WAVE_HEIGHT, r.localBounds.size.z));
                    }
                }
            }
        }

        private void OnEnable()
        {
            #if URP
            RenderPipelineManager.beginCameraRendering += OnCameraRender;
            #endif
        }

        private void LateUpdate()
        {
            if (followTarget != null)
            {
                SetPosition(followTarget);
            }
        }
        
        #if URP
        private void OnCameraRender(ScriptableRenderContext context, Camera targetCamera)
        {
            if (targetCamera.cameraType == CameraType.Preview) return;
            
            //Component set up to follow a specific target
            if (followTarget != null) return;
            
            #if UNITY_EDITOR
            //Skip if disabled in scene-view
            if (targetCamera.cameraType == CameraType.SceneView && (enableInEditMode == false || Application.isPlaying)) return;
            #endif
            
            SetPosition(targetCamera.transform);
        }
        #endif

        private void OnDisable()
        {
            #if URP
            RenderPipelineManager.beginCameraRendering -= OnCameraRender;
            #endif
        }

        private void OnDrawGizmosSelected()
        {
            if (!ShowWireFrame) return;

            MeshFilter[] meshes = GetComponentsInChildren<MeshFilter>();
            
            Gizmos.color = new Color(0,0,0,0.25f);
            for (int i = 0; i < meshes.Length; i++)
            {
                Gizmos.matrix = meshes[i].transform.localToWorldMatrix;
                Gizmos.DrawWireMesh(meshes[i].sharedMesh);
            }
        }
    }
}