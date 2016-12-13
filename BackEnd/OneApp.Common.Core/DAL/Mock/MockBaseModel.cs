using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneApp.Common.Core.DAL.Mock
{
    public abstract class MockBaseModel<TKey> : IUniqueMockEntity
    {
        public abstract void SetPrimaryKey();
         
    }
    public class MockBaseModel : MockBaseModel<int>
    {
        static Dictionary<string, int> _idsSequencer = new Dictionary<string, int>();

        public int Id { get; set; }

        public override void SetPrimaryKey()
        {
            lock (_idsSequencer)
            {
                var guid = this.GetType().GUID.ToString();
                if (!_idsSequencer.ContainsKey(guid))
                {
                    _idsSequencer.Add(guid, 0);
                }
                _idsSequencer[guid] = _idsSequencer[guid] + 1;

                Id = _idsSequencer[guid];
            }
        }
    }

}