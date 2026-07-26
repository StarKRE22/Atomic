// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace StylizedWater3
{
    public static class RenderTargetDebugger
    {
        public class RenderTarget
        {
            public string name;
            public int order = 1000;
            
            public string description = string.Empty;

            public string textureName;
            public int propertyID;
        }
        
        public static List<RenderTarget> renderTargets = new List<RenderTarget>();
        //For dropdown menus
        public static string[] renderTargetNames;

        public static int InspectedProperty = -1;
        public static RTHandle CurrentRT;

        public static void Initialize()
        {
            renderTargets.Clear();
        
            var assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    if (type.IsAbstract || type.IsInterface) continue;

                    if (type.IsSubclassOf(typeof(RenderTarget)))
                    {
                        //Debug.Log($"Found {type}");

                        RenderTarget rt = Activator.CreateInstance(type) as RenderTarget;
                        
                        renderTargets.Add(rt);
                    }
                }
            }

            renderTargets = renderTargets.OrderBy(o => o.order).ToList();

            renderTargetNames = new string[renderTargets.Count];
            for (int i = 0; i < renderTargetNames.Length; i++)
            {
                renderTargetNames[i] = renderTargets[i].name;
            }
        }

        public static void Cleanup()
        {
            InspectedProperty = -1;
            CurrentRT?.Release();
        }
    }
}