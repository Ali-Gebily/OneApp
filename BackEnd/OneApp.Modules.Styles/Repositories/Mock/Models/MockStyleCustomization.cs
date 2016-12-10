using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneApp.Modules.Styles.Repositories.Mock.Models
{
    public class MockStyleCustomization : MockBaseModel
    {
     
        public long RuleId { get; set; }

        public int StyleId { get; set; }

        public string EntityId { get; set; }

        public MockRule Rule { get { return StylesMockContext.Rules.FirstOrDefault(r => r.Id == this.RuleId); } }

        public MockStyle Style
        {
            get
            {
                return StylesMockContext.Styles.FirstOrDefault(s => s.Id == this.StyleId);
            }
            set
            {
                if (value == null)
                {
                    throw new Exception("Style can't be null");
                }
                StyleId = value.Id;
                StylesMockContext.Styles.Add(value);
            }
        }

    }
}