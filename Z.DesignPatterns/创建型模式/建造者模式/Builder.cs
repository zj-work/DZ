using System;
using System.Collections.Generic;
using System.Text;

namespace Z.DesignPatterns
{
    public abstract class Builder
    {
        public abstract void BuilderPartCpu();

        public abstract void BuilderPartMainBoard();

        public abstract Computer GetComputer();
    }
}
