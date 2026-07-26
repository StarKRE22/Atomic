namespace AllIn13DShader
{
	public static class ConstantsRuntime
	{
		//Special Properties
		public const string GLOBAL_PROPERTY_GLOBAL_TIME = "allIn13DShader_globalTime";

#if UNITY_EDITOR
		//Standard Shader
#if ALLIN13DSHADER_BIRP
		public const string STANDARD_SHADER_NAME = "Standard";
#elif ALLIN13DSHADER_URP
		public const string STANDARD_SHADER_NAME = "Universal Render Pipeline/Lit";
#else
		public const string STANDARD_SHADER_NAME = "Standard";
#endif

#endif 
	}
}