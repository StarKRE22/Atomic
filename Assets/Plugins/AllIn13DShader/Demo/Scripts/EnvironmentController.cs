#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

using UnityEngine;

namespace AllIn13DShader
{
	public class EnvironmentController : MonoBehaviour
	{
		public DemoElement linkedDemoElement;
		public TransformScaler transformScaler;
		public AlphaTween alphaTween;

		public MeshRenderer[] renderers;

		public void Init(DemoSceneConfiguration demoSceneConfig)
		{
			transformScaler.Init(demoSceneConfig, ScaleFinishedCallback);
			alphaTween.Init(demoSceneConfig, renderers);
		}

		private void ScaleFinishedCallback(TransformScaler.ScalingType scalingOperation)
		{
			if(scalingOperation == TransformScaler.ScalingType.SCALING_DOWN)
			{
				Destroy(gameObject);
			}
		}
		public void Show(DemoElement linkedDemoElement)
		{
			gameObject.SetActive(true);
			this.linkedDemoElement = linkedDemoElement;

			transformScaler.ScaleUp();
			alphaTween.FadeIn();
		}

		public void Hide()
		{
			for(int i = 0; i < renderers.Length; i++)
			{
				Material mat = renderers[i].material;
				DemoUtils.SetMaterialTransparent(mat);
			}

			transformScaler.ScaleDown();
			alphaTween.FadeOut();
		}

		protected virtual void Update()
		{

		}

#if UNITY_EDITOR
		[ContextMenu("Setup Renderers")]
		public void SetupRenderers()
		{

			PrefabStage prefabStage = PrefabStageUtility.GetPrefabStage(gameObject);

			if(prefabStage != null)
			{
				this.renderers = transform.GetComponentsInChildren<MeshRenderer>(true);
				EditorSceneManager.MarkSceneDirty(prefabStage.scene);
			}
		}
#endif
	}
}