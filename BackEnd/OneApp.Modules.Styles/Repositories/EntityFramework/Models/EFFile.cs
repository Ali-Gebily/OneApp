using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OneApp.Common.Core.DAL.EntityFramework;
using OneApp.Modules.Styles.Models;

namespace OneApp.Modules.Styles.Repositories.EntityFramework.Models
{
    [Table("Files")]
    public class EFFile: BaseEFModel
    {
        [Required]
        public byte[] Data { get; set; }

        [Required]
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