﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OneApp.Modules.Styles.Models;

namespace OneApp.Modules.Styles.Repositories.Mock.Models
{
    public class MockStyle : IStyleMapper
    {

        public MockStyle()
        {

        }
        public MockStyle(MockStyle old)
        {
            this.Color = old.Color;
            this.BackgroundColor = old.BackgroundColor;
            this.BackgroundImage = old.BackgroundImage;
        }

        public string Color { get; set; }

        public string BackgroundColor { get; set; }

        public int? BackgroundImage { get; set; }

        public StyleDTO GetStyleDTO()
        {
            var dto = new StyleDTO();

            dto.BackgroundColor = this.BackgroundColor;
            dto.BackgroundImage = this.BackgroundImage;
            dto.Color = this.Color;

            return dto;
        }
        public void SetStyleProperties(StyleDTO dto)
        {
            if (dto.Color != null)
            {
                this.Color = dto.Color;
            }
            if (dto.BackgroundColor != null)
            {
                this.BackgroundColor = dto.BackgroundColor;
            }
            if (dto.BackgroundImage.HasValue)
            {
                this.BackgroundImage = dto.BackgroundImage;
            }
        }


    }
}