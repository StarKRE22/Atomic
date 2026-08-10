using System;
using System.IO;

namespace Atomic.SourceGenerators.Shared
{
    /// <summary>
    /// Helper for writing generated source files to <c>Temp/GeneratedCode/</c> for debugging/inspection.
    /// Activated by adding <c>ATOMIC_OUTPUT_SOURCEGEN_FILES</c> to Unity's Scripting Define Symbols.
    /// Mirrors Unity.Entities.SourceGen.Common.SourceOutputHelpers approach.
    /// </summary>
    internal static class SourceOutputHelpers
    {
        static bool s_OutputSourceGenFiles;

        /// <summary>
        /// Must be called once per generation pass with the current parse options.
        /// Reads <c>ATOMIC_OUTPUT_SOURCEGEN_FILES</c> preprocessor symbol.
        /// </summary>
        public static void Setup(Microsoft.CodeAnalysis.ParseOptions parseOptions)
        {
            s_OutputSourceGenFiles = false;
            foreach (var symbolName in parseOptions.PreprocessorSymbolNames)
            {
                if (symbolName == "ATOMIC_OUTPUT_SOURCEGEN_FILES")
                {
                    s_OutputSourceGenFiles = true;
                    break;
                }
            }
        }

        /// <summary>
        /// Writes generated source text to <c>Temp/GeneratedCode/{assemblyName}/{fileName}</c>.
        /// Silently ignores IO errors (non-critical, debug-only).
        /// </summary>
        public static void OutputSourceToFile(string assemblyName, string fileName, Func<string> sourceTextProvider)
        {
            if (!s_OutputSourceGenFiles)
                return;

            try
            {
                var generatedCodePath = GetGeneratedCodePath(assemblyName);
                var filePath = Path.Combine(generatedCodePath, fileName);
                File.WriteAllText(filePath, sourceTextProvider());
            }
            catch (IOException)
            {
                // Non-critical, debug-only output — ignore silently
            }
            catch (UnauthorizedAccessException)
            {
                // Non-critical, debug-only output — ignore silently
            }
        }

        /// <summary>
        /// Appends a line to <c>Temp/GeneratedCode/SourceGen.log</c>.
        /// </summary>
        public static void LogInfo(string message)
        {
            if (!s_OutputSourceGenFiles)
                return;

            try
            {
                var logPath = Path.Combine(GetGeneratedCodeBasePath(), "SourceGen.log");
                var dir = Path.GetDirectoryName(logPath);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                using var writer = File.AppendText(logPath);
                writer.WriteLine(message);
            }
            catch (IOException)
            {
                // Non-critical, debug-only — ignore silently
            }
        }

        static string GetGeneratedCodePath(string assemblyName)
        {
            var basePath = GetGeneratedCodeBasePath();
            var assemblyDir = Path.Combine(basePath, assemblyName);
            if (!Directory.Exists(assemblyDir))
                Directory.CreateDirectory(assemblyDir);
            return assemblyDir;
        }

        static string GetGeneratedCodeBasePath()
        {
            // When running as a Unity source generator, the working directory
            // is the Unity project root, so Temp/GeneratedCode/ resolves correctly.
            return Path.Combine("Temp", "GeneratedCode");
        }
    }
}
