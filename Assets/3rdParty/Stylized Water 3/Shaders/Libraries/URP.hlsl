// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

#ifndef PIPELINE_INCLUDED
#define PIPELINE_INCLUDED
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderVariablesFunctions.hlsl"

#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/UnityInstancing.hlsl"

#ifndef _DISABLE_DEPTH_TEX
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"
#endif

#if _REFRACTION || UNDERWATER_ENABLED
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareOpaqueTexture.hlsl"
#endif

// Deprecated in URP 11+ https://github.com/Unity-Technologies/Graphics/pull/2529. Keep function for backwards compatibility
// Compute Normalized Device Coordinate here (this is normally done in GetVertexPositionInputs, but clip and world-space coords are done manually already)
#if UNITY_VERSION >= 202110 && !defined(UNITY_SHADER_VARIABLES_FUNCTIONS_DEPRECATED_INCLUDED)
float4 ComputeScreenPos(float4 positionCS)
{
	return ComputeNormalizedDeviceCoordinates(positionCS);
}
#endif
#endif