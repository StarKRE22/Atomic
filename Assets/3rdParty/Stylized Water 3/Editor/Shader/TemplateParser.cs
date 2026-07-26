// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

using System;
using System.IO;
using System.Text;
using UnityEditor;

namespace StylizedWater3
{
    public static class TemplateParser
    {
        //Converts relative include paths such as (../../Libraries/File.hlsl) to an absolute path
        //Supports the source file being part of a package
        public static string RelativeToAbsoluteIncludePath(string filePath, string relativePath)
        {
            string fileDir = Path.GetDirectoryName(filePath);

            //Count how many folders should be traversed up
            int levels = relativePath.Split(new[]
            {
                ".."
            }, StringSplitOptions.None).Length - 1;

            string traveledPath = fileDir;
            if (levels > 0)
            {
                for (int i = 0; i < levels; i++)
                {
                    //Remove the number of needed sub-directories needed to reach the destination
                    int strimStart = traveledPath.LastIndexOf(Path.DirectorySeparatorChar);
                    traveledPath = traveledPath.Remove(strimStart);
                }
            }

            //The directory without the "up" navigators
            string relativeFolder = relativePath.Replace("../", string.Empty);

            //Concatenate them together
            string absolutePath = traveledPath + "/" + relativeFolder;

            //Convert back- to forward slashes
            absolutePath = absolutePath.Replace("\\", "/");

            return absolutePath;
        }

        //Pre-process the template to inject additional template contents into it
        private static void ModifyTemplate(ref string[] lines, WaterShaderImporter importer)
        {
            StringBuilder templateBuilder = new StringBuilder();

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                //Inject additional passes into template
                if (line.Contains("%additional_passes%"))
                {
                    for (int j = 0; j < importer.settings.additionalPasses.Length; j++)
                    {
                        if (importer.settings.additionalPasses[j] != null)
                        {
                            string filePath = AssetDatabase.GetAssetPath(importer.settings.additionalPasses[j]);

                            importer.RegisterDependency(filePath);
                            
                            string[] passContexts = File.ReadAllLines(filePath);

                            for (int k = 0; k < passContexts.Length; k++)
                            {
                                templateBuilder.AppendLine(passContexts[k]);
                            }
                        }
                    }
                    
                    continue;
                }
                
                templateBuilder.AppendLine(lines[i]);
            }
            lines = templateBuilder.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        }
        
        public static string CreateShaderCode(string templatePath, ref string[] lines, WaterShaderImporter importer, bool tessellation = false)
        {
            if (importer == null)
            {
                throw new Exception("Failed to compile shader from template code. The importer is invalid, this should not even be possible. Whatever you did, undo it...");
            }
            
            //Extension installation states
            var underwaterInstalled = StylizedWaterEditor.UnderwaterRenderingInstalled();
            var dynamicEffectsInstalled = StylizedWaterEditor.DynamicEffectsInstalled();

            FogIntegration.Integration fogIntegration = importer.settings.autoIntegration ? FogIntegration.GetFirstInstalled() : FogIntegration.GetIntegration(importer.settings.fogIntegration);

            AssetInfo.VersionChecking.CheckUnityVersion();
            
            //Shader name
            string prefix = importer.settings.hidden ? "Hidden/" : string.Empty;
            string suffix = tessellation ? ShaderParams.ShaderNames.TESSELLATION_NAME_SUFFIX : string.Empty;
            string shaderName = $"{prefix}{AssetInfo.ASSET_NAME}/{importer.settings.shaderName}";

            ModifyTemplate(ref lines, importer);
            
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < lines.Length; i++)
            {
                //Ignore blank lines and comments for analysis
                if (string.IsNullOrWhiteSpace(lines[i]) || lines[i].StartsWith("//"))
                {
                    sb.AppendLine(lines[i]);
                    continue;
                }

                //First non-space character
                int indent = System.Text.RegularExpressions.Regex.Match(lines[i], "[^-\\s]").Index;

                string whitespace = lines[i].Replace(lines[i].Substring(indent), "");

                //AppendLine using previous line's white spacing
                void AddLine(string source)
                {
                    sb.AppendLine(source.Insert(0, whitespace));
                }

                //Remove whitespaces
                string line = lines[i].Remove(0, indent);

                bool Matches(string source) { return string.CompareOrdinal(source, line) == 0; }

                if (Matches("%asset_version%"))
                {
                    AddLine($"//Asset version {AssetInfo.INSTALLED_VERSION}");
                    continue;
                }

                if (Matches("%compiler_version%"))
                {
                    AddLine($"//Shader generator version: {new Version(AssetInfo.SHADER_GENERATOR_VERSION_MAJOR, AssetInfo.SHADER_GENERATOR_MINOR, AssetInfo.SHADER_GENERATOR_PATCH)}");
                    continue;
                }
                
                if (Matches("%unity_version%"))
                {
                    AddLine($"//Unity version: {AssetInfo.VersionChecking.GetUnityVersion()}");
                    continue;
                }

                if (Matches("%shader_name%"))
                {
                    AddLine($"Shader \"{shaderName}{suffix}\"");
                    continue;
                }

                if (Matches("%custom_directives%"))
                {
                    foreach (WaterShaderImporter.Directive directive in importer.settings.customIncludeDirectives)
                    {
                        if(directive.enabled == false) continue;
                        
                        string directivePrefix = string.Empty;

                        switch (directive.type)
                        {
                            case WaterShaderImporter.Directive.Type.define:
                                directivePrefix = "#define ";
                                break;
                            case WaterShaderImporter.Directive.Type.include:
                                directivePrefix = "#include ";
                                break;
                            case WaterShaderImporter.Directive.Type.pragma:
                                directivePrefix = "#pragma ";
                                break;
                        }

                        if (directive.value != string.Empty) AddLine($"{directivePrefix}{directive.value}");
                    }
                    continue;
                }

                if (Matches("%pragma_target%"))
                {
                    if (tessellation)
                    {
                        AddLine("#pragma target 4.6");
                    }
                    else
                    {
                        AddLine("#pragma target 3.0");
                    }

                    continue;
                }

                if (Matches("%pragma_renderers%"))
                {
                    if (tessellation)
                    {
                        AddLine("#pragma exclude_renderers gles");
                    }
                    
                    continue;
                }

                if (line.StartsWith("Fallback"))
                {
                    if (tessellation)
                    {
                        //Fallback to non-tessellation variant (with without suffix)
                        AddLine($"Fallback \"{shaderName}\"");
                        //Test, disable fallback
                        //AddLine(line);
                    }
                    else
                    {
                        //Leave as is
                        AddLine(line);
                    }

                    continue;
                }

                if (importer.settings.type == WaterShaderImporter.WaterShaderSettings.ShaderType.WaterSurface)
                {
                    if (Matches("%tessellation_properties%"))
                    {
                        if (tessellation)
                        {
                            AddLine("_TessValue(\"Max subdivisions\", Range(1, 64)) = 16");
                            AddLine("_TessMin(\"Start Distance\", Float) = 0");
                            AddLine("_TessMax(\"End Distance\", Float) = 15");
                        }

                        continue;
                    }

                    if (Matches("%tessellation_directives%"))
                    {
                        if (tessellation)
                        {
                            AddLine("#define TESSELLATION_ON");
                            AddLine("#pragma require tessellation tessHW");
                            AddLine("#pragma hull Hull");
                            AddLine("#pragma domain Domain");
                        }

                        continue;
                    }
                    
                    if (line.Contains("%render_queue_offset%"))
                    {
                        int offset = 0;

                        switch (fogIntegration.asset)
                        {
                            case FogIntegration.Assets.COZY: offset = 2;
                                break;
                            //case Fog.Assets.AtmosphericHeightFog : offset = 2; //Should actually render after the fog sphere, but asset inherently relies on double fog shading it seems?
                                //break;
                            default: offset = 0;
                                break;
                        }
                        
                        line = line.Replace("%render_queue_offset%", offset.ToString());
                        AddLine(line);

                        continue;
                    }

                    if (Matches("%stencil%"))
                    {
                        if (importer.settings.fogIntegration == FogIntegration.Assets.COZY)
                        {
                            AddLine("Stencil { Ref 221 Comp Always Pass Replace }");
                        }

                        continue;
                    }
                    
                    if (Matches("%multi_compile_light_cookies%"))
                    {
                        if (importer.settings.lightCookies)
                        {
                            AddLine("#pragma multi_compile_fragment _ _LIGHT_COOKIES");
                        }
                        
                        continue;
                    }
                }

                if (Matches("%defines%"))
                {
                    if (importer.settings.additionalLightCaustics)
                    {
                        AddLine("#define _ADDITIONAL_LIGHT_CAUSTICS");
                    }
                    if (importer.settings.additionalLightTranslucency)
                    {
                        AddLine("#define _ADDITIONAL_LIGHT_TRANSLUCENCY");
                    }
                    
                    continue;
                }

                if (Matches("%multi_compile underwater rendering%"))
                {
                    if (underwaterInstalled)
                    {
                        AddLine($"#pragma multi_compile_fragment _ {ShaderParams.Keywords.UnderwaterRendering}");
                    }

                    continue;
                }
                
                if (Matches("%multi_compile dynamic effects%"))
                {
                    if (dynamicEffectsInstalled)
                    {
                        AddLine($"#pragma multi_compile _ {ShaderParams.Keywords.DynamicEffects}");
                    }

                    continue;
                }
                
                if (line.StartsWith("#include "))
                {
                    string includePath = line.Replace("#include ", string.Empty);
                    //Remove parenthesis
                    includePath = includePath.Replace("\"", string.Empty);

                    importer.RegisterDependency(includePath);
                }

                if (importer.settings.type == WaterShaderImporter.WaterShaderSettings.ShaderType.WaterSurface)
                {
                    if (Matches("%define_fog_integration%"))
                    {
                        AddLine($"#define {fogIntegration.asset.ToString()}");
                        
                        if (fogIntegration.asset == FogIntegration.Assets.UnityFog)
                        {
                            AddLine("#pragma multi_compile_fog");
                        }
                        
                        continue;
                    }

                    /* include FogLibrary */
                    if (Matches("%include_fog_integration_library%"))
                    {
                        //Default until otherwise valid
                        line = string.Empty;

                        if (fogIntegration.asset != FogIntegration.Assets.None && fogIntegration.asset != FogIntegration.Assets.UnityFog)
                        {
                            string includePath = AssetDatabase.GUIDToAssetPath(fogIntegration.libraryGUID);

                            importer.RegisterDependency(includePath);

                            //Not found error
                            if (includePath == string.Empty)
                            {
                                if (EditorUtility.DisplayDialog(AssetInfo.ASSET_NAME,
                                    fogIntegration.name + " fog shader library could not be found with the GUID \"" + fogIntegration.libraryGUID + "\".\n\n" +
                                    "This means it was changed by the author (rare!), you deleted the \".meta\" file at some point, or the asset simply isn't installed.", "Ok"))
                                {

                                }
                            }
                            else
                            {
                                line = $"#include \"{includePath}\"";

                                AddLine(line);
                                continue;
                            }
                        }
                    }
                }

                //Shaders created using "ShaderUtil.CreateShaderAsset" don't exist in a literal sense. Hence any relative file paths are invalid
                //Convert them to absolute file paths
                //Bonus: moving a shader file (or its folder) triggers it to re-import, thus always keeping the file path up-to-date
                if (line.StartsWith("#include_library"))
                {
                    string relativePath = line.Replace("#include_library ", string.Empty);
                    //Remove parenthesis
                    relativePath = relativePath.Replace("\"", string.Empty);

                    string includePath = RelativeToAbsoluteIncludePath(templatePath, relativePath);

                    line = $"#include \"{includePath}\"";

                    importer.RegisterDependency(includePath);

                    AddLine(line);
                    continue;
                }

                //Insert whitespace back in
                line = line.Insert(0, whitespace);

                //Nothing special, keep whatever line this is
                sb.AppendLine(line);
            }

            //Convert to separate lines again
            lines = sb.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            //Convert to single string, respecting line breaks and spacing.
            return String.Join(Environment.NewLine, lines);
        }
    }
}