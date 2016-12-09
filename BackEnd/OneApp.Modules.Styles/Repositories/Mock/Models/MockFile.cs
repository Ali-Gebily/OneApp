using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneApp.Modules.Styles.Repositories.Mock.Models
{
    public class MockFile: MockBaseModel
    { 
        public byte[] Data { get; set; }

        public string ContentType { get; set; }

        public string Name { get; set; }

        public int Length { get; set; }
    }
}