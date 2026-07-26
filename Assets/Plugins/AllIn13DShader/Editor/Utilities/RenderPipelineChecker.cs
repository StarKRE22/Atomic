using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Build;

#if UNITY_2019_3_OR_NEWER

namespace AllIn13DShader
{
	public class RenderPipelineChecker
	{
		public enum RenderPipeline
		{
			NONE = 0,
			BIRP = 1,
			URP = 2,
			HDRP = 3
		}

		private const string SYMBOL_URP = "ALLIN13DSHADER_URP";
		private const string SYMBOL_HDRP = "ALLIN13DSHADER_HDRP";
		private const string SYMBOL_BIRP = "ALLIN13DSHADER_BIRP";

		private const string HDRP_PACKAGE = "HDRenderPipelineAsset";
		private const string URP_PACKAGE = "UniversalRenderPipelineAsset";

		public static bool IsHDRP
		{
			get; private set;
		}
		public static bool IsURP
		{
			get; private set;
		}
		public static bool IsStandardRP
		{
			get; private set;
		}

		public static RenderPipeline CurrentRenderPipeline
		{
			get; private set;
		}

		public static void RefreshData()
		{
			IsHDRP = DoesTypeExist(HDRP_PACKAGE);
			IsURP = DoesTypeExist(URP_PACKAGE);

			if (!(IsHDRP || IsURP))
			{
				IsStandardRP = true;
			}

			else if (IsURP)
			{
				CurrentRenderPipeline = RenderPipeline.URP;
			}
			else if (IsHDRP)
			{
				CurrentRenderPipeline = RenderPipeline.HDRP;
			}
			else
			{
				CurrentRenderPipeline = RenderPipeline.BIRP;
			}
		}

		public static bool DoesTypeExist(string className)
		{
			var foundType = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
				from type in GetTypesSafe(assembly)
				where type.Name == className
				select type).FirstOrDefault();

			return foundType != null;
		}

		public static IEnumerable<Type> GetTypesSafe(System.Reflection.Assembly assembly)
		{
			Type[] types;

			try
			{
				types = assembly.GetTypes();
			}
			catch (ReflectionTypeLoadException e)
			{
				types = e.Types;
			}

			return types.Where(x => x != null);
		}

		public static bool IsRenderPipelineDefined()
		{
			bool res = false;

			BuildTarget buildTarget = EditorUserBuildSettings.activeBuildTarget;
			BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;

			NamedBuildTarget namedBuildTarget = NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup);
			string[] defineSymbols = new string[0];
			PlayerSettings.GetScriptingDefineSymbols(namedBuildTarget, out defineSymbols);

			for (int i = defineSymbols.Length - 1; i >= 0; i--)
			{
				if (defineSymbols[i] == SYMBOL_URP || defineSymbols[i] == SYMBOL_HDRP || defineSymbols[i] == SYMBOL_BIRP)
				{
					res = true;
				}
			}

			return res;
		}

		private static string[] GetSymbolsWithoutPipeline()
		{
			BuildTarget buildTarget = EditorUserBuildSettings.activeBuildTarget;
			BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;

			NamedBuildTarget namedBuildTarget = NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup);
			string[] defineSymbols = new string[0];
			PlayerSettings.GetScriptingDefineSymbols(namedBuildTarget, out defineSymbols);

			for (int i = defineSymbols.Length - 1; i >= 0; i--)
			{
				if (defineSymbols[i] == SYMBOL_URP || defineSymbols[i] == SYMBOL_HDRP || defineSymbols[i] == SYMBOL_BIRP)
				{
					ArrayUtility.RemoveAt(ref defineSymbols, i);
				}
			}

			return defineSymbols;
		}

		public static void RemovePipelineSymbols()
		{
			string[] defineSymbols = GetSymbolsWithoutPipeline();
			ApplyDefineSymbols(defineSymbols);
		}

		private static void ApplyDefineSymbols(string[] defineSymbols)
		{
			BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
			NamedBuildTarget namedBuildTarget = NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup);

			PlayerSettings.SetScriptingDefineSymbols(namedBuildTarget, defineSymbols);
		}

		public static void CheckRenderPipeline()
		{
			RefreshData();

			RenderPipeline lastRenderPipeline = (RenderPipeline)EditorPrefs.GetInt(Constants.LAST_RENDER_PIPELINE_CHECKED_KEY, 0);

			if (lastRenderPipeline != CurrentRenderPipeline || !IsRenderPipelineDefined())
			{
				string[] defineSymbols = GetSymbolsWithoutPipeline();
				if (IsURP)
				{
					ArrayUtility.Add(ref defineSymbols, SYMBOL_URP); 
				}
				else if (IsHDRP)
				{
					ArrayUtility.Add(ref defineSymbols, SYMBOL_HDRP);
				}
				else
				{
					ArrayUtility.Add(ref defineSymbols, SYMBOL_BIRP);
				}

				ApplyDefineSymbols(defineSymbols);
				EditorPrefs.SetInt(Constants.LAST_RENDER_PIPELINE_CHECKED_KEY, (int)CurrentRenderPipeline);
			}
		}

		[InitializeOnLoadMethod]
		public static void ScriptsReloaded()
		{
			CheckRenderPipeline();
		}
	}
}

#endif