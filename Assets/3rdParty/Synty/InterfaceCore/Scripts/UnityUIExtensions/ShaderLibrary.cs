/// Credit SimonDarksideJ
/// Updated by Synty: Renamed Unity.UI.Extensions to Synty.Interface.Extensions to avoid conflicts with Unity Ui Extensions and to pass Unity asset store submission.
using System.Collections.Generic;
using UnityEngine;

namespace Synty.Interface.Extensions
{
    public static class ShaderLibrary
    {
        public static Dictionary<string, Shader> shaderInstances = new Dictionary<string, Shader>();
        public static Shader[] preLoadedShaders;

        public static Shader GetShaderInstance(string shaderName)
        {
            if (shaderInstances.ContainsKey(shaderName))
            {
                return shaderInstances[shaderName];
            }

            var newInstance = Shader.Find(shaderName);
            if (newInstance != null)
            {
                shaderInstances.Add(shaderName, newInstance);
            }
            return newInstance;
        }
    }
}
