using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Data.Entity
{
    [Dapper.Contrib.Extensions.Table("tbl_log")]
    public class tbl_log
    {
        public int id { get; set; }
        public string type { get; set; }
        public string content { get; set; }
        public DateTime createtime { get; set; }
        public string ip { get; set; }
        public string remarks { get; set; }
    }
}
