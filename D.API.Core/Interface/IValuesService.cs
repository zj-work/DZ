using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D.API.Core.Interface
{
    public interface IValuesService
    {
        IEnumerable<string> GetNames();
    }
}
