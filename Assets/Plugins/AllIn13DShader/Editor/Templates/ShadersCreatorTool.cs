using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace AllIn13DShader
{
	public static class ShadersCreatorTool
	{
		private const string TEMPLATE_EXTENSION = ".allIn13DTemplate";

		private static string[] TEMPLATES_NAMES = new string[] 
		{
			"AllIn13DShaderOutline_NoShadowCaster_Template",
			"AllIn13DShaderOutline_Template",
			"AllIn13DShader_NoShadowCaster_Template"
		};

		private static string[] TEMPLATE_TAGS = new string[]
		{
			"COMMON_PROPERTIES",
			"BASE_PASS",
			"FORWARD_ADD_PASS",
			"SHADOW_CASTER_PASS",
			"CUSTOM_EDITOR",
			"BASE_PASS_URP",
			"SHADOW_CASTER_PASS_URP",
			"DEPTH_NORMALS_PASS_URP",
		};

		private const string REGEX_CORE = @"\/\*<{0}_START>\*\/\s*([\t\r\ ]*(?:.*\n)*)[\t\r\ ]*\s+\/\*<{0}_END>\*\/";
		private const string TAG_FORMAT = "<{0}>";

		[MenuItem("Tools/Create Shader Files")]
		public static void BuildShaderFiles()
		{
			Shader mainShader = Shader.Find(Constants.SHADER_FULL_NAME_ALLIN13D);
			string mainShaderPath = AssetDatabase.GetAssetPath(mainShader);

			string shaderFileText = File.ReadAllText(mainShaderPath);

			for(int i = 0; i < TEMPLATES_NAMES.Length; i++)
			{
				string templatePath = Path.Combine(Constants.TEMPLATES_FOLDER, TEMPLATES_NAMES[i]) + TEMPLATE_EXTENSION;
				
				string templateText = File.ReadAllText(templatePath);
				string newShaderFileText = SearchAndReplaceTemplateTags(shaderFileText, templateText);

				string newShaderFileName = TEMPLATES_NAMES[i].Replace("_Template", "");
				string newShaderPath = Path.Combine(Constants.SHADERS_FOLDER_PATH, newShaderFileName + ".shader");
				File.WriteAllText(newShaderPath, newShaderFileText);
			}

			AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
		}

		private static string SearchAndReplaceTemplateTags(string shaderFileText, string templateText)
		{
			string res = templateText;

			for (int i = 0; i < TEMPLATE_TAGS.Length; i++)
			{
				string tagData = GetDataByTag(shaderFileText, TEMPLATE_TAGS[i]);
				res = OverrideTagWithData(TEMPLATE_TAGS[i], tagData, res);
			}

			return res;
		}

		private static string GetDataByTag(string shaderFileTex, string tag)
		{
			string regex = string.Format(REGEX_CORE, tag);
			MatchCollection matchCollection = Regex.Matches(shaderFileTex, regex);
			string res = matchCollection[0].Groups[1].Value.TrimStart().TrimEnd();

			return res;
		}

		private static string OverrideTagWithData(string tag, string data, string targetFileTxt)
		{
			string res = targetFileTxt;

			string formattedTag = string.Format(TAG_FORMAT, tag);
			res = res.Replace(formattedTag, data);

			return res;
		}
	}
}