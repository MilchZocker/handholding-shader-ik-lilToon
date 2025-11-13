//----------------------------------------------------------------------------------------------------------------------
// Macro

// Custom variables
#define LIL_CUSTOM_PROPERTIES \
    float _EnableFakeArm; \
    float _BoneLength; \
    float _ExtraForearmLength; \
    float _ExtraGrabRatio; \
    float _ShaderIKTargetLightIntensity; \
    float _IsLeftArm;

// Custom textures
#define LIL_CUSTOM_TEXTURES

// Add vertex shader input
#define LIL_REQUIRE_APP_POSITION
#define LIL_REQUIRE_APP_COLOR

// NO SCALING - apply IK transform directly to normal-sized mesh
#define LIL_CUSTOM_VERTEX_OS \
    bool isLeftArm = (_IsLeftArm >= 0.5); \
    positionOS = transformArm( \
        positionOS, \
        input.color, \
        _ShaderIKTargetLightIntensity, \
        float4(0.001, (isLeftArm ? -1.0 : 1.0) * -0.002, -0.003, 1.0), \
        _BoneLength / 1000000.0, \
        (_BoneLength + _ExtraForearmLength) / 1000000.0, \
        (_BoneLength * _ExtraGrabRatio + _ExtraForearmLength) / 1000000.0, \
        (_BoneLength * _ExtraGrabRatio + _ExtraForearmLength) / 1000000.0, \
        (_BoneLength * _ExtraGrabRatio + _ExtraForearmLength) / 1000000.0, \
        0.95, \
        isLeftArm \
    );

// Discard if disabled
#define BEFORE_FOG \
    if (_EnableFakeArm < 0.5) { \
        discard; \
    }

//----------------------------------------------------------------------------------------------------------------------
// Vertex shader
#define LIL_CUSTOM_VERTEX_OS

//----------------------------------------------------------------------------------------------------------------------
// Fragment shader
#define LIL_CUSTOM_FRAG_INPUTS
