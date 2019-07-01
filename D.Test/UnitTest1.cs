using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Collections.Generic;
using D.Data;
using D.Data.Entity;
using System.Linq;

namespace D.Test
{
    [TestClass]
    public class UnitTest1
    {
        DbContext<tbl_log> dbContext = new DbContext<tbl_log>(DbConfig.DbMain);

        [TestMethod]
        public void TestMethod1()
        {
            List<A> list1 = new List<A>()
            {
                new A(){ id=1,name="zj1" },
                new A(){ id=2,name="zj2" },
                new A(){ id=3,name="zj3" },
                new A(){ id=4,name="zj4" },
                new A(){ id=5,name="zj5" }
            };
            List<B> list2 = new List<B>()
            {
                new B(){ id=1,age=24 },
                new B(){ id=3,age=25 },
                new B(){ id=5,age=26 },
                new B(){ id=7,age=27 },
                new B(){ id=8,age=28 }
            };
            var res = list1.Join(list2, p => p.id, q => q.id, (p, q) => new
            {
                p.id,
                p.name,
                q.age
            });
            Trace.WriteLine(res.ToString());
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
