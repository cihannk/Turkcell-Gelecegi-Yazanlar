using SoldierWars.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoldierWars.Entities.Real
{
    internal abstract class AbstractArmor
    {
        public abstract string Name { get; set; }
        public abstract double ShieldValue { get; set; }
        public abstract int Price { get; set; }
    }
}
