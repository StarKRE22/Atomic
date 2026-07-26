// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

#ifndef PROJECTION_UTILS_INCLUDED
#define PROJECTION_UTILS_INCLUDED

//Position, relative to rendering bounds (normalized 0-1)
float2 WorldToProjectionUV(float3 positionWS, float2 origin, float size)
{
	return (positionWS.xz - origin.xy) / size;
}

float ProjectionEdgeMask(float3 positionWS, float2 origin, float size, float blendDistance)
{
	const float extents = (size * 0.499);

	//Shift to origin
	positionWS = positionWS - extents;
	
	const float2 boundsMin = origin.xy - extents;
	const float2 boundsMax = origin.xy + extents;
	
	float2 weightDir = min(positionWS.xz - boundsMin, boundsMax - positionWS.xz) / blendDistance;
	
	return saturate(min(weightDir.x, weightDir.y));
}

#endif