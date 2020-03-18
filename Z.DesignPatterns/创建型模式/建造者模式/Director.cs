using System;
using System.Collections.Generic;
using System.Text;

namespace Z.DesignPatterns
{
    public class Director
    {
        public void Construct(Builder builder)
        {
            Console.WriteLine($"{ builder.GetHashCode() } 开始组装电脑");
            builder.BuilderPartCpu();
            builder.BuilderPartMainBoard();
            Console.WriteLine($"{ builder.GetHashCode() } 组装电脑完成");
        }
    }
}
