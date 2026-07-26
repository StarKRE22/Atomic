
using UnityEditor;
using UnityEngine;

namespace AllIn13DShader
{
	public class DepthColoringPropertiesDrawer
	{
		private AllIn1DepthColoringProperties depthColoringProperties;

		private CommonStyles commonStyles;

		private GradientEditorDrawer gradientDrawer;

		private DepthColoringCamera depthColoringCamera;
		private bool isActive;

		public DepthColoringPropertiesDrawer(AllIn1DepthColoringProperties depthColoringProperties)
		{
			SetDepthColoringProperties(depthColoringProperties);
		}

		public void SetDepthColoringProperties(AllIn1DepthColoringProperties depthColoringProperties)
		{
			this.depthColoringProperties = depthColoringProperties;
		}

		private void RefreshReferences()
		{
			if (gradientDrawer == null)
			{
				gradientDrawer = new GradientEditorDrawer();
			}

			if (commonStyles == null)
			{
				commonStyles = new CommonStyles();
				commonStyles.InitStyles();
			}

			if(depthColoringCamera == null)
			{
				depthColoringCamera = GameObject.FindObjectOfType<DepthColoringCamera>();
			}
		}

		public void Draw(bool useBoxStyle = false)
		{
			RefreshReferences();

			if (useBoxStyle)
			{
				EditorGUILayout.BeginVertical(commonStyles.boxStyle);
			}
			else
			{
				EditorGUILayout.BeginVertical();
			}


			EditorGUI.BeginChangeCheck();

			Rect rect = EditorGUILayout.GetControlRect(true, gradientDrawer.GetPropertyHeight());

			depthColoringProperties.depthColoringGradientTex = (Texture2D)gradientDrawer.Draw(rect, depthColoringProperties.depthColoringGradientTex);

			depthColoringProperties.fallOff = EditorGUILayout.Slider("Fall Off", depthColoringProperties.fallOff, 0.1f, 1.5f);

			GUILayout.Space(20f);
			if (depthColoringCamera == null)
			{
				EditorGUILayout.LabelField("Depth coloring camera not found in the scene. You need to add DepthColoringCamera.cs component to the main camera in order to use this effect.", commonStyles.warningLabel);
				isActive = false;
			}
			else
			{
				isActive = depthColoringCamera.depthColoringProperties == depthColoringProperties;

				if (isActive)
				{
					EditorGUILayout.LabelField("Active", commonStyles.okLabel);
				}
				else
				{
					EditorGUILayout.LabelField("This properties are not active", commonStyles.warningLabel);
					if (GUILayout.Button("Active"))
					{
						depthColoringCamera.depthColoringProperties = depthColoringProperties;
						isActive = true;
					}
				}
			}
			EditorGUILayout.EndVertical();

			if (EditorGUI.EndChangeCheck() && isActive)
			{
				ApplyChanges();
			}
		}

		private void ApplyChanges()
		{
			EditorUtility.SetDirty(depthColoringProperties);
		
			depthColoringProperties.ApplyValues();
		}
	}
}