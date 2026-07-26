#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

namespace AllIn13DShader
{
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	[AddComponentMenu("AllIn13DShader/AddAllIn13DShader")]
	public class AllIn13DShaderComponent : MonoBehaviour
	{
		public Renderer currRenderer;
		public Material currMaterial
		{
			get
			{
				return currRenderer.sharedMaterial;
			}
			set
			{
				currRenderer.sharedMaterial = value;
			}
		}

#if UNITY_EDITOR
		public void NewCleanMaterial(Shader shader, Material matPreset)
		{
			Material previousMat = currMaterial;

			currMaterial = new Material(shader);
			currMaterial.CopyMatchingPropertiesFromMaterial(matPreset);

			string materialName = currMaterial.name;
			int nameStartIndex = materialName.LastIndexOf("/");
			if(nameStartIndex >= 0)
			{
				materialName = materialName.Substring(nameStartIndex + 1);
			}

			currMaterial.name = $"MAT_{materialName}";
		}

		public void CleanMaterial()
		{
			currMaterial = new Material(Shader.Find(ConstantsRuntime.STANDARD_SHADER_NAME));
		}
		 
		public bool CheckValidComponent()
		{
			bool res = true;

			bool dirty = false;
			if (currRenderer == null || currMaterial == null)
			{
				res = res && TryGetComponent<Renderer>(out currRenderer);
				dirty = true;
			}

			if (dirty && res)
			{
				currRenderer = GetComponent<Renderer>();
				
				EditorUtility.SetDirty(this);

				if(currMaterial != null)
				{
					EditorUtility.SetDirty(currMaterial);
				}
			}

			return res;
		}

		public void ApplyMaterialToChildren()
		{
			ApplyMaterialRecursively(transform, currMaterial);
		}

		public Material DuplicateCurrentMaterial()
		{
			currMaterial = new Material(currMaterial);
			return currMaterial;
		}

		private void ApplyMaterialRecursively(Transform tr, Material mat)
		{
			bool existsMeshRenderer = tr.TryGetComponent<Renderer>(out currRenderer);

			if (existsMeshRenderer)
			{
				currRenderer.sharedMaterial = mat;
			}

			int childCount = tr.childCount;
			for(int i = 0; i < childCount; i++)
			{
				ApplyMaterialRecursively(tr.GetChild(i), mat);
			}
		}
#endif
	}
}