using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Formatters.Json.Internal;
using Newtonsoft.Json;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CRM.Core.Helpers
{
    /// <summary>
    /// 2020.02.20      Rui     统一处理 webapi 返回null 转为“”帮助类
    /// </summary>
    public class JsonOutputFormatterHelper : JsonOutputFormatter
    {
        public JsonOutputFormatterHelper(JsonSerializerSettings serializerSettings)
            : base(serializerSettings, ArrayPool<char>.Shared)
        {

        }

        public new JsonSerializerSettings SerializerSettings => base.SerializerSettings;

        protected override JsonWriter CreateJsonWriter(TextWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException(nameof(writer));
            }
            var jsonWriter = new NullJsonWriter(writer)
            {
                ArrayPool = new JsonArrayPool<char>(ArrayPool<char>.Shared),
                CloseOutput = false,
                AutoCompleteOnClose = false
            };
            return jsonWriter;
        }
    }

    public class NullJsonWriter : JsonTextWriter
    {
        public NullJsonWriter(TextWriter textWriter)
            : base(textWriter)
        {

        }

        public override void WriteNull()
        {
            this.WriteValue(String.Empty);
        }
    }
}
