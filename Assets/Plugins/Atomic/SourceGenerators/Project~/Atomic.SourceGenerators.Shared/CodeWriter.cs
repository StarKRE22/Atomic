using System.Text;

namespace Atomic.SourceGenerators.Shared
{
    /// <summary>
    /// Fluent code writer with automatic indentation and scope management.
    /// Mirrors Unity.Entities.SourceGen.Aspect.Printer approach.
    /// </summary>
    internal ref struct CodeWriter
    {
        readonly StringBuilder _sb;
        int _indent;

        public CodeWriter()
        {
            _sb = new StringBuilder(1024);
            _indent = 0;
        }

        /// <summary>Increases indentation by one level.</summary>
        public CodeWriter Indent() { _indent++; return this; }

        /// <summary>Decreases indentation by one level.</summary>
        public CodeWriter Dedent() { _indent--; return this; }

        /// <summary>Appends the current indent (tabs).</summary>
        CodeWriter WriteIndent()
        {
            for (int i = 0; i < _indent; i++)
                _sb.Append('\t');
            return this;
        }

        /// <summary>Writes indent + text + newline.</summary>
        public CodeWriter Line(string text)
        {
            WriteIndent();
            _sb.AppendLine(text);
            return this;
        }

        /// <summary>Writes a blank line (indent + empty + newline).</summary>
        public CodeWriter Line()
        {
            WriteIndent();
            _sb.AppendLine();
            return this;
        }

        /// <summary>Writes scope open (e.g. <c>"{"</c>) on its own line and increases indent.</summary>
        public CodeWriter Open(string text = "{")
        {
            Line(text);
            _indent++;
            return this;
        }

        /// <summary>Decreases indent and writes scope close (e.g. <c>"}"</c>) on its own line.</summary>
        public CodeWriter Close(string text = "}")
        {
            _indent--;
            Line(text);
            return this;
        }

        /// <summary>Appends text without indent or newline.</summary>
        public CodeWriter Raw(string text)
        {
            _sb.Append(text);
            return this;
        }

        /// <summary>Appends text + newline without indent.</summary>
        public CodeWriter RawLine(string text)
        {
            _sb.AppendLine(text);
            return this;
        }

        /// <summary>Returns the complete generated source code.</summary>
        public readonly string Result => _sb.ToString();
    }
}
