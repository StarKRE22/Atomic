using UnityEngine;

namespace AllIn13DShader
{
	public class AllIn1MainLightController : MonoBehaviour
	{
		public Light mainDirectionalLight;
		
		public void DemoChanged(DemoElementData demoElementData)
		{
			mainDirectionalLight.enabled = demoElementData.directionalLightEnabled;
			mainDirectionalLight.intensity = demoElementData.mainLightIntensity;
		}
	}
}