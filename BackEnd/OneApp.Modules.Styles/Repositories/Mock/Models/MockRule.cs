using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneApp.Modules.Styles.Repositories.Mock.Models
{
    public class MockRule : MockBaseModel
    {

        public string Selector { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public int Scope { get; set; }

        public long DefaultStyleId { get; set; }

        public MockStyle DefaultStyle
        {
            get
            {
                return StylesMockContext.Styles.FirstOrDefault(s => s.Id == this.DefaultStyleId);
            }
            set {
                if (value == null) {
                    throw new Exception("DefaultStyle can't be null");
                }
                StylesMockContext.Styles.Add(value);
                DefaultStyleId = value.Id;
            }
        }

        public IEnumerable<MockStyleCustomization> StyleCustomizations { get {
                return StylesMockContext.StyleCustomizations.Where(sc => sc.RuleId == this.Id);
            } }

    }
}