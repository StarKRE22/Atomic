using System;
using UnityEngine;

namespace AllIn13DShader
{
	public class DemoElement : MonoBehaviour
	{
		public MeshRenderer[] renderers;

		public DemoElementData demoElementData;

		public TransformScaler transformScaler;

		public PropertyTweenCollection tweenCollection;

		public int demoIndex { get; private set; }


		public virtual void Init(DemoSceneConfiguration demoSceneConfig, int demoIndex)
		{
			transform.localScale = Vector3.zero;
			gameObject.SetActive(false);

			transformScaler.Init(demoSceneConfig, ScaleFinishedCallback);

			tweenCollection.Init();

			this.demoIndex = demoIndex;
		}

		public virtual void Update()
		{
			tweenCollection.Update(Time.deltaTime);
		}

		public void Show()
		{
			gameObject.SetActive(true);
			transformScaler.ScaleUp();
		}

		public void Hide()
		{
			transformScaler.ScaleDown();
		}

		public void ScaleFinishedCallback(TransformScaler.ScalingType scalingOperation)
		{
			if(scalingOperation == TransformScaler.ScalingType.SCALING_DOWN)
			{
				gameObject.SetActive(false);
			}
		}

		public bool IsScaling()
		{
			bool res = transformScaler.IsScaling();
			return res;
		}
		
		// #if UNITY_EDITOR
		// private void OnValidate()
		// {
		// 	if(demoElementData == null)
		// 	{
		// 		string goName = gameObject.name;
		// 		if(goName.StartsWith("P_Demo_")) goName = goName.Substring(7);
		// 		
		// 		string[] guids = UnityEditor.AssetDatabase.FindAssets("t:DemoElementData");
		// 		foreach(string guid in guids)
		// 		{
		// 			string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
		// 			DemoElementData data = UnityEditor.AssetDatabase.LoadAssetAtPath<DemoElementData>(path);
		// 			if(data != null && data.prefab != null && data.prefab.name.Contains(goName))
		// 			{
		// 				demoElementData = data;
		// 				UnityEditor.EditorUtility.SetDirty(this);
		// 				break;
		// 			}
		// 		}
		// 	}
		// }
		// #endif
	}
}