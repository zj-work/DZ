using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D.API.Core.Implement
{
    public class ValuesService : Interface.IValuesService
    {
        public IEnumerable<string> GetNames()
        {
            return new List<string>()
            {
                "01","02","03","04","05","zj06","zy07","zk08","ze09"
            };
        }
    }
}
