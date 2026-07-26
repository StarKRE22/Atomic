using UnityEngine;
using UnityEngine.Rendering;

namespace AllIn13DShader
{
	public class BlendingMode : ScriptableObject
	{
		public string displayName;

		public RenderQueue renderQueue;
		public BlendMode blendSrc;
		public BlendMode blendDst;
		public bool depthWrite;

		public string[] defaultEnabledEffects;
	}
}