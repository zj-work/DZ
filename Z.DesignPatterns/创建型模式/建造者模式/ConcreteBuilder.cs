using System;
using System.Collections.Generic;
using System.Text;

namespace Z.DesignPatterns
{
    public class ConcreteBuilder:Builder
    {
        private readonly Computer _computer = new Computer();

        public override void BuilderPartCpu()
        {
            _computer.Add($"CPU {this.GetHashCode()}");
            System.Threading.Thread.Sleep(500);
            Console.WriteLine($"{ this.GetHashCode() } 组装CPU完成");
        }

        public override void BuilderPartMainBoard()
        {
            _computer.Add($"Main board {this.GetHashCode()}");
            System.Threading.Thread.Sleep(500);
            Console.WriteLine($"{ this.GetHashCode() } 组装Main board完成");
        }

        public override Computer GetComputer()
        {
            return _computer;
        }
    }
}
