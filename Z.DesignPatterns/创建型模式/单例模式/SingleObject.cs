using System;
using System.Collections.Generic;
using System.Text;

namespace Z.DesignPatterns
{
    public class SingleObject
    {
        private static SingleObject instance = null;
        private static readonly object lockObj = new object();

        private SingleObject() { }

        public static SingleObject GetInstance()
        {
            if (instance == null)
            {
                lock (lockObj)
                {
                    if (lockObj == null)
                    {
                        instance = new SingleObject();
                    }
                }
            }
            return instance;
        }
    }
}
