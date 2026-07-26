using UnityEngine;

namespace AllIn13DShader
{
	[ExecuteInEditMode]
	public class AllIn13DShaderRandomTimeSeed : MonoBehaviour
	{
		[SerializeField] private float minSeedValue = 0;
		[SerializeField] private float maxSeedValue = 100f;

		private void Start()
		{
			RefreshTimingSeed();
		}

		[ContextMenu("Refresh Timing Seed")]
		private void RefreshTimingSeed()
		{
			MaterialPropertyBlock properties = new MaterialPropertyBlock();
			properties.SetFloat("_TimingSeed", Random.Range(minSeedValue, maxSeedValue));
			GetComponent<Renderer>().SetPropertyBlock(properties);
		}
	}
}