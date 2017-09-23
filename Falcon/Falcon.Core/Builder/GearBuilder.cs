using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Falcon.Core.Builder
{
    public class GearBuilder
    {
        private readonly StringBuilder _builder;
        private int _indentLength;
        private string _lineIndent;

        public GearBuilder()
        {
            _builder = new StringBuilder();
            _indentLength = 0;
            _lineIndent = string.Empty;
        }

        internal GearBuilder AddCommand(string value)
        {
            _builder.AppendLine($"{_lineIndent}{value}");
            return this;
        }

        internal GearBuilder NewLine()
        {
            _builder.AppendLine();
            return this;
        }

        internal GearBuilder Append(string value)
        {
            _builder.Append(value);
            return this;
        }

        internal GearBuilder IncreaseIndent()
        {
            _indentLength += 3;
            _lineIndent = new string(' ', _indentLength);

            return this;
        }

        internal GearBuilder DecreaseIndent()
        {
            _indentLength = _indentLength >= 3 ? _indentLength - 3 : 0;
            _lineIndent = new string(' ', _indentLength);

            return this;
        }

        public GearBuilder ApplyForEach<T>(Func<GearBuilder, T, GearBuilder> funcToApply, IEnumerable<T> collection)
        {
            return collection.Aggregate(this, funcToApply);
        }

        internal GearBuilder AddCommandWithParameters(string command, params object[] parameters)
        {
            if (parameters == null || parameters.Length == 0 || parameters.Any(param => param == null))
                return this;

            if (parameters.Any(o => string.IsNullOrEmpty(o.ToString())))
                return this;

            return AddCommand(string.Format(command, parameters));
        }

        public string CreateOutput()
        {
            return _builder.ToString();
        }
    }
}