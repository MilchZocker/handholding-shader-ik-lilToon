#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace lilToon
{
    public class HideArmInspector : lilToonInspector
    {
        // Custom properties
        MaterialProperty enableFakeArm;
        MaterialProperty enableFakeLeftArm;
        
        private static bool isShowCustomProperties;
        private const string shaderName = "lilToonHideArm";
        
        protected override void LoadCustomProperties(MaterialProperty[] props, Material material)
        {
            isCustomShader = true;
            ReplaceToCustomShaders();
            isShowRenderMode = true; // Enable render mode for cutout
            
            enableFakeArm = FindProperty("_EnableFakeArm", props);
            enableFakeLeftArm = FindProperty("_EnableFakeLeftArm", props);
        }
        
        protected override void DrawCustomProperties(Material material)
        {
            isShowCustomProperties = Foldout("Handholding Shader IK - Hide Arm", "Handholding Shader IK - Hide Arm", isShowCustomProperties);
            if(isShowCustomProperties)
            {
                EditorGUILayout.BeginVertical(boxOuter);
                EditorGUILayout.LabelField("Handholding Shader IK - Hide Arm", customToggleFont);
                EditorGUILayout.BeginVertical(boxInnerHalf);
                
                EditorGUILayout.HelpBox("This shader uses Cutout mode to hide the arm. Make sure Cutout is set to 0.5 or less.", MessageType.Info);
                
                if (enableFakeArm != null)
                    m_MaterialEditor.ShaderProperty(enableFakeArm, "Enable Fake Right Arm");
                if (enableFakeLeftArm != null)
                    m_MaterialEditor.ShaderProperty(enableFakeLeftArm, "Enable Fake Left Arm");
                    
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndVertical();
            }
        }
        
        protected override void ReplaceToCustomShaders()
        {
            lts         = Shader.Find(shaderName + "/lilToon");
            ltsc        = Shader.Find("Hidden/" + shaderName + "/Cutout");
            ltst        = Shader.Find("Hidden/" + shaderName + "/Transparent");
            ltsot       = Shader.Find("Hidden/" + shaderName + "/OnePassTransparent");
            ltstt       = Shader.Find("Hidden/" + shaderName + "/TwoPassTransparent");
            ltso        = Shader.Find("Hidden/" + shaderName + "/OpaqueOutline");
            ltsco       = Shader.Find("Hidden/" + shaderName + "/CutoutOutline");
            ltsto       = Shader.Find("Hidden/" + shaderName + "/TransparentOutline");
            ltsoto      = Shader.Find("Hidden/" + shaderName + "/OnePassTransparentOutline");
            ltstto      = Shader.Find("Hidden/" + shaderName + "/TwoPassTransparentOutline");
            
            ltsl        = Shader.Find(shaderName + "/lilToonLite");
            ltslc       = Shader.Find("Hidden/" + shaderName + "/Lite/Cutout");
            ltslt       = Shader.Find("Hidden/" + shaderName + "/Lite/Transparent");
            ltslot      = Shader.Find("Hidden/" + shaderName + "/Lite/OnePassTransparent");
            ltsltt      = Shader.Find("Hidden/" + shaderName + "/Lite/TwoPassTransparent");
            ltslo       = Shader.Find("Hidden/" + shaderName + "/Lite/OpaqueOutline");
            ltslco      = Shader.Find("Hidden/" + shaderName + "/Lite/CutoutOutline");
            ltslto      = Shader.Find("Hidden/" + shaderName + "/Lite/TransparentOutline");
            ltsloto     = Shader.Find("Hidden/" + shaderName + "/Lite/OnePassTransparentOutline");
            ltsltto     = Shader.Find("Hidden/" + shaderName + "/Lite/TwoPassTransparentOutline");
        }
    }
}

#endif
