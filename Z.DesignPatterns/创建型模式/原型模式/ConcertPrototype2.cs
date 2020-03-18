using System;
using System.Collections.Generic;
using System.Text;

namespace Z.DesignPatterns
{
    public class ConcertPrototype2:Prototype
    {
        public ConcertPrototype2(Guid id) : base(id) { }

        public override Prototype Clone()
        {
            return (Prototype)this.MemberwiseClone();
        }
    }
}
