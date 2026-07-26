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
    [AddComponentMenu("Stylized Water 3/Water Custom Time")]
    public class SetCustomWaterTime : MonoBehaviour
    {
        public enum Mode
        {
            None,
            Interval,
            Time,
            EditorTime,
            Custom
        }

        public Mode mode = Mode.Custom;

        [Min(0.02f)]
        public float interval = 0.2f;
        [Min(0f)]
        public float customTime = 0f;
        private float elapsedTime;

        private void OnEnable()
        {
            RenderPipelineManager.beginContextRendering += OnBeginFrame;
        }

        private void OnBeginFrame(ScriptableRenderContext context, List<Camera> cams)
        {
            SetTime();
        }

        private void SetTime()
        {
            if (mode == Mode.None)
            {
                ResetTime();
                return;
            }

            if (mode == Mode.Interval)
            {
                elapsedTime += Time.deltaTime;

                if (elapsedTime >= interval)
                {
                    elapsedTime = 0;

                    WaterObject.CustomTime = Time.time;
                }
            }

            if (mode == Mode.Time)
            {
                WaterObject.CustomTime = Time.time;
            }
			
			#if UNITY_EDITOR
            if (mode == Mode.EditorTime)
            {
                WaterObject.CustomTime = (float)UnityEditor.EditorApplication.timeSinceStartup;
            }
			#endif

            if (mode == Mode.Custom)
            {
                WaterObject.CustomTime = customTime;
            }
        }

        private void ResetTime()
        {
            //Revert to using normal time
            WaterObject.CustomTime = -1;
        }
        
        private void OnDisable()
        {
            RenderPipelineManager.beginContextRendering -= OnBeginFrame;
        }
    }
}