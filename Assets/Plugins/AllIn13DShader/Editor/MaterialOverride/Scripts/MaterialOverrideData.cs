using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace AllIn13DShader
{
	[System.Serializable]
	public class MaterialOverrideData : ScriptableObject
	{
		public class MaterialOverride
		{
			public Material sourceMaterial;
			public Material previewMaterial;

			public MaterialOverride(Material sourceMaterial)
			{
				this.sourceMaterial = sourceMaterial;

				if(sourceMaterial == null)
				{
					this.previewMaterial = null;
				}
				else
				{
					this.previewMaterial = new Material(sourceMaterial);
					this.previewMaterial.name = this.previewMaterial.name + "_PREVIEW";
				}
			}

			public void ApplyPreviewToSource()
			{
				sourceMaterial.CopyMatchingPropertiesFromMaterial(previewMaterial);
			}


			public bool ApplySourceToPreview()
			{
				bool res = true;

				if (previewMaterial == null || sourceMaterial == null)
				{
					res = false;
				}
				else
				{
					previewMaterial.CopyMatchingPropertiesFromMaterial(sourceMaterial);
				}

				return res;
			}
		}

		public class RendererOverride
		{
			public Renderer renderer;
			public MaterialOverride[] materialsOverrides;

			public Material[] sourceMaterials;
			public Material[] previewMaterials;

			public RendererOverride(Renderer renderer)
			{
				this.renderer = renderer;
				this.materialsOverrides = new MaterialOverride[renderer.sharedMaterials.Length];

				sourceMaterials = new Material[materialsOverrides.Length];
				previewMaterials = new Material[materialsOverrides.Length];

				for (int i = 0; i < materialsOverrides.Length; i++)
				{
					materialsOverrides[i]	= new MaterialOverride(renderer.sharedMaterials[i]);
					sourceMaterials[i]		= materialsOverrides[i].sourceMaterial;
					previewMaterials[i]		= materialsOverrides[i].previewMaterial;
				}
			}

			public void UsePreviewMaterials()
			{
				if (renderer != null)
				{
					renderer.sharedMaterials = previewMaterials;
				}
			}

			public void UseMaterialSource()
			{
				if (renderer != null)
				{
					renderer.sharedMaterials = sourceMaterials;
				}
			}

			public void ApplyPreviewMaterial()
			{
				for (int i = 0; i < materialsOverrides.Length; i++)
				{
					materialsOverrides[i].ApplyPreviewToSource();
					renderer.sharedMaterials[i] = materialsOverrides[i].sourceMaterial;
				}
			}

			//public void ApplyPropertyOverrideOnPreviewMaterial(PropertyOverride propertyOverride)
			//{
			//	switch (propertyOverride.shaderPropertyType)
			//	{
			//		case ShaderPropertyType.Float:
			//		case ShaderPropertyType.Range:
			//			materialOverride.previewMaterial.SetFloat(propertyOverride.propertyName, propertyOverride.floatValue);
			//			break;
			//	}
			//}

			public Material[] GetPreviewMaterials()
			{
				return previewMaterials;
			}

			public void CleanPreviewMaterials()
			{
				for(int i = 0; i < materialsOverrides.Length; i++)
				{
					MaterialOverride matOverride = materialsOverrides[i];

					matOverride.ApplySourceToPreview();

					string[] keywords = new string[matOverride.sourceMaterial.shaderKeywords.Length];
					matOverride.sourceMaterial.shaderKeywords.CopyTo(keywords, 0);

					matOverride.previewMaterial.shaderKeywords = keywords;
				}
			}
		}

		public enum ApplyTarget
		{
			NONE = 0,
			SELECTED_FOLDERS = 1,
			CURRENT_SCENE = 2,
			ALL_PROJECT = 3,
		}

		public List<AbstractEffectOverride> effectOverrides;
		public List<PropertyOverride> generalPropertiesOverrides;

		public RendererOverride[] rendererOverrides;

		public ApplyTarget applyTarget;

		[SerializeField] private Object[] folders;

		public void Initialize()
		{
			ResetData();
		}

		public void ResetData()
		{
			effectOverrides = new List<AbstractEffectOverride>();
			generalPropertiesOverrides = new List<PropertyOverride>();
			
			rendererOverrides = new RendererOverride[0];
			applyTarget = ApplyTarget.NONE;
		}

		public void CreateRendererOverride()
		{
			Renderer[] renderers = FindObjectsOfType<Renderer>();
			for (int i = 0; i < renderers.Length; i++)
			{
				if (renderers[i] is ParticleSystemRenderer) { continue; }

				RendererOverride rendererOverride = new RendererOverride(renderers[i]);
				ArrayUtility.Add(ref rendererOverrides, rendererOverride);
			}
		}

		public List<Material> CollectAffectedMaterials()
		{
			List<Material> res = new List<Material>();

			switch (applyTarget)
			{
				case ApplyTarget.SELECTED_FOLDERS:
					CollectAffectedMaterialsOnSelectedFolders(res);
					break;
				case ApplyTarget.CURRENT_SCENE:
					CollectAffectedMaterialsOnCurrentScene(res);
					break;
				case ApplyTarget.ALL_PROJECT:
					CollectAffectedMaterialsAllProject(res);
					break;
			}

			return res;
		}

		public void UsePreviewMaterials()
		{
			for (int i = 0; i < rendererOverrides.Length; i++)
			{
				rendererOverrides[i].UsePreviewMaterials();
			}
		}

		public void UseMaterialSource()
		{
			for (int i = 0; i < rendererOverrides.Length; i++)
			{
				rendererOverrides[i].UseMaterialSource();
			}
		}

		public void EndOverrideProcess()
		{
			UseMaterialSource();

			effectOverrides = new List<AbstractEffectOverride>();
			rendererOverrides = new RendererOverride[0];
		}

		public void ApplyChangesToMaterials(Material[] materials)
		{
			for (int i = 0; i < materials.Length; i++)
			{
				ApplyChangesToMaterial(materials[i]);
			}
		}

		public void ApplyChangesToMaterials(List<Material> materials)
		{
			for(int i = 0; i < materials.Count; i++)
			{
				ApplyChangesToMaterial(materials[i]);
			}
		}

		public void ApplyChangesToMaterial(Material mat)
		{
			for (int i = 0; i < effectOverrides.Count; i++)
			{
				effectOverrides[i].ApplyChangesToMaterial(mat);
			}

			for (int i = 0; i < generalPropertiesOverrides.Count; i++)
			{
				generalPropertiesOverrides[i].ApplyChangesToMaterial(mat);
			}
		}

		private void CollectAffectedMaterialsOnCurrentScene(List<Material> affectedMaterials)
		{
			for (int i = 0; i < rendererOverrides.Length; i++)
			{
				affectedMaterials.AddRange(rendererOverrides[i].sourceMaterials);
			}
		}

		private void CollectAffectedMaterialsOnSelectedFolders(List<Material> affectedMaterials)
		{
			HashSet<string> materialsInFolderGUIDs = new HashSet<string>();

			for (int i = 0; i < folders.Length; i++)
			{
				string folderPath = AssetDatabase.GetAssetPath(folders[i]);
				if (AssetDatabase.IsValidFolder(folderPath))
				{
					string[] assetsPathsInFolder = AssetDatabase.FindAssets("t: Material", new string[] { folderPath });

					for (int j = 0; j < assetsPathsInFolder.Length; j++)
					{
						materialsInFolderGUIDs.Add(assetsPathsInFolder[j]);
					}
				}
			}

			foreach (string guid in materialsInFolderGUIDs)
			{
				string path = AssetDatabase.GUIDToAssetPath(guid);
				Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);

				if (EditorUtils.IsAllIn13DShader(mat.shader))
				{
					affectedMaterials.Add(mat);
				}
			}
		}

		private void CollectAffectedMaterialsAllProject(List<Material> affectedMaterials)
		{
			string[] materialsGUIDs = AssetDatabase.FindAssets("t: Material");

			foreach (string guid in materialsGUIDs)
			{
				string path = AssetDatabase.GUIDToAssetPath(guid);
				Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);

				if (EditorUtils.IsAllIn13DShader(mat.shader))
				{
					affectedMaterials.Add(mat);
				}
			}
		}

		public bool IsApplyEnabled()
		{
			bool res = false;

			switch (applyTarget)
			{
				case ApplyTarget.SELECTED_FOLDERS:
					res = (folders != null) && (folders.Length > 0);
					break;
				case ApplyTarget.CURRENT_SCENE:
					res = true;
					break;
				case ApplyTarget.ALL_PROJECT:
					res = true;
					break;
			}

			return res;
		}

		public AbstractEffectOverride AddEffectOverride(AllIn13DEffectConfig effectConfig)
		{
			AbstractEffectOverride res = FindEffectOverride(effectConfig.keywordPropertyName);

			if (res == null)
			{
				switch (effectConfig.effectConfigType)
				{
					case EffectConfigType.EFFECT_ENUM:
						res = new EffectEnumOverride(effectConfig);
						break;
					case EffectConfigType.EFFECT_TOGGLE:
						res = new EffectToggleOverride(effectConfig);
						break;
				}

				effectOverrides.Add(res);
			}

			return res;
		}

		public void AddCopmleteEffectOverride(AllIn13DEffectConfig effectConfig, Shader shader)
		{
			AbstractEffectOverride effectOverride = FindEffectOverride(effectConfig.keywordPropertyName);

			if(effectOverride == null)
			{
				effectOverride = AddEffectOverride(effectConfig);
			}

			for (int i = 0; i < effectConfig.effectProperties.Count; i++)
			{
				effectOverride.AddPropertyOverride(effectConfig.effectProperties[i], shader);	
			}
		}

		public void AddPropertyOverride(AllIn13DEffectConfig effectConfig, EffectProperty effectProperty, Shader shader)
		{
			AbstractEffectOverride effectOverride = FindEffectOverride(effectConfig.keywordPropertyName);

			effectOverride = AddEffectOverride(effectConfig);
			effectOverride.AddPropertyOverride(effectProperty.propertyIndex, shader);
		}

		public void AddGeneralPropertyOverride(int propertyIndex, Shader shader)
		{
			PropertyOverride propertyOverride = new PropertyOverride(null, propertyIndex, shader);
			if (!generalPropertiesOverrides.Contains(propertyOverride))
			{
				generalPropertiesOverrides.Add(propertyOverride);
			}
		}

		public void RemoveEffectOverride(AbstractEffectOverride effectOverrideToRemove)
		{
			effectOverrides.Remove(effectOverrideToRemove);
			
			RebuildPreviewMaterial();
		}

		public void RemovePropertyOverride(PropertyOverride propertyOverride)
		{
			bool removeSuccessfully = propertyOverride.Remove();
			if (!removeSuccessfully)
			{
				generalPropertiesOverrides.Remove(propertyOverride);
			}
		}

		public void RebuildPreviewMaterial()
		{
			for (int i = 0; i < rendererOverrides.Length; i++)
			{
				rendererOverrides[i].CleanPreviewMaterials();
			}

			ShowPreviewChanges();
		}

		public void ShowPreviewChanges()
		{
			for (int i = 0; i < rendererOverrides.Length; i++)
			{
				RendererOverride rendererOverride = rendererOverrides[i];

				rendererOverride.UsePreviewMaterials();

				ApplyChangesToMaterials(rendererOverride.previewMaterials);
			}
		}

		public AbstractEffectOverride FindEffectOverride(string effectPropertyName)
		{
			AbstractEffectOverride res = null;

			for(int i = 0; i < effectOverrides.Count; i++)
			{
				if (effectOverrides[i].propertyName == effectPropertyName)
				{
					res = effectOverrides[i];
					break;
				}
			}

			return res;
		}

		public bool IsEmpty()
		{
			bool res = effectOverrides.Count <= 0 && generalPropertiesOverrides.Count <= 0;
			return res;
		}
	}
}