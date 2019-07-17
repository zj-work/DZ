using System;
using System.Collections.Generic;
using System.Text;

namespace D.Service.Core
{
    public interface IMenuService
    {
        IEnumerable<string> GetMenuByGroup(string groupId);
    }
}
