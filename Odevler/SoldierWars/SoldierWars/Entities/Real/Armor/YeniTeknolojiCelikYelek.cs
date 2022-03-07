using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoldierWars.Entities.Real.Armor
{
    internal class YeniTeknolojiCelikYelek : AbstractArmor
    {
        public override string Name { get; set; } = "Yeni Teknoloji Celik Yelek";

        public override double ShieldValue { get; set; } =  100;

        public override int Price { get; set; } =35;
    }
}
