using System;
using System.Collections.Generic;
using System.Text;

namespace Z.DesignPatterns
{
    public class ShapeFactory
    {
        public Shape GetShape(string shapeType)
        {
            if (string.IsNullOrEmpty(shapeType)) { return null; }
            else if ("Rectangle".Equals(shapeType)) { return new Rectangle(); }
            else if ("Circle".Equals(shapeType)) { return new Circle(); }
            else { return null; }
        }
    }
}
