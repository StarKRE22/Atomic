// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

using System;
using UnityEngine;

namespace StylizedWater3
{
    /// <summary>
    /// Utility for creating an orthographic top-down projection
    /// </summary>
    public class PlanarProjection
    {
        private static readonly Quaternion viewRotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
        private static readonly Vector3 viewScale = new Vector3(1, 1, -1);
        private static readonly Plane[] frustrumPlanes = new Plane[6];

        //Input
        public Vector3 center;
        public Vector3 offset;
        
        public float scale;
        public int resolution;
        public bool expandHeight = true;
        
        //Output
        public Matrix4x4 projection;
        public Matrix4x4 view;
        public Rect viewportRect;
        public Vector3 boundsMin;
        public Vector3 boundsMax;
        
        //Important to snap the projection to the nearest texel. Otherwise pixel swimming is introduced when moving, due to bilinear filtering
        private static Vector3 Stabilize(Vector3 pos, float texelSize)
        {
            float Snap(float coord, float cellSize) => Mathf.FloorToInt(coord / cellSize) * (cellSize) + (cellSize * 0.5f);

            return new Vector3(Snap(pos.x, texelSize), Snap(pos.y, texelSize), Snap(pos.z, texelSize));
        }
        
        public void Recalculate()
        {
            float extent = scale * 0.5f;
            
            Vector3 centerPosition = center + offset;
            
            //var frustumHeight = 2.0f * scale * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad); //Still clips, plus doesn't support orthographc
            var frustumHeight = expandHeight ? 10000f : scale;
            centerPosition += (Vector3.up * frustumHeight * 0.5f);
            
            centerPosition = Stabilize(centerPosition, scale / resolution);

            projection = Matrix4x4.Ortho(-extent, extent, -extent, extent, 0.02f, frustumHeight);

            view = Matrix4x4.TRS(centerPosition, viewRotation, viewScale).inverse;
            
            viewportRect = new Rect(0, 0, resolution, resolution);

            boundsMin.x = centerPosition.x - extent;
            boundsMin.y = centerPosition.y - extent;
            boundsMin.z = centerPosition.z - extent;
            
            boundsMax.x = centerPosition.z + extent;
            boundsMax.y = centerPosition.z + extent;
            boundsMax.z = centerPosition.z + extent;
        }

        /// <summary>
        /// Sets a Vector4 uniform up to be passed onto shader land, where a world-space position can be used to calculate a sampling UV
        /// </summary>
        /// <param name="coords"></param>
        public void SetUV(ref Vector4 coords)
        {
            coords.x = boundsMin.x;
            coords.y = boundsMin.z;
            coords.z = scale;
            coords.w = 1; //Enable sampling shaders
        }
        
        public static int CalculateResolution(float scale, int texelsPerUnit, int min, int max)
        {
            int res = Mathf.RoundToInt(scale * texelsPerUnit);
            //if(NON_POWER_OF_TWO == false) res = Mathf.NextPowerOfTwo(res);
            
            return Mathf.Clamp(res, min, max);
        }

        public static float FadePercentageToLength(float renderRange, float fadePercentage)
        {
            fadePercentage = Mathf.Max(0.01f, fadePercentage);
            
            return (renderRange * 0.5f) * (fadePercentage / 100f);
        }

        public void CalculateFrustumPlanes()
        {
            GeometryUtility.CalculateFrustumPlanes(projection * view, frustrumPlanes);
        }

        public bool TestPlanesAABB(Bounds bounds)
        {
            return GeometryUtility.TestPlanesAABB(frustrumPlanes, bounds);
        }

        //Using data only from the matrices, to ensure what you're seeing closely represents them
        public void DrawOrthographicViewGizmo()
        {
            Gizmos.matrix = Matrix4x4.identity;
            
            CalculateFrustumPlanes();
            
            float near = frustrumPlanes[4].distance;
            float far = frustrumPlanes[5].distance;
            float height = near + far;

            Vector3 position = new Vector3(view.inverse.m03, view.inverse.m13, view.inverse.m23);
            Vector3 orthoSize = new Vector3((frustrumPlanes[0].distance + frustrumPlanes[1].distance), height, frustrumPlanes[2].distance + frustrumPlanes[3].distance);

            //orthoSize = Vector3.one * 50f;
            Gizmos.DrawSphere(position, 1f);

            position -= Vector3.up * height * 0.5f;
            Gizmos.DrawWireCube(position, orthoSize);
            Gizmos.color = Color.white * 0.25f;
            Gizmos.DrawCube(position, orthoSize);
        }
    }
}