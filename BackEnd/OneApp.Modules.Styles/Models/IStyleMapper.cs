using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneApp.Modules.Styles.Models
{
    public interface IStyleMapper
    {
        StyleDTO GetStyleDTO();
        void SetStyleProperties(StyleDTO dto);
    }
}