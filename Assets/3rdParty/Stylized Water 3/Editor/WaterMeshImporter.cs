// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

using UnityEditor.AssetImporters;
using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;

namespace StylizedWater3
{
    [ScriptedImporter(3, FILE_EXTENSION, -1)]
    public class WaterMeshImporter : ScriptedImporter
    {
        private const string FILE_EXTENSION = "watermesh";
        
        [SerializeField] public WaterMesh waterMesh = new WaterMesh();

        public override void OnImportAsset(AssetImportContext context)
        {
            waterMesh.Rebuild();

            context.AddObjectToAsset("mesh", waterMesh.mesh);
            context.SetMainObject(waterMesh.mesh);
        }
        
        //Handles correct behaviour when double-clicking a .watermesh asset assigned to a field
        //Otherwise the OS prompts to open it
        [UnityEditor.Callbacks.OnOpenAsset]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            Object target = EditorUtility.InstanceIDToObject(instanceID);

            if (target is Mesh)
            {
                var path = AssetDatabase.GetAssetPath(instanceID);
                
                if (Path.GetExtension(path) != "." + FILE_EXTENSION) return false;

                Selection.activeObject = target;
                return true;
            }
            
            return false;
        }

    }
}
