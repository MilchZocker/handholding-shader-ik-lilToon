//----------------------------------------------------------------------------------------------------------------------
// Includes
#include "Assets/HandHolding/handholding-shader-ik-master/Assets/HaiHandholdingShaderIK/HaiHandholdingShaderIK.cginc"

//----------------------------------------------------------------------------------------------------------------------
// Macro

// Custom variables
#define LIL_CUSTOM_PROPERTIES \
    float _EnableFakeArm; \
    float _BoneLength; \
    float _ExtraForearmLength; \
    float _ExtraGrabRatio; \
    float _ShaderIKTargetLightIntensity; \
    float _VertexScale; \
    float _IsLeftArm;

// Fallback declarations for passes (e.g., shadow/depth) where lilToon may skip injecting custom properties
#ifndef _EnableFakeArm
float _EnableFakeArm;
#endif
#ifndef _BoneLength
float _BoneLength;
#endif
#ifndef _ExtraForearmLength
float _ExtraForearmLength;
#endif
#ifndef _ExtraGrabRatio
float _ExtraGrabRatio;
#endif
#ifndef _ShaderIKTargetLightIntensity
float _ShaderIKTargetLightIntensity;
#endif
#ifndef _VertexScale
float _VertexScale;
#endif
#ifndef _IsLeftArm
float _IsLeftArm;
#endif

// Custom textures
#define LIL_CUSTOM_TEXTURES

// Add vertex shader input
#define LIL_REQUIRE_APP_POSITION
#define LIL_REQUIRE_APP_COLOR

// Apply IK with vertex scaling; lengths in same scale as properties
#define LIL_CUSTOM_VERTEX_OS \
    bool isLeftArm = (_IsLeftArm >= 0.5); \
    float scaleFactor = _VertexScale; \
    float lengthScale = scaleFactor / 1000000.0; \
    float4 restPos = float4(0.001, (isLeftArm ? -1.0 : 1.0) * -0.002, -0.003, 1.0) * scaleFactor; \
    float4 visibleVertex = float4(positionOS.xyz * scaleFactor, 1.0); \
    positionOS = transformArm( \
        visibleVertex, \
        input.color, \
        _ShaderIKTargetLightIntensity, \
        restPos, \
        _BoneLength * lengthScale, \
        (_BoneLength + _ExtraForearmLength) * lengthScale, \
        (_BoneLength * _ExtraGrabRatio + _ExtraForearmLength) * lengthScale, \
        (_BoneLength * _ExtraGrabRatio + _ExtraForearmLength) * lengthScale, \
        (_BoneLength * _ExtraGrabRatio + _ExtraForearmLength) * lengthScale, \
        0.95, \
        isLeftArm \
    );

#define BEFORE_FOG \
    if (_EnableFakeArm < 0.5) { \
        discard; \
    }

//----------------------------------------------------------------------------------------------------------------------
// Fragment shader
#define LIL_CUSTOM_FRAG_INPUTS
