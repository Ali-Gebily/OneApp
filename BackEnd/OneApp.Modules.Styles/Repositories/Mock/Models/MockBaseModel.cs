using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneApp.Modules.Styles.Repositories.Mock.Models
{
    public class MockBaseModel
    {
        static Dictionary<string, int> _idsSequencer = new Dictionary<string, int>();

        public int GetNextId()
        {
            lock (_idsSequencer)
            {
                var guid = this.GetType().GUID.ToString();
                if (!_idsSequencer.ContainsKey(guid))
                {
                    _idsSequencer.Add(guid, 0);
                }
                _idsSequencer[guid] = _idsSequencer[guid] + 1;

                return _idsSequencer[guid];
            }
        }

        public int Id { get; private set; }

        public MockBaseModel()
        {
            Id = GetNextId();
        }
    }
}