using System;
using System.Collections.Generic;
using System.Text;

namespace D.Service.Core.Implement
{
    public class MenuService : IMenuService
    {
        public IEnumerable<string> GetMenuByGroup(string groupId)
        {
            List<string> res = new List<string>();
            if (groupId.Equals("1"))
            {
                res.AddRange(new string[]
                {
                    "系统管理","账户管理","工作量查看"
                });
            }
            else
            {
                res.AddRange(new string[]
                {
                    "文件上传","文件管理"
                });
            }
            return res;
        }
    }
}
