using System;
using System.Collections.Generic;
using System.Text;

namespace Z.DesignPatterns
{
    public class SingleObject
    {
        private static SingleObject instance = new SingleObject();
        private SingleObject() { }
        public static SingleObject GetInstance()
        {
            return instance;
        }
    }
}
