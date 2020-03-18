using System;
using System.Collections.Generic;
using System.Text;

namespace Z.DesignPatterns
{
    public class Computer
    {
        private readonly IList<string> _parts = new List<string>();

        public void Add(string part)
        {
            _parts.Add(part);
        }
    }
}
