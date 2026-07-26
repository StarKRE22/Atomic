using System.IO;

namespace AllIn13DShader
{
	public static class Constants
	{
		public const string EFFECT_ATTRIBUTE_PREFIX = "Effect(";
		public const string EFFECT_PROPERTY_ATTRIBUTE_PREFIX = "EffectProperty(";
		public const string HEADER_PREFIX = "Header(";
		public const string SINGLE_PROPERTY_ATTRIBUTE = "SingleProperty";
		public const string ADVANCED_PROPERTY_ATTRIBUTE = "AdvancedProperty";


		public const string SHADER_ROOT = "AllIn13DShader";

		public static string[] SHADERS_NAMES = new string[]
		{
			"AllIn13DShader",
			"AllIn13DShader_NoShadowCaster",
			"AllIn13DShaderOutline_NoShadowCaster",
			"AllIn13DShaderOutline",
		};

		public static string MAIN_SHADER_NAME
		{
			get
			{
				return SHADERS_NAMES[0];
			}
		}

		public static string SHADER_FULL_NAME_ALLIN13D
		{
			get
			{
				return SHADER_ROOT + "/" + MAIN_SHADER_NAME;
			}
		}

		public static string SHADER_FULL_NAME_ALLIN13D_OUTLINE
		{
			get
			{
				return SHADER_ROOT + "/" + SHADERS_NAMES[1];
			}
		}

		//Version
		public static string VERSION = "1.0";

		//Paths
		public static string SHADERS_FOLDER_PATH = Path.Combine(GlobalConfiguration.instance.RootPluginPath, "Shaders");/*"Assets/AllIn13DShader/Shaders";*/
		public static string SHADERS_PROPERTIES_FOLDER_PATH = /*"Assets/AllIn13DShader/Editor"*/Path.Combine(GlobalConfiguration.instance.RootPluginPath, "Editor");
		public static string TEMPLATES_FOLDER = Path.Combine(SHADERS_PROPERTIES_FOLDER_PATH, "Templates");

		//
		public const string KEYWORD_NONE = "NONE";

		//Special Properties
		public const string MATPROPERTY_RENDERING_MODE = "_RenderPreset";
		public const string MATPROPERTY_BLEND_SRC = "_BlendSrc";
		public const string MATPROPERTY_BLEND_DST = "_BlendDst";

		//Drawers IDs
		public const string GENERAL_EFFECT_DRAWER_ID = "GENERAL_EFFECT_DRAWER";
		public const string TRIPLANAR_EFFECT_DRAWER_ID = "TRIPLANAR_EFFECT_DRAWER";
		public const string COLOR_RAMP_EFFECT_DRAWER_ID = "COLOR_RAMP_EFFECT_DRAWER";
		public const string OUTLINE_DRAWER_ID = "OUTLINE_DRAWER_ID";
		public const string TEXTURE_BLENDING_EFFECT_DRAWER_ID = "TEXTURE_BLENDING_EFFECT_DRAWER";

		//Regex
		public const string REGEX_EFFECT = @"\(EffectID#([\w\s]+),.*GroupID#([\w\s]+)(?:,.*AllowReset#([\w\s]+))?(?:,.*DependentOn#([\w\s]+))?(?:,.*IncompatibleWith#([\w\s]+))?(?:,.*Doc#([\w\s\\\.]+))?(?:,.*CustomDrawer#([\w\s]+))?\)";
		public const string REGEX_EFFECT_PROPERTY = @"EffectProperty\((.*)\)";
		//public const string REGEX_EFFECT_PROOPERTY_COMPLETE = @"EffectProperty\(ParentEffect# ([\w\s]+), Keywords\((.*)\)\)";
		public const string REGEX_EFFECT_PROOPERTY_COMPLETE = @"EffectProperty\(ParentEffect# ([\w\s]+)(?:,.*KeywordsOp# ([\w]+))?(?:,.*IncompatibleWithKws\(([\w]+)\))?, Keywords\((.*)\), AllowReset# ([\w]+)\)";
		public const string REGEX_PARENT_EFFECT_KEYWORDS = @".*\((.*)\)";
		public const string REGEX_KEYWORDS_ENUM = @"KeywordEnum\((.*)\)";

		//Editor Prefs Keys
		public const string LAST_TIME_SHADER_PROPERTIES_REBUILT_KEY = "AllIn13DShader_RebuiltTime";
		public const string LAST_RENDER_PIPELINE_CHECKED_KEY = "AllIn13DShader_LastRenderPipeline";
	}
}