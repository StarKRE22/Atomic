using System;
using UnityEngine;

namespace AllIn13DShader
{
	public class DemoElementData : ScriptableObject
	{
		public string demoName;
		public GameObject prefab;
		public DemoEnvironment environment;

		[Header("Info")]
		[TextArea] public string info;

		[Header("Min Directional Light")]
		public bool directionalLightEnabled = true;
		public float mainLightIntensity = 1.0f;

		[Header("Skybox")]
		public bool skyboxEnabled = true;

		[Header("Postprocess")]
		public bool postProcessEnabled = false;

		// private void OnValidate()
		// {
		// 	if(string.IsNullOrEmpty(demoName)) demoName = GetFormattedName();
		// }
		//
		// private string GetFormattedName()
		// {
		// 	if(prefab == null) return string.Empty;
  //   
		// 	string prefabName = prefab.name;
		// 	if(prefabName.StartsWith("P_Demo_")) prefabName = prefabName.Substring(7);
  //   
		// 	if(prefabName.Length > 1)
		// 	{
		// 		int i = 1;
		// 		while(i < prefabName.Length)
		// 		{
		// 			if(char.IsUpper(prefabName[i]) && !char.IsWhiteSpace(prefabName[i-1]))
		// 			{
		// 				prefabName = prefabName.Insert(i, " ");
		// 				i++;
		// 			}
		// 			i++;
		// 		}
		// 	}
  //   
		// 	return prefabName;
		// }
	}
}