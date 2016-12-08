using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneApp.Modules.Styles.Models.CSSAttributes
{
    public class ColorAttribute : CSSAttributeDTO
    {
        public ColorAttribute()
        {
            this.CSSProperty = CSSProperty.Color;
            this.Name = "Text Color";
            this.CSSValueType = CSSValueType.Color;
        }
        public override string Format(string baseUrl, int ruleId)
        {
            return string.IsNullOrEmpty(Value) ? string.Empty : ("color: " + Value + Important + ";");

        }
    }
}