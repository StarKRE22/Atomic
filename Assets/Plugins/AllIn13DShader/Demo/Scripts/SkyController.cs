using UnityEngine;

namespace AllIn13DShader
{
    public class SkyController : MonoBehaviour
    {
        [SerializeField] private Material[] skyMaterials;
        [SerializeField] private int firstSkyIndex = 0;
        [SerializeField] private InputHandler skyChangeInputHandler;

        private int currentSkyIndex;

        public void Init()
        {
            currentSkyIndex = firstSkyIndex;
            
            RenderSettings.defaultReflectionMode = UnityEngine.Rendering.DefaultReflectionMode.Skybox;
            RenderSettings.skybox = skyMaterials[currentSkyIndex];
        }

        public void NextSky()
        {
            currentSkyIndex++;
            if(currentSkyIndex >= skyMaterials.Length)
            {
                currentSkyIndex = 0;
            }

            // Directly set the skybox to the new material
            RenderSettings.skybox = skyMaterials[currentSkyIndex];
            DynamicGI.UpdateEnvironment();
        }

        public void DemoChanged(DemoElementData demoElementData)
        {
            if(demoElementData.skyboxEnabled)
            {
                RenderSettings.skybox = skyMaterials[currentSkyIndex];
            }
            else
            {
                RenderSettings.skybox = null;
            }

            DynamicGI.UpdateEnvironment();
        }
    }
}