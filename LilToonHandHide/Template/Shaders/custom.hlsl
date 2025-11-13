//----------------------------------------------------------------------------------------------------------------------
// Macro

// Custom variables
#define LIL_CUSTOM_PROPERTIES \
    float _EnableFakeArm; \
    float _EnableFakeLeftArm;

// Custom textures
#define LIL_CUSTOM_TEXTURES

// Add vertex shader input
#define LIL_REQUIRE_APP_POSITION
#define LIL_REQUIRE_APP_COLOR

// Add vertex shader output - force vertex color passthrough
#define LIL_V2F_FORCE_TEXCOORD0
#define LIL_CUSTOM_V2F_MEMBER(id0,id1,id2,id3,id4,id5,id6,id7) \
    float4 vertexColor : TEXCOORD##id0;

// Add vertex copy
#define LIL_CUSTOM_VERT_COPY \
    output.vertexColor = input.color;

// Store vertex color in fd immediately after unpacking
#define BEFORE_ANIMATE_MAIN_UV \
    float4 customArmColor = input.vertexColor;

// Check and discard AFTER all color calculations, right before final output
#define BEFORE_FOG \
    if (_EnableFakeArm > 0.5) { \
        if (customArmColor.g < 0.01 && customArmColor.b < 0.01) { \
            discard; \
        } \
    } \
    if (_EnableFakeLeftArm > 0.5) { \
        if (customArmColor.r < 0.01 && customArmColor.g < 0.01) { \
            discard; \
        } \
    }

//----------------------------------------------------------------------------------------------------------------------
// Vertex shader
#define LIL_CUSTOM_VERTEX_OS

//----------------------------------------------------------------------------------------------------------------------
// Fragment shader
#define LIL_CUSTOM_FRAG_INPUTS
