using SoldierWars.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoldierWars.Entities.Real.Gun
{
    internal class RocketLauncher : AbstractGun
    {
        public override string Name { get; set; } = "Rocket Launcher";
        public override int Damage { get; set; } = 100;
        public override int Price { get; set; } = 70;
        public override double Heaviness { get; set; } = 3;
    }
}
