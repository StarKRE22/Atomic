using UnityEngine;

namespace AllIn13DShader
{
	public class DemoElementColorRamp : DemoElement
	{
		private static int COLOR_RAMP_MATPROP_ID = Shader.PropertyToID("_ColorRampTex"); 

		private float timer;
		private int currentTextureIndex;

		public Texture[] colorRamps;
		public float timeBetweenTextures;

		public Material mat;

		public override void Update()
		{
			base.Update();

			timer += Time.deltaTime;

			if(timer >= timeBetweenTextures)
			{
				timer = 0f;
				currentTextureIndex = currentTextureIndex < colorRamps.Length - 1 ? currentTextureIndex + 1 : 0;

				mat.SetTexture(COLOR_RAMP_MATPROP_ID, colorRamps[currentTextureIndex]);
			}
		}
	}
}