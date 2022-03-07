using SoldierWars.Entities.Abstract;
using SoldierWars.Entities.Real;
using SoldierWars.Entities.Real.Armor;
using SoldierWars.Entities.Real.Gun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoldierWars.Entities
{
    internal abstract class AbstractSoldier
    {
        public abstract string Name { get; set; }
        public double Stamina { get; set; } = 100;
        public double Hp { get; set; } = 100;
        public abstract double BaseHitPoint { get; set; }
        public abstract int BaseStaminaConsumption { get; set; }
        public AbstractArmor Armor { get; set; } = new StandartCelikYelek();
        public AbstractGun Gun { get; set; } = new Pistol();
        public abstract int SoldierPrice { get; set; }
        public bool IsAlive { get; set; } = true;

        public void Rest()
        {
            if (IsAlive)
            {
                Console.WriteLine($"Asker {Name} dinleniyor");
                Stamina += BaseStaminaConsumption;
                Console.WriteLine($"Asker {Name} dinlendi Stamina: {this.Stamina}");
            }
        }
        public void Attack(AbstractSoldier enemySoldier)
        {
            if (IsAlive)
            {
                double minStamina = this.Gun.Heaviness * BaseStaminaConsumption;
                if (this.Stamina <= minStamina)
                {
                    Console.WriteLine($"Asker {Name}'nın enerjisi {this.Stamina}, saldıramıyor. En az gereken stamina {minStamina}");
                    Rest();
                }
                else
                {
                    //Console.WriteLine($"{Name} adlı asker {Gun.Name} silahıyla {enemySoldier.Name}'e saldırdı.");
                    this.Stamina -= minStamina;
                    double fullDamage = BaseHitPoint + Gun.Damage;
                    if (enemySoldier.Armor.ShieldValue > 0)
                    {
                        Console.WriteLine($"Asker {Name} {enemySoldier.Name}'e {fullDamage} vurdu ");
                        enemySoldier.Armor.ShieldValue -= fullDamage;
                        TakeDamageIfVestIsBroken(enemySoldier);
                    }
                    else
                    {
                        Console.WriteLine($"Asker {Name} {enemySoldier.Name}'e {fullDamage} vurdu ");
                        enemySoldier.Hp -= fullDamage;
                    }

                }
            }
        }

        private void TakeDamageIfVestIsBroken(AbstractSoldier enemySoldier)
        {
            if (enemySoldier.Armor.ShieldValue < 0)
            {
                enemySoldier.Hp += enemySoldier.Armor.ShieldValue;
                enemySoldier.Armor.ShieldValue = 0;
            }
        }

        public bool CheckDead()
        {
            if (Hp <= 0)
            {
                IsAlive = false;
                return true;
            }      
            return false;
        }
    }
}
