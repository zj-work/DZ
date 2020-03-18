using System;
using System.Collections.Generic;
using System.Text;

namespace Z.DesignPatterns
{
    public class ConcertPrototype1 : Prototype
    {
        public ConcertPrototype1(Guid id) : base(id) { }

        public override Prototype Clone()
        {
            // MemberwiseClone 方法创建一个浅表副本，具体来说就是创建一个新对象，然后将当前对象的非静态字段复制到该新对象。如果字段是值类型的，则对该字段执行逐位复制。如果字段是引用类型，则复制引用但不复制引用的对象；因此，原始对象及其复本引用同一对象。
            return (Prototype)this.MemberwiseClone();
        }
    }
}
