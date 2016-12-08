using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneApp.Modules.Styles.Models.CSSAttributes
{
    public class BackgroundImageAttribute : CSSAttributeDTO
    {
        public BackgroundImageAttribute()
        {
            this.Name = "Background Image";
            this.CSSProperty = CSSProperty.BackgroundImage;
            this.CSSValueType = CSSValueType.File;
        }

        public override string Format(string baseUrl, int ruleId)
        {
            return string.IsNullOrEmpty(Value) ? string.Empty : ("background-image: url('" + baseUrl + "/" + Value + "')" + Important + "; " );
        }

    }
}