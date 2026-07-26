using UnityEngine;

namespace AllIn13DShader
{
    [RequireComponent(typeof(Light))]
    public class AllIn1FadeLight : MonoBehaviour
    {
        [SerializeField] private float fadeDuration = 0.1f;
        [SerializeField] private bool destroyWhenFaded = true;
        private Light targetLight;
        private float animationRatioRemaining = 1f;
        private float iniLightIntensity;

        private void Start()
        {
            targetLight = GetComponent<Light>();
            iniLightIntensity = targetLight.intensity;
        }

        private void Update()
        {
            targetLight.intensity = Mathf.Lerp(0f, iniLightIntensity, animationRatioRemaining);
            animationRatioRemaining -= Time.deltaTime / fadeDuration;
            if(destroyWhenFaded && animationRatioRemaining <= 0f) Destroy(gameObject);
        }
    }
}