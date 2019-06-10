using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DZ.Data;
using System.Diagnostics;
using DZ.Data.Entity;
using System.Collections.Generic;

namespace DZ.Test
{
    [TestClass]
    public class UnitTest1
    {
        DbContext<tbl_log> dbContext = new DbContext<tbl_log>(DbConfig.DbMain);

        [TestMethod]
        public void TestMethod1()
        {
            var res = dbContext.Add(new tbl_log()
            {
                content = "测试DbContext插入数据01",
                flag = "Info"
            });
            Debug.WriteLine("测试结果：" + res);
        }
    }
}
