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
        public string content { get; set; }
        public string flag { get; set; }
    }
}
