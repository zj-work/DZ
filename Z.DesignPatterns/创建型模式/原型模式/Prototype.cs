using System;
using System.Collections.Generic;
using System.Text;

namespace Z.DesignPatterns
{
    public abstract class Prototype
    {
        private Guid _id = Guid.Empty;

        public Prototype(Guid id)
        {
            this._id = id;
        }

        public Guid ID { get { return this._id; } }

        public abstract Prototype Clone();
    }
}
