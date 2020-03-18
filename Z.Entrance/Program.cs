using System;
using Z.DesignPatterns;

namespace Z.Entrance
{
    class Program
    {
        static void Main(string[] args)
        {
            //建造者模式
            BuilderPattern();

            Console.ReadKey();
        }

        static void BuilderPattern()
        {
            var director = new Director();
            Builder builder1 = new ConcreteBuilder();
            Builder builder2 = new ConcreteBuilder();

            director.Construct(builder1);
            director.Construct(builder2);
        }
    }
}
