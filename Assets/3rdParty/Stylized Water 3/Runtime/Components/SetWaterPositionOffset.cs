// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//  • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//  • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

using UnityEngine;

namespace StylizedWater3
{
    [ExecuteAlways]
    [AddComponentMenu("Stylized Water 3/Water Position Offset")]
    public class SetWaterPositionOffset : MonoBehaviour
    {
        private void Update()
        {
            //Note: in a floating origin system, apply the value after offsetting all the transforms!
            //Otherwise the water geometry gets shifted in one frame, and this offset is applied the next. This induces a jitter.
            StylizedWater3.WaterObject.PositionOffset = this.transform.position;
        }

        private void OnDisable()
        {
            StylizedWater3.WaterObject.PositionOffset = Vector3.zero;
        }
    }
}