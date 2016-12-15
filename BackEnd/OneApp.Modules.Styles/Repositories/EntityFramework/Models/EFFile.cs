using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OneApp.Common.Core.DAL.EntityFramework;
using OneApp.Modules.Styles.Models;

namespace OneApp.Modules.Styles.Repositories.EntityFramework.Models
{
    public class EFFile : BaseEFModel
    {
        public byte[] Data { get; set; }
        public string ContentType { get; set; }

        public string Name { get; set; }
        public int Length { get; set; }

        public FileDataDTO GetFileDataDTO()
        {
            FileDataDTO dto = new FileDataDTO();

            dto.ContentType = this.ContentType;
            dto.Data = this.Data;
            dto.Id = this.Id;
            dto.Length = this.Length;
            dto.Name = this.Name;
            return dto;
        }
    }
}