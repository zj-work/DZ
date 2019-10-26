using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Collections.Generic;
using D.Data;
using D.Data.Entity;
using System.Linq;
using D.Extend;

namespace D.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Log.Info("测试操作日志");
            Log.Error("测试错误日志");
            Trace.WriteLine("测试完毕");
        }

        public class A
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        public class B
        {
            public int id { get; set; }
            public int age { get; set; }
        }
    }
}
