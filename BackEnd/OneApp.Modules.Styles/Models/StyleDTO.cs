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

 
        /// <summary> for color value properties:
        /// (null)  means that it's not included in style
        /// ("") included but not set 
        /// any other string means it's included and has value,
        /// color value format should be like rgba(r,g,b,a)
        /// </summary>
        [JsonProperty(_color)]
        public string Color { get; set; }

        [JsonProperty(_backgroundColor)]
        public string BackgroundColor { get; set; }

        /// <summary>
        /// (null)  means that it's not included in style
        /// (0) included but not set 
        /// >0 means it's included and has value
        /// </summary>
        [JsonProperty(_backgroundImage)]
        public int? BackgroundImage { get; set; }

 
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

            if (BackgroundImage > 0)
            {
                sb.Append("background-image: url('" + baseUrl + "/" + BackgroundImage + "') !important; \n");

            }

            return sb.ToString();
        }

        public void SetFilePropertyWithId(string propertyName, int value)
        {
            switch (propertyName)
            {

                case _backgroundImage:
                    this.BackgroundImage = value;
                    break;
                default:
                    throw new NotImplementedException("can not set property with key=" + propertyName);
            }
        }
        public List<int> GetFilePropertiesValues()
        {
            var values = new List<int>();
            if (BackgroundImage > 0)
            {
                values.Add(BackgroundImage.Value);
            }
            return values;
        }
    }
}