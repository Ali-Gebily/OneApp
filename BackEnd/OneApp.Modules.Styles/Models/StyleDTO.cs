using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using OneApp.Modules.Styles.Models.CSSAttributes;

namespace OneApp.Modules.Styles.Models
{
    public class StyleDTO
    {
        [JsonProperty("color")]
        public ColorAttribute Color { get; set; }

        [JsonProperty("background_color")]
        public BackgroundColorAttribute BackgroundColor { get; set; }

        [JsonProperty("background_image")]
        public BackgroundImageAttribute BackgroundImage { get; set; }


        public string GetFormattedStyle(string baseUrl, int ruleId)
        {

            var sb = new StringBuilder();
            if (Color != null)
            {
                sb.Append(Color.Format(baseUrl, ruleId) + "\n");
            }

            if (BackgroundColor != null)
            {
                sb.Append(BackgroundColor.Format(baseUrl, ruleId) + "\n");
            }

            if (BackgroundImage != null)
            {
                sb.Append(BackgroundImage.Format(baseUrl, ruleId) + "\n");
            }

            return sb.ToString();
        }

        public CSSAttributeDTO GetProperty(CSSProperty p)
        {
            switch (p)
            {
                case CSSProperty.Color:
                    return Color;
                case CSSProperty.BackgroundColor:
                    return BackgroundColor;
                case CSSProperty.BackgroundImage:
                    return BackgroundImage;
                default:
                    throw new NotImplementedException(p.ToString());
            }
        }
        public List<CSSAttributeDTO> GetFileProperties()
        {
            var properties = new List<CSSAttributeDTO>();

            properties.Add(BackgroundImage);
            return properties;
        }
    }
}