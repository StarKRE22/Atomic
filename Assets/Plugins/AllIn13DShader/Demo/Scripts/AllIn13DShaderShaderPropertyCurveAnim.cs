using UnityEngine;
using System.Collections.Generic;

namespace AllIn13DShader
{
    public class AllIn13DShaderShaderPropertyCurveAnim : MonoBehaviour
    {
        [SerializeField] private string numericPropertyName = "_HsvShift";
        
        [SerializeField] private AnimationCurve animationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        [SerializeField] private float animationDuration = 1f;
        [SerializeField] private float waitDuration = 0.5f;
        [SerializeField] private float minValue = 0f;
        [SerializeField] private float maxValue = 1f;
        
        [Space, SerializeField, Header("If empty uses instances of currently used Materials")]
        private Material[] materials;
        private Material[] originalMaterials;
        private bool restoreMaterialsOnDisable = false;

        private int propertyShaderID;
        private float timer = 0f;
        private bool isAnimating = true;
        private bool isValid = true;

        private void Start()
        {
            if(materials == null || materials.Length == 0)
            {
                Renderer renderer = GetComponent<Renderer>();
                if(renderer != null) materials = renderer.materials;
            }
            else
            {
                originalMaterials = new Material[materials.Length];
                for(int i = 0; i < materials.Length; i++)
                    if(materials[i] != null) originalMaterials[i] = new Material(materials[i]);
                restoreMaterialsOnDisable = true;
            }

            if(materials == null || materials.Length == 0) DestroyComponentAndLogError(gameObject.name + " has no valid Materials, deleting AllIn1VfxScrollShaderProperty component");
            else
            {
                bool allValid = true;
                for(int i = 0; i < materials.Length; i++)
                {
                    if(materials[i] == null || !materials[i].HasProperty(numericPropertyName))
                    {
                        allValid = false;
                        break;
                    }
                }
                
                if(allValid) 
                    propertyShaderID = Shader.PropertyToID(numericPropertyName);
                else 
                    DestroyComponentAndLogError(gameObject.name + "'s Material(s) don't all have a " + numericPropertyName + " property");
            }
        }

        private void Update()
        {
            if(materials == null || materials.Length == 0)
            {
                if(isValid)
                {
                    Debug.LogError("The object " + gameObject.name + " has no Materials and you are trying to access them. Please take a look");
                    isValid = false;   
                }
                return;
            }
            
            timer += Time.deltaTime;
            
            if(isAnimating)
            {
                if(timer < animationDuration)
                {
                    float normalizedTime = timer / animationDuration;
                    float curveValue = animationCurve.Evaluate(normalizedTime);
                    float remappedValue = Mathf.Lerp(minValue, maxValue, curveValue);
                    
                    for(int i = 0; i < materials.Length; i++)
                        if(materials[i] != null) materials[i].SetFloat(propertyShaderID, remappedValue);
                }
                else
                {
                    isAnimating = false;
                    timer = 0f;
                }
            }
            else
            {
                if(timer >= waitDuration)
                {
                    isAnimating = true;
                    timer = 0f;
                }
            }
        }

        private void DestroyComponentAndLogError(string logError)
        {
            Debug.LogError(logError);
            Destroy(this);
        }

        private void OnDisable()
        {
            if(restoreMaterialsOnDisable && materials != null && originalMaterials != null)
            {
                for(int i = 0; i < materials.Length; i++)
                    if(materials[i] != null && originalMaterials[i] != null)
                        materials[i].CopyPropertiesFromMaterial(originalMaterials[i]);
            }
        }

        private void OnDestroy()
        {
            if(originalMaterials != null)
                for(int i = 0; i < originalMaterials.Length; i++)
                    if(originalMaterials[i] != null) Destroy(originalMaterials[i]);
        }
    }
}