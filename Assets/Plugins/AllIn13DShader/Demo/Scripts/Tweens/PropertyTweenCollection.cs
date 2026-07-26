using UnityEngine;

namespace AllIn13DShader
{
	[System.Serializable]
	public class PropertyTweenCollection
	{
		public Material mat;

		public PropertyTweenFader[] faderTweens;
		public PropertyTweenSinWave[] sinWaveTweens;

		public void Init()
		{
			if(mat == null) return;
			
			for (int i = 0; i < faderTweens.Length; i++)
			{
				faderTweens[i].Init(mat);
			}

			for (int i = 0; i < sinWaveTweens.Length; i++)
			{
				sinWaveTweens[i].Init(mat);
			}
		}

		public void Update(float deltaTime)
		{
			if(mat == null) return;
			
			for(int i = 0; i < faderTweens.Length; i++)
			{
				faderTweens[i].Update(deltaTime);
			}

			for (int i = 0; i < sinWaveTweens.Length; i++)
			{
				sinWaveTweens[i].Update(deltaTime);
			}
		}
	}
}