using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneApp.Modules.Styles.Repositories.Mock.Models
{
    public class MockStyle: MockBaseModel
    {
        public string Color { get; set; }

        public string BackgroundColor { get; set; }

        public int? BackgroundImage { get; set; } 
    }
}