// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

#if SWS_DEV
#define ENABLE_SHADER_STRIPPING_LOG
#endif

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;
#if URP
using UnityEngine.Rendering.Universal;
#endif

namespace StylizedWater3
{
    public static class ShaderConfigurator
    {
        public static ShaderMessage[] GetErrorMessages(Shader shader)
        {
            ShaderMessage[] messages = null;

            int n = ShaderUtil.GetShaderMessageCount(shader);

            if (n < 1) return messages;
            
            messages = ShaderUtil.GetShaderMessages(shader);
            
            //Filter for errors
            messages = messages.Where(x => x.severity == ShaderCompilerMessageSeverity.Error).ToArray();

            return messages;
        }

        #if URP
        //Strips keywords from the shader for features belonging to newer URP versions.
        private class KeywordStripper : IPreprocessShaders
        {
            private const string LOG_FILEPATH = "Logs/Stylized Water 3 Compilation.log";

		    #if ENABLE_SHADER_STRIPPING_LOG
            private System.Diagnostics.Stopwatch m_stripTimer = new System.Diagnostics.Stopwatch();
            #endif
            
            private List<ShaderKeyword> StrippedKeywords = new List<ShaderKeyword>();

            //URP 18+
            private readonly ShaderKeyword _TEST = new ShaderKeyword("_TEST");

            private readonly bool heightPrePassEnabled;

            #if SWS_DEV
            [MenuItem("SWS/Debug/HeightPrePassEnabled")]
            #endif
            private static bool HeightPrePassEnabled()
            {
                bool state = false;

                if (StylizedWaterRenderFeature.RequireHeightPrePass) return true;
                
                //Check if the displacement pre-pass is enabled anywhere
                for (int i = 0; i < GraphicsSettings.allConfiguredRenderPipelines.Length; i++)
                {
                    UniversalRenderPipelineAsset pipeline = (UniversalRenderPipelineAsset)GraphicsSettings.allConfiguredRenderPipelines[i];

                    ScriptableRendererData[] renderers = PipelineUtilities.GetRenderDataList(pipeline);

                    for (int j = 0; j < renderers.Length; j++)
                    {
                        StylizedWaterRenderFeature renderFeature = (StylizedWaterRenderFeature)PipelineUtilities.GetRenderFeature<StylizedWaterRenderFeature>(renderers[j]);

                        if (renderFeature)
                        {
                            state |= renderFeature.heightPrePassSettings.enable;
                            
                            #if SWS_DEV
                            //Debug.Log($"{renderers[j].name}. Enable?:{renderFeature.heightPrePassSettings.enable}");
                            #endif
                        }
                    }
                }

                #if SWS_DEV
                //Debug.Log("Height pre-pass enabled somewhere: " + state);
                #endif

                return state;
            }
            
            //Note: Constructor is called once, when building starts
            public KeywordStripper()
            {
                if (PlayerSettings.GetGraphicsAPIs(EditorUserBuildSettings.activeBuildTarget)[0] == GraphicsDeviceType.OpenGLES3)
                {
                    if (UniversalRenderPipeline.asset.msaaSampleCount > 1)
                    {
                        Debug.LogWarning("[Stylized Water 3] You are deploying a build using the OpenGLES 3.0 graphics API with MSAA enabled (in your URP pipeline asset). Due to a bug in some graphics chips, transparent materials (including the water) will not render. " +
                                         "Disable MSAA, or use the Vulkan graphics API", UniversalRenderPipeline.asset);
                    }
                }

                //Check if the displacement pre-pass is enabled anywhere
                heightPrePassEnabled = HeightPrePassEnabled();
                
                StrippedKeywords.Clear();

                if (!heightPrePassEnabled)
                {
                    StrippedKeywords.Add(new ShaderKeyword(ShaderParams.Keywords.WaterHeightPass));
                }
                
                //Note: Keywords for extensions are only injected through the shader generator. Hence they don't need to be stripped
                
                #if !UNITY_6000_0_OR_NEWER
                StrippedKeywords.Add(_TEST);
                #endif

                LogInitialization();
            }
            
            public int callbackOrder => 0;

            public void OnProcessShader(Shader shader, ShaderSnippetData snippet, IList<ShaderCompilerData> compilerDataList)
            {
			    #if URP
                if (UniversalRenderPipeline.asset == null || compilerDataList == null || compilerDataList.Count == 0) return;

                //Only run for specific shaders
                if (shader.name.Contains("Stylized Water 3") == false) return;

                LogStart(shader, snippet, compilerDataList);

                var inputShaderVariantCount = compilerDataList.Count;
                for (int i = 0; i < inputShaderVariantCount;)
                {
                    //If any of the excluded keywords are enabled in this variant, strip it
                    bool removeInput = StripUnused(shader, compilerDataList[i], snippet);

                    // Remove at swap back
                    if (removeInput)
                        compilerDataList[i] = compilerDataList[--inputShaderVariantCount];
                    else
                        ++i;
                }

                if (compilerDataList is List<ShaderCompilerData> inputDataList)
                {
                    inputDataList.RemoveRange(inputShaderVariantCount, inputDataList.Count - inputShaderVariantCount);
                }
                else
                {
                    for (int i = compilerDataList.Count - 1; i >= inputShaderVariantCount; --i)
                        compilerDataList.RemoveAt(i);
                }

                LogStrippingEnd(compilerDataList.Count);
			    #endif
            }
            
            private bool StripUnused(Shader shader, ShaderCompilerData compilerData, ShaderSnippetData snippet)
            {
                if (StripPass(shader, snippet))
                {
                    return true;
                }
                
                foreach (var keyword in StrippedKeywords)
                {
                    if (StripKeyword(shader, keyword, compilerData, snippet))
                    {
                        return true;
                    }
                }

                return false;
            }

            private bool StripPass(Shader shader, ShaderSnippetData snippet)
            {
                if (heightPrePassEnabled == false && snippet.passName == ShaderParams.Passes.HeightPrePass)
                {
                    Log($"- Stripped Pass {snippet.passName} ({shader.name}) (Stage: {snippet.shaderType})");
                    
                    return true;
                }

                return false;
            }
            
            private bool StripKeyword(Shader shader, ShaderKeyword keyword, ShaderCompilerData compilerData, ShaderSnippetData snippet)
            {
                if (compilerData.shaderKeywordSet.IsEnabled(keyword))
                {
                    LogStripping(shader, keyword, snippet);
                    return true;
                }

                return false;
            }

            #region Logging
            struct StrippingLog
            {
                public Shader shader;
                public ShaderKeyword keyword;
                public string passName;
                public ShaderType shaderType;
            }

            private void LogInitialization()
            {
                #if ENABLE_SHADER_STRIPPING_LOG
			    //Clear log file first
			    File.WriteAllLines(LOG_FILEPATH, new string[] {});
                
                Log("KeywordStripper initialized...", true);
                
                Log(string.Empty);

                Log($"Displacement Pre-pass enabled in build: {heightPrePassEnabled}", true);

                Log(string.Empty);
                
                for (int i = 0; i < StrippedKeywords.Count; i++)
                {
                    Log($"• {StrippedKeywords[i].name} keyword to be stripped");
                }

                Log($"{StrippedKeywords.Count} total keywords to be stripped");
                #endif
            }

            private void LogStart(Shader shader, ShaderSnippetData snippet, IList<ShaderCompilerData> compilerDataList)
            {
                #if ENABLE_SHADER_STRIPPING_LOG
                m_stripTimer.Start();
                
                var text = $"OnProcessShader running for {shader.name}. (Pass: {snippet.passName}) (Stage: {snippet.shaderType}). Num variants: {compilerDataList.Count}";

                Log(text, true);
                #endif
            }

            StrippingLog prevLog;

            private void LogStripping(Shader shader, ShaderKeyword keyword, ShaderSnippetData snippet)
            {
                #if ENABLE_SHADER_STRIPPING_LOG
                
                //Try to avoid spamming the log with duplicates, this otherwise slows down compilation to a crawl
                if (prevLog.keyword.index == keyword.index && prevLog.shader == shader && prevLog.passName == snippet.passName && prevLog.shaderType == snippet.shaderType)
                {
                    //File.AppendAllText(LOG_FILEPATH, "- Skipping log!\n" );
                    return;
                }

                prevLog.shader = shader;
                prevLog.keyword = keyword;
                prevLog.passName = snippet.passName;
                prevLog.shaderType = snippet.shaderType;
                
                var text = $"- Stripped {keyword.name} ({shader.name}) variant. (Pass {snippet.passName}) (Stage: {snippet.shaderType})";

			    Log(text);
                #endif
            }

            private void LogStrippingEnd(int count)
            {
                #if ENABLE_SHADER_STRIPPING_LOG
                m_stripTimer.Stop();
                System.TimeSpan stripTimespan = m_stripTimer.Elapsed;
                
                var text = $"Stripping took {stripTimespan.Minutes}m{stripTimespan.Seconds}s ({stripTimespan.Milliseconds}ms). Remaining variants to compile: {count}";
                
			    Log(text);

                m_stripTimer.Reset();
                #endif
            }

            private void Log(string text, bool newLine = false)
            {
			    #if ENABLE_SHADER_STRIPPING_LOG
                File.AppendAllText(LOG_FILEPATH, (newLine ? "\n" : "") + text + "\n");
			    #endif
            }
            #endregion
        }
        #endif
    }
}