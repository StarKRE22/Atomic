using System.Collections;
using UnityEngine;

namespace AllIn13DShader
{
    public class MaterialPrewarmer : MonoBehaviour
    {
        [SerializeField] private Material[] materialsToPrewarm;
        [SerializeField] private Renderer rendererPrefab;
        [SerializeField] private GameObject[] objectsToPrewarm;
    
        private void Start()
        {
            StartCoroutine(PrewarmMaterials());
        }
    
        private IEnumerator PrewarmMaterials()
        {
            if(materialsToPrewarm == null || materialsToPrewarm.Length == 0)
            {
                Debug.LogWarning("No materials assigned for prewarming.");
                Destroy(gameObject);
                yield break;
            }
        
            if(rendererPrefab == null)
            {
                Debug.LogError("Renderer prefab is not assigned for prewarming.");
                Destroy(gameObject);
                yield break;
            }
            
            Vector3 spawnPos = Vector3.down * 1000f;
        
            // Spawn renderers and assign materials
            for(int i = 0; i < materialsToPrewarm.Length; i++)
            {
                if(materialsToPrewarm[i] == null) continue;
            
                Renderer instance = Instantiate(rendererPrefab, spawnPos, Quaternion.identity, transform);
                instance.material = materialsToPrewarm[i];
            }
            
            for(int i = 0; i < objectsToPrewarm.Length; i++)
            {
                Instantiate(objectsToPrewarm[i], spawnPos, Quaternion.identity, transform);
            }
        
            // Give the GPU a frame to process everything
            yield return new WaitForEndOfFrame();
            yield return null;
        
            Destroy(gameObject);
        }
    }
}