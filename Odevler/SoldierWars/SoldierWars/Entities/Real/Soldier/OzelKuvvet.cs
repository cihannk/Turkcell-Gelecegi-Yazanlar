using SoldierWars.Entities.Abstract;
using SoldierWars.Entities.Real.Armor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoldierWars.Entities.Real.Soldier
{
    internal class OzelKuvvet : AbstractSoldier
    {
        public override string Name { get; set; } = "Ozel Kuvvet";
        public override double BaseHitPoint { get; set; } = 20;
        public override int BaseStaminaConsumption { get; set; } = 10;
        public override int SoldierPrice { get; set; } = 25;

    }
}
