using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoldierWars.Entities.Real.Armor
{
    internal class StandartCelikYelek : AbstractArmor
    {
        public override string Name { get; set; } = "Standart Celik Yelek";
        public override double ShieldValue { get; set; } = 50;

        public override int Price { get; set; } = 0;
    }
}
