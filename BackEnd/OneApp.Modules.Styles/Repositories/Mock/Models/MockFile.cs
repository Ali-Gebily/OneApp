﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OneApp.Common.Core.DAL.Mock;
using OneApp.Modules.Styles.Models;

namespace OneApp.Modules.Styles.Repositories.Mock.Models
{
    public class MockFile : MockBaseModel
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