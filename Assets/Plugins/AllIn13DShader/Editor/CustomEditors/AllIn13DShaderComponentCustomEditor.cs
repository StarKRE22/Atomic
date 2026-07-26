using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace AllIn13DShader
{
	[CustomEditor(typeof(AllIn13DShaderComponent))]
	[CanEditMultipleObjects]
	public class AllIn13DShaderComponentCustomEditor : Editor
	{
		private PropertiesConfigCollection propertiesConfigCollection;
		private GlobalConfiguration globalConfiguration;
		private Texture imageInspector;

		private void RefreshReferences()
		{
			propertiesConfigCollection = EditorUtils.FindAsset<ScriptableObject>("PropertiesConfigCollection") as PropertiesConfigCollection;
			if(propertiesConfigCollection == null)
			{
				propertiesConfigCollection = PropertiesConfigCreator.CreateConfig();
			}

			globalConfiguration = EditorUtils.FindAssetByName<GlobalConfiguration>("GlobalConfiguration");
		}

		private void OnEnable()
		{
			if (!Application.isPlaying)
			{
				RefreshReferences();

				bool isValidComponents = CheckSelectedComponents();
				if (!isValidComponents)
				{
					EditorUtility.DisplayDialog("Missing Renderer", "Some of the selected game objects have no Renderer component. AllIn13DShaderComponent will be removed", "Ok");
					return;
				}

				CheckMaterialReference();
			}
		}

		public override void OnInspectorGUI()
		{
			bool saveAssets = false;

			serializedObject.Update();

			DrawHeaderImage();

			EditorGUI.BeginDisabledGroup(Application.isPlaying);
			if(GUILayout.Button("Deactivate All Effects"))
			{
				ExecuteActionAfterCheck(DeactivateAllEffects);
				saveAssets = true;
			}

			if (GUILayout.Button("New Clean Material"))
			{
				ExecuteActionAfterCheck(NewCleanMaterial);
			}

			if(GUILayout.Button("Create New Material With Same Properties (SEE DOC)"))
			{
				ExecuteActionAfterCheck(MakeCopyMaterial);
			}

			if(GUILayout.Button("Save Material To Folder (SEE DOC)"))
			{
				ExecuteActionAfterCheck(SaveMaterialToFolder);
			}

			if(GUILayout.Button("Apply Material To All Children"))
			{
				ExecuteActionAfterCheck(ApplyMaterialToAllChildren);
			}

			if (GUILayout.Button("Render Material To Image"))
			{
				ExecuteActionAfterCheck(RenderMaterialToImage);
			}
			EditorGUI.EndDisabledGroup();

			serializedObject.ApplyModifiedProperties();


			if (saveAssets)
			{
				AssetDatabase.SaveAssets();
				EditorSceneManager.SaveOpenScenes();
				AssetDatabase.Refresh();
			}

			EditorGUILayout.Space();
			EditorUtils.DrawThinLine();

			if (GUILayout.Button("Remove Component"))
			{
				RemoveComponent();
			}

			if(GUILayout.Button("Remove Component and Material"))
			{
				RemoveComponentAndMaterial();
			}
		}

		private void DrawHeaderImage()
		{
			if(imageInspector == null) imageInspector = AllIn13DShaderConfig.GetInspectorImage();
			Rect rect = EditorGUILayout.GetControlRect(GUILayout.Height(32));
			GUI.DrawTexture(rect, imageInspector, ScaleMode.ScaleToFit, true);
		}

		private void CheckMaterialReference()
		{
			for (int i = 0; i < targets.Length; i++)
			{
				AllIn13DShaderComponent allIn13DShaderComponent = (AllIn13DShaderComponent)targets[i];

				Material currMaterial = allIn13DShaderComponent.currMaterial;
				if (currMaterial == null || !propertiesConfigCollection.IsAllIn3DShaderMaterial(currMaterial))
				{
					Shader shader = propertiesConfigCollection.shaderPropertiesConfig[0].shader;

					Material oldMaterial = allIn13DShaderComponent.currMaterial;
					allIn13DShaderComponent.NewCleanMaterial(shader, globalConfiguration.defaultPreset);

					MaterialConverterTool.ApplyConversion(oldMaterial, allIn13DShaderComponent.currMaterial);
				}
			}

			EditorUtils.SetDirtyCurrentScene();
		}

		private void DeactivateAllEffects()
		{
			bool successOperation = true;
			bool selectedComponentsAreValid = CheckSelectedComponents();

			for (int i = 0; i < targets.Length; i++)
			{
				AllIn13DShaderComponent allIn13DShaderComponent = (AllIn13DShaderComponent)targets[i];
				successOperation = successOperation && allIn13DShaderComponent.CheckValidComponent();

				Material mat = allIn13DShaderComponent.currMaterial;
				PropertiesConfig propertiesConfig = propertiesConfigCollection.FindPropertiesConfigByShader(mat.shader);

				List<AllIn13DEffectConfig> effects = propertiesConfig.GetAllEffects();

				for (int j = 0; j < effects.Count; j++)
				{
					mat.SetFloat(effects[j].keywordPropertyName, 0f);
				}

				EditorUtility.SetDirty(allIn13DShaderComponent);
				EditorUtility.SetDirty(mat);
			}
		}

		private void NewCleanMaterial()
		{
			for (int i = 0; i < targets.Length; i++)
			{
				AllIn13DShaderComponent allIn13DShaderComponent = (AllIn13DShaderComponent)targets[i];
				
				Shader shader = propertiesConfigCollection.shaderPropertiesConfig[0].shader;
				allIn13DShaderComponent.NewCleanMaterial(shader, globalConfiguration.defaultPreset);
			}
		}

		public void MakeCopyMaterial()
		{
			for (int i = 0; i < targets.Length; i++)
			{
				AllIn13DShaderComponent allIn13DShaderComponent = (AllIn13DShaderComponent)targets[i];
				
				Renderer currRenderer = allIn13DShaderComponent.currRenderer;
				Material currMat = currRenderer.sharedMaterial;

				string materialName = "MAT_" + allIn13DShaderComponent.gameObject.name;
				Material copy = new Material(currMat);
				copy.name = materialName;

				currRenderer.sharedMaterial = copy;

				EditorUtility.SetDirty(allIn13DShaderComponent);
			}
		}

		public void SaveMaterialToFolder()
		{
			for (int i = 0; i < targets.Length; i++)
			{
				AllIn13DShaderComponent allIn13DShaderComponent = (AllIn13DShaderComponent)targets[i];
				SaveMaterialToFolder(allIn13DShaderComponent);
			}

			EditorUtils.SetDirtyCurrentScene();
		}

		private bool CheckSelectedComponents()
		{
			bool res = true;

			for (int i = 0; i < targets.Length; i++)
			{
				AllIn13DShaderComponent allIn13DShaderComponent = (AllIn13DShaderComponent)targets[i];

				bool isValid = allIn13DShaderComponent.CheckValidComponent();
				if (!isValid)
				{
					DestroyImmediate(allIn13DShaderComponent);
				}

				res = res && isValid;
			}

			return res;
		}

		private void ExecuteActionAfterCheck(Action action)
		{
			bool selectedComponentsAreValid = CheckSelectedComponents();

			if (selectedComponentsAreValid)
			{
				action();
			}
			else
			{
				SceneView.lastActiveSceneView.ShowNotification(new GUIContent("Some of the selected components are not valid"));
			}
		}

		private void SaveMaterialToFolder(AllIn13DShaderComponent comp)
		{
			Material matToSave = comp.currMaterial;
			bool isAlreadySaved = AssetDatabase.Contains(matToSave);
			if (isAlreadySaved)
			{
				matToSave = comp.DuplicateCurrentMaterial();
			}

			string folderPath = GlobalConfiguration.instance.MaterialSavePath;
			if (!Directory.Exists(folderPath))
			{
				bool ok = EditorUtility.DisplayDialog("The desired save folder doesn't exist",
					"Go to Window -> AllIn13DShaderWindow and set a valid folder", "Set default values and save material", "Cancel");

				if (ok)
				{
					Directory.CreateDirectory(folderPath);
				}
			}

			if (Directory.Exists(folderPath))
			{
				string fullPath = Path.Combine(folderPath, matToSave.name + ".mat");
				fullPath = AssetDatabase.GenerateUniqueAssetPath(fullPath);

				AssetDatabase.CreateAsset(matToSave, fullPath);
				AssetDatabase.Refresh();

				EditorGUIUtility.PingObject(matToSave);
			}
		}

		private void ApplyMaterialToAllChildren()
		{
			for (int i = 0; i < targets.Length; i++)
			{
				AllIn13DShaderComponent allIn13DShaderComponent = (AllIn13DShaderComponent)targets[i];
				allIn13DShaderComponent.ApplyMaterialToChildren();

				EditorUtility.SetDirty(allIn13DShaderComponent);
			}

			EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
		}

		private void RenderMaterialToImage()
		{
			for (int i = 0; i < targets.Length; i++)
			{
				AllIn13DShaderComponent allIn13DShaderComponent = (AllIn13DShaderComponent)targets[i];
				RenderToImage(allIn13DShaderComponent);

				EditorUtility.SetDirty(allIn13DShaderComponent);
			}
		}

		public void RenderToImage(AllIn13DShaderComponent allIn13DShaderComponent)
		{
			Texture tex = allIn13DShaderComponent.currMaterial.GetTexture("_MainTex");
			if (tex != null)
			{
				string folderPath = GlobalConfiguration.instance.RenderImageSavePath;
				string fileName = allIn13DShaderComponent.gameObject.name + ".png";
				RenderMaterialToImageTool.RenderAndSaveTexture(allIn13DShaderComponent.currMaterial, tex, 4.0f, folderPath, fileName);
			}
			else
			{
				EditorUtility.DisplayDialog("No valid target texture found",
					   "All In 1 3DShader component couldn't find a valid Main Texture in this GameObject (" +
					   allIn13DShaderComponent.gameObject.name +
					   "). This means that the material you are using has no Main Texture or that the texture couldn't be reached through the Renderer component you are using." +
					   " Please make sure to have a valid Main Texture in the Material", "Ok");
			}
		}

		private void RemoveComponent()
		{
			for (int i = targets.Length - 1; i >= 0; i--)
			{
				AllIn13DShaderComponent allIn13DShaderComponent = (AllIn13DShaderComponent)targets[i];
				DestroyImmediate(allIn13DShaderComponent);
			}

			//EditorUtils.SetDirtyCurrentScene();

			SetSceneDirty();
			EditorUtils.ShowNotification("AllIn3DShader: Component Removed");
		}

		private void RemoveComponentAndMaterial()
		{
			for (int i = 0; i < targets.Length; i++)
			{
				AllIn13DShaderComponent allIn13DShaderComponent = (AllIn13DShaderComponent)targets[i];
				allIn13DShaderComponent.CleanMaterial();
			}

			for (int i = targets.Length - 1; i >= 0; i--)
			{
				AllIn13DShaderComponent allIn13DShaderComponent = (AllIn13DShaderComponent)targets[i];
				DestroyImmediate(allIn13DShaderComponent);
			}

			SetSceneDirty();
		}

		public void SetSceneDirty()
		{
			if (!Application.isPlaying) EditorSceneManager.MarkAllScenesDirty();

			//If you get an error here please delete the code block below
#if UNITY_2021_2_OR_NEWER
			var prefabStage = UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage();
#else
            var prefabStage = UnityEditor.Experimental.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage();
#endif
			if (prefabStage != null)
			{
				EditorSceneManager.MarkSceneDirty(prefabStage.scene);
			}	

			//Until here
		}
	}
}