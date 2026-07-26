using UnityEditor;
using UnityEngine;

namespace AllIn13DShader
{
	[CustomEditor(typeof(DepthColoringCamera))]
	public class DepthColoringCameraCustomEditor : Editor
	{
		private SerializedProperty spCam;
		private SerializedProperty spDepthColoringProperties;

		private AllIn1DepthColoringProperties depthColoringProperties;
		private DepthColoringPropertiesDrawer depthColoringPropertiesDrawer;

		private DepthColoringCamera depthColoringCamera;

		private bool depthColoringFoldout;

		private void RefreshReferences()
		{
			if(spCam == null)
			{
				spCam = serializedObject.FindProperty("cam");
			}

			if(spDepthColoringProperties == null)
			{
				spDepthColoringProperties = serializedObject.FindProperty("depthColoringProperties");
			}
		}

		private void RefreshDepthColoringPropertiesDrawer()
		{
			if(depthColoringPropertiesDrawer == null && depthColoringProperties != null)
			{
				depthColoringPropertiesDrawer = new DepthColoringPropertiesDrawer(depthColoringProperties);
			}
			else if(depthColoringPropertiesDrawer != null && depthColoringProperties != null)
			{
				depthColoringPropertiesDrawer.SetDepthColoringProperties(depthColoringProperties);
			}
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			RefreshReferences();

			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(spCam);
			EditorGUILayout.PropertyField(spDepthColoringProperties);

			serializedObject.ApplyModifiedProperties();

			if(spDepthColoringProperties.objectReferenceValue != null)
			{
				GUILayout.Space(25f);

				depthColoringProperties = (AllIn1DepthColoringProperties)spDepthColoringProperties.objectReferenceValue;

				RefreshDepthColoringPropertiesDrawer();

				if(depthColoringPropertiesDrawer != null)
				{
					depthColoringPropertiesDrawer.Draw(true);
				}
			}
		}
	}
}