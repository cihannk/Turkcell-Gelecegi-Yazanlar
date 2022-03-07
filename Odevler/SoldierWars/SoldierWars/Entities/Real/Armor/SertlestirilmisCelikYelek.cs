using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoldierWars.Entities.Real.Armor
{
    internal class SertlestirilmisCelikYelek : AbstractArmor
    {
        public override string Name { get; set; } = "Sertlestirilmis Celik Yelek";

        public override double ShieldValue { get; set; } = 75;

        public override int Price { get; set; } = 20;
    }
}
