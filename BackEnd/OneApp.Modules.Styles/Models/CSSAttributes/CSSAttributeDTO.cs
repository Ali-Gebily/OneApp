using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace OneApp.Modules.Styles.Models.CSSAttributes
{
    // [JsonConverter(typeof(JsonCSSAttributeConverter))]
    //we will use composition instead of inheritance
    public abstract class CSSAttributeDTO
    {
        [JsonProperty("css_property")]
        public CSSProperty CSSProperty { get; protected set; }//used as Id when uploading file


        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("css_value_type")]
        public CSSValueType CSSValueType { get; protected set; }//used as Id when uploading file



        [JsonIgnore]
        public bool IsImportant { get; set; }

        public abstract string Format(string baseUrl, int ruleId);

        protected string Important
            {
            get {return IsImportant ? " !important" : string.Empty; }
            }
    }
    //public abstract class JsonCreationConverter<T> : CustomCreationConverter<T>
    // {
    //     /// <summary>
    //     /// Create an instance of objectType, based properties in the JSON object
    //     /// </summary>
    //     /// <param name="objectType">type of object expected</param>
    //     /// <param name="jObject">contents of JSON object that will be deserialized</param>
    //     /// <returns></returns>
    //     protected abstract T Create(Type objectType, JObject jObject);

    //     public override bool CanConvert(Type objectType)
    //     {
    //         return typeof(T).IsAssignableFrom(objectType);
    //     }

    //     public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    //     {
    //         // Load JObject from stream
    //         JObject jObject = JObject.Load(reader);

    //         // Create target object based on JObject
    //         T target = Create(objectType, jObject);

    //         // Populate the object properties
    //         serializer.Populate(jObject.CreateReader(), target);

    //         return target;
    //     }

    // }
    // public class JsonCSSAttributeConverter : JsonCreationConverter<CSSAttributeDTO>
    // {
    //     public override CSSAttributeDTO Create(Type objectType)
    //     {
    //         throw new NotImplementedException();    
    //     }
    //     protected override CSSAttributeDTO Create(Type objectType, JObject jObject)
    //     {
    //         var type = (CSSProperty)(int)jObject.Property("css_property");
    //         switch (type)
    //         {
    //             case CSSProperty.Color:
    //                 return new ColorAttribute();
    //             case CSSProperty.BackgroundColor:
    //                 return new BackgroundColorAttribute();
    //             case CSSProperty.BackgroundImage:
    //                 return new BackgroundImageAttribute();
    //             default:
    //                 throw new NotImplementedException(type.ToString());
    //         }
    //     }
    // }


}