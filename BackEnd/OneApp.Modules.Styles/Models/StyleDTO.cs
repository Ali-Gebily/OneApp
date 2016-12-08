using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace OneApp.Modules.Styles.Models
{
    public class StyleDTO
    {
        const string _color = "color";
        const string _backgroundColor = "background_color";
        const string _backgroundImage = "background_image";



        [JsonProperty(_color)]
        public string Color { get; set; }

        [JsonProperty(_backgroundColor)]
        public string BackgroundColor { get; set; }

        [JsonProperty(_backgroundImage)]
        public string BackgroundImage { get; set; }


        public string GetFormattedStyle(string baseUrl, int ruleId)
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(Color))
            {
                sb.Append("color: " + Color + " !important; \n");
            }
            if (!string.IsNullOrEmpty(BackgroundColor))
            {
                sb.Append("background-color: " + BackgroundColor + " !important; \n");
            }

            if (!string.IsNullOrEmpty(BackgroundImage))
            {
                sb.Append("background-image: url('" + baseUrl + "/" + BackgroundImage + "') !important; \n");

            }

            return sb.ToString();
        }

        public void SetProperty(string propertyName, string value)
        {
            switch (propertyName)
            {
                case _color:
                    this.Color = value;
                    break;
                case _backgroundColor:
                    this.BackgroundColor = value;
                    break;
                case _backgroundImage:
                    this.BackgroundImage = value;
                    break;
                default:
                    throw new NotImplementedException("can not set property with key=" + propertyName);
            }
        }
        public List<string> GetFilePropertiesValues()
        {
            var values = new List<string>();
            if (!string.IsNullOrEmpty(BackgroundImage))
            {
                values.Add(BackgroundImage);
            }
            return values;
        }
    }
}