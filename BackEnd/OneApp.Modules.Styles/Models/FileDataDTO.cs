using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneApp.Modules.Styles.Models
{
    public class FileDataDTO
    {

        public string Id { get; set; }

        public byte[] Data { get; set; }

        public string ContentType { get; set; }
         
        public string Name { get; set; }
         
        public int Length { get; set; }
    }
}