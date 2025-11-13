#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace lilToon
{
    public class FakeArmInspector : lilToonInspector
    {
        // Custom properties
        MaterialProperty enableFakeArm;
        MaterialProperty boneLength;
        MaterialProperty extraForearmLength;
        MaterialProperty extraGrabRatio;
        MaterialProperty shaderIKTargetLightIntensity;
        MaterialProperty vertexScale;
        MaterialProperty isLeftArm;
        
        private static bool isShowCustomProperties;
        private const string shaderName = "lilToonFakeArm";
        
        protected override void LoadCustomProperties(MaterialProperty[] props, Material material)
        {
            isCustomShader = true;
            ReplaceToCustomShaders();
            isShowRenderMode = false; // Fake arm should only use ForwardBase
            
            enableFakeArm = FindProperty("_EnableFakeArm", props);
            boneLength = FindProperty("_BoneLength", props);
            extraForearmLength = FindProperty("_ExtraForearmLength", props);
            extraGrabRatio = FindProperty("_ExtraGrabRatio", props);
            shaderIKTargetLightIntensity = FindProperty("_ShaderIKTargetLightIntensity", props);
            vertexScale = FindProperty("_VertexScale", props);
            isLeftArm = FindProperty("_IsLeftArm", props);
        }
        
        protected override void DrawCustomProperties(Material material)
        {
            isShowCustomProperties = Foldout("Handholding Shader IK - Fake Arm", "Handholding Shader IK - Fake Arm", isShowCustomProperties);
            if(isShowCustomProperties)
            {
                EditorGUILayout.BeginVertical(boxOuter);
                EditorGUILayout.LabelField("Handholding Shader IK - Fake Arm", customToggleFont);
                EditorGUILayout.BeginVertical(boxInnerHalf);
                
                if (enableFakeArm != null)
                    m_MaterialEditor.ShaderProperty(enableFakeArm, "Enable Fake Arm");
                if (isLeftArm != null)
                    m_MaterialEditor.ShaderProperty(isLeftArm, "Is Left Arm");
                    
                DrawLine();
                
                if (boneLength != null)
                    m_MaterialEditor.ShaderProperty(boneLength, "Bone Length");
                if (extraForearmLength != null)
                    m_MaterialEditor.ShaderProperty(extraForearmLength, "Extra Forearm Length");
                if (extraGrabRatio != null)
                    m_MaterialEditor.ShaderProperty(extraGrabRatio, "Extra Grab Ratio");
                    
                DrawLine();
                
                if (shaderIKTargetLightIntensity != null)
                    m_MaterialEditor.ShaderProperty(shaderIKTargetLightIntensity, "Target Light Intensity");
                if (vertexScale != null)
                    m_MaterialEditor.ShaderProperty(vertexScale, "Vertex Scale");
                    
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndVertical();
            }
        }
        
        protected override void ReplaceToCustomShaders()
        {
            lts = Shader.Find(shaderName + "/lilToon");
            ltsl = Shader.Find(shaderName + "/lilToonLite");
            // Only need ForwardBase pass for fake arm - other variants set to null
            ltsc = null;
            ltst = null;
            ltsot = null;
            ltstt = null;
            ltso = null;
            ltsco = null;
            ltsto = null;
            ltsoto = null;
            ltstto = null;
        }
    }
}

#endif
