using SoldierWars.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoldierWars.Entities.Real.Gun
{
    internal class Pistol : AbstractGun
    {
        public override string Name { get; set; } = "Pistol";
        public override int Damage { get; set; } = 10;
        public override int Price { get; set; } = 0;
        public override double Heaviness { get; set; } = 1;
    }
}
