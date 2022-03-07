using SoldierWars.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoldierWars.Entities.Real.Gun
{
    internal class M4A1 : AbstractGun
    {
        public override string Name { get; set; } = "M4A1";
        public override int Damage { get; set; } = 20;
        public override int Price { get; set; } = 30;
        public override double Heaviness { get; set; } = 1.5;
    }
}
