using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoldierWars.Entities.Abstract
{
    internal abstract class AbstractGun
    {
        public abstract string Name { get; set; }
        public abstract int Damage { get; set; }
        public abstract int Price { get; set; }
        public abstract double Heaviness { get; set; }
    }
}
