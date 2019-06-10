﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Collections.Generic;
using D.Data;
using D.Data.Entity;

namespace D.Test
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
