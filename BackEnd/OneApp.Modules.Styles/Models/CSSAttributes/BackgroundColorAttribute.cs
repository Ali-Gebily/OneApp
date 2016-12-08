using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneApp.Modules.Styles.Models.CSSAttributes
{
    public class BackgroundColorAttribute : ColorAttribute
    {
        public BackgroundColorAttribute()
        {
            this.Name = "Background Color";
            this.CSSProperty = CSSProperty.BackgroundColor;
        }
        public override string Format(string baseUrl, int ruleId)
        {
            return string.IsNullOrEmpty(Value) ? string.Empty :( "background-color: " + Value + Important + ";");
        }
    }
}