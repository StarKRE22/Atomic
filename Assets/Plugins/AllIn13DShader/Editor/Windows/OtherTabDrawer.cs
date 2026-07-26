using UnityEditor;
using UnityEngine;

namespace AllIn13DShader
{
	public class OtherTabDrawer
	{
		private GlobalConfiguration globalConfiguration;
		private CommonStyles commonStyles;

		public void Setup(GlobalConfiguration globalConfiguration, CommonStyles commonStyles)
		{
			this.globalConfiguration = globalConfiguration;
			this.commonStyles = commonStyles;

			if (globalConfiguration.defaultPreset == null)
			{
				globalConfiguration.projectType = GlobalConfiguration.ProjectType.STANDARD_BASIC;
				globalConfiguration.RefreshDefaultMaterial();
				globalConfiguration.Save();
			}
		}

		public void Draw()
		{
			GUILayout.Label("Default Materials", commonStyles.bigLabel);
			GUILayout.Space(20);

			EditorGUI.BeginChangeCheck();
			globalConfiguration.projectType = (GlobalConfiguration.ProjectType)EditorGUILayout.EnumPopup("Project Type", globalConfiguration.projectType);
			bool projectTypeChanged = EditorGUI.EndChangeCheck(); 

			bool disabled = globalConfiguration.projectType != GlobalConfiguration.ProjectType.CUSTOM;
			EditorGUI.BeginDisabledGroup(disabled);
			EditorGUI.BeginChangeCheck();
			globalConfiguration.defaultPreset = (Material)EditorGUILayout.ObjectField("Default Material", globalConfiguration.defaultPreset, typeof(Material), false);
			bool defaultPresetChanged = EditorGUI.EndChangeCheck();
			EditorGUI.EndDisabledGroup();

			if (projectTypeChanged)
			{
				globalConfiguration.RefreshDefaultMaterial();
			}

			if(projectTypeChanged || defaultPresetChanged)
			{
				globalConfiguration.Save();
			}
		}
	}
}