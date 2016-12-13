using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneApp.Modules.Styles.Repositories.Mock.Models
{
    public class MockStyleCustomization
    {

        public string UserId { get; set; }

        public string EntityId { get; set; }


        public MockStyle Style { get; set; }
    }
}