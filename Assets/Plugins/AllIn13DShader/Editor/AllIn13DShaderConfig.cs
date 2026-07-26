using UnityEngine;

namespace AllIn13DShader
{
	public static class AllIn13DShaderConfig
	{
		//Default Material Name
		public const string MATERIAL_NAME_DEFAULT = "AllIn13DMaterial.mat";

		public static Texture GetInspectorImage()
		{
			Texture res = null;

			res = EditorUtils.FindAsset<Texture>("AllIn13dShaderCustomEditorHeader");

			return res;
		}
	}
}