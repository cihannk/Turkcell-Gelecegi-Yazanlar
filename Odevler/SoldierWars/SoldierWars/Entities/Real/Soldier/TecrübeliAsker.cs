using SoldierWars.Entities.Abstract;
using SoldierWars.Entities.Real.Armor;
using SoldierWars.Entities.Real.Gun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoldierWars.Entities.Real.Soldier
{
    internal class TecrübeliAsker : AbstractSoldier
    {
        public override string Name { get; set; } = "Tecrübeli Asker";
        public override double BaseHitPoint { get; set; } = 15;
        public override int BaseStaminaConsumption { get; set; } = 15;
        public override int SoldierPrice { get; set; } = 15;

    }
}
