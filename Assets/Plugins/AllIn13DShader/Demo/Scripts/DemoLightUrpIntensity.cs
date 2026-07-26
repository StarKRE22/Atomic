using UnityEngine;

namespace AllIn13DShader
{
    public class DemoLightUrpIntensity : MonoBehaviour
    {
        [SerializeField] private Light thisLight;
        [SerializeField] private float urpIntensityMultiplier = 5f;

#if ALLIN13DSHADER_URP
        private void Start()
        {
            thisLight.intensity *= urpIntensityMultiplier;
        }
#endif

        private void Reset()
        {
            if(thisLight == null) 
            {
                thisLight = GetComponent<Light>();
            }
        }
    }
}