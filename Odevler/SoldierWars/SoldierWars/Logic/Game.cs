using SoldierWars.Entities;
using SoldierWars.Entities.Abstract;
using SoldierWars.Entities.Real;
using SoldierWars.Entities.Real.Armor;
using SoldierWars.Entities.Real.Gun;
using SoldierWars.Entities.Real.Soldier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoldierWars.Logic
{
    internal class Game
    {
        public int Balance { get; set; } = 325;
        public int EnemyBalance { get; set; } = 325;
        public int RemainingSoliderCount { get; private set; } =  5;
        public List<AbstractSoldier> SoldierOptions { get; } = new List<AbstractSoldier> { new Piyade(), new TecrübeliAsker(), new OzelKuvvet() };
        public List<AbstractGun> GunOptions { get; set; } = new List<AbstractGun> { new Pistol(), new M4A1(), new RocketLauncher() };
        public List<AbstractArmor> VestOptions { get; set; } = new List<AbstractArmor> { new StandartCelikYelek(), new SertlestirilmisCelikYelek(), new YeniTeknolojiCelikYelek() };

        public List<AbstractSoldier> Soldiers { get; set; } = new List<AbstractSoldier>();
        public List<AbstractSoldier> SoldiersOpponent { get; set; } = new List<AbstractSoldier>();
        public Dictionary<AbstractSoldier, AbstractSoldier> Matches = new Dictionary<AbstractSoldier, AbstractSoldier>();
        public void SubstractFromRemainingSoldierCount()
        {
            RemainingSoliderCount -= 1;
        }
        public void ShowBalanceAndSoldiersCount()
        {
            if (Soldiers == null)
            {
                Console.WriteLine($"Bakiye: {Balance} - Asker Sayısı: 0");
            }
            else
            {
                Console.WriteLine($"Bakiye: {Balance} - Asker Sayısı: {Soldiers.Count}");
            }
        }
        public void ShowSoldiers(List<AbstractSoldier> soldiers)
        {
            int i = 1;
            foreach (var soldier in soldiers)
            {
                Console.WriteLine("-------");
                Console.WriteLine($"Asker {i}: Adı: {soldier.Name} Canı: {soldier.Hp} Zırhı: {soldier.Armor.ShieldValue} Enerjisi: {soldier.Stamina} Kullandığı silah: {soldier.Gun.Name} Yaşıyor Mu: {soldier.IsAlive}");
                i++;
            }
        }
        public void ShowGunOptions()
        {
            Console.WriteLine("Silahlar");
            Console.WriteLine("--------------");
            foreach (var gun in GunOptions)
            {
                Console.WriteLine($"isim: {gun.Name} fiyat: {gun.Price}$ hasar: {gun.Damage} ağırlık: {gun.Heaviness}");
                Console.WriteLine("-");
            }
            Console.WriteLine("--------------");
        }
        public void ShowVestOptions()
        {
            Console.WriteLine("Zırhlar");
            Console.WriteLine("--------------");
            foreach (var vest in VestOptions)
            {
                Console.WriteLine($"isim: {vest.Name} fiyat: {vest.Price}$ koruma değeri: {vest.ShieldValue}");
                Console.WriteLine("-");
            }
            Console.WriteLine("--------------");
        }
        public void ShowSoldierOptions()
        {
            Console.WriteLine("Askerler");
            Console.WriteLine("--------------");
            foreach (var soldier in SoldierOptions)
            {
                Console.WriteLine($"isim: {soldier.Name} fiyat: {soldier.SoldierPrice}$ base damage: {soldier.BaseHitPoint} yorulma derecesi: {soldier.BaseStaminaConsumption}");
                Console.WriteLine("-");
            }
            Console.WriteLine("--------------");
        }
        public void AddSoliderToArmy(AbstractSoldier soldier, bool isPlayer)
        {
            if (isPlayer)
            {
                Soldiers.Add(soldier);
                Console.WriteLine($"Asker {soldier.Name} Orduya Eklendi");
                Balance -= soldier.SoldierPrice;
            }
            else
            {
                SoldiersOpponent.Add(soldier);
                EnemyBalance -= soldier.SoldierPrice;
            }
            
        }
        public void CustomizeSoldier(AbstractSoldier soldier, AbstractArmor armor, AbstractGun gun)
        {
            soldier.Armor = armor;
            soldier.Gun = gun;
            Balance -= armor.Price + gun.Price;
        }
        public void CustomizeSoldierArmor(AbstractArmor armor, AbstractSoldier soldier)
        {
            soldier.Armor = armor;
            Balance -= armor.Price;
        }
        public void CustomizeSoldierGun(AbstractGun gun, AbstractSoldier soldier)
        {
            soldier.Gun = gun;
            Balance -= gun.Price;
        }
        public void RandomMatch()
        {
            List<AbstractSoldier> soldiers = new List<AbstractSoldier>();
            soldiers.AddRange(Soldiers);
            //if (object.ReferenceEquals(Soldiers, soldiers))
            //    Console.WriteLine("eşitler");
            List<AbstractSoldier> oppenentSoldiers = new List<AbstractSoldier>();
            oppenentSoldiers.AddRange(SoldiersOpponent);
            Random rand = new Random();
            while(soldiers.Count > 0)
            {
                int randVal = rand.Next(0, soldiers.Count);
                int randVal2 = rand.Next(0, soldiers.Count);
                Matches.Add(soldiers[randVal], oppenentSoldiers[randVal2]);
                soldiers.RemoveAt(randVal);
                oppenentSoldiers.RemoveAt(randVal2);
            }
        }
        
        public void BattleAllSoldierObjects(Dictionary<AbstractSoldier, AbstractSoldier> soldiersDict)
        {
            bool deadSoldierIsMine;
            foreach (var soldiers in soldiersDict)
            {
                Console.WriteLine("-----Ara Savaş-----");
                AbstractSoldier deadSoldier = BattleSoliders(soldiers.Key, soldiers.Value);
                Console.WriteLine("-----Ara Savaş Sonu-----");

                if (soldiersDict.Keys.Contains(deadSoldier))
                    deadSoldierIsMine = true;
                else
                    deadSoldierIsMine = false;
                PowerUpArmy(soldiersDict, !deadSoldierIsMine, deadSoldier);
                Thread.Sleep(5000);
            }
        }
        public AbstractSoldier BattleSoliders(AbstractSoldier mySoldier, AbstractSoldier enemySoldier)
        {
            Random rand = new Random();
            AbstractSoldier deadSoldier = null;
            int whoIsAttackingFirst = rand.Next(0, 2);
            
            while (deadSoldier == null)
            {
                if (whoIsAttackingFirst == 0)
                {
                    mySoldier.Attack(enemySoldier);
                    if (enemySoldier.CheckDead())
                    {
                        deadSoldier= enemySoldier;
                        Console.WriteLine($"* Asker {enemySoldier.Name} öldü");
                        break;
                    }
                    enemySoldier.Attack(mySoldier);
                    if (mySoldier.CheckDead())
                    {
                        deadSoldier = mySoldier;
                        Console.WriteLine($"* Asker {mySoldier.Name} öldü");
                        break;
                    }
                }
                else
                {
                    enemySoldier.Attack(mySoldier);
                    if (mySoldier.CheckDead())
                    {
                        deadSoldier = mySoldier;
                        Console.WriteLine($"* Asker {mySoldier} öldü");
                        break;
                    }
                    mySoldier.Attack(enemySoldier);
                    if (enemySoldier.CheckDead())
                    {
                        deadSoldier = enemySoldier;
                        Console.WriteLine($"* Asker {enemySoldier} öldü");
                        break;
                    }
                }
            }
            return deadSoldier;
        }

        public void PowerUpArmy(Dictionary<AbstractSoldier, AbstractSoldier> matches, bool powerUpMyTeam, AbstractSoldier deadSoldier)
        {
            double baseHitPointIncreasePerSoldier = (deadSoldier.Gun.Damage + deadSoldier.BaseHitPoint) / matches.Count;
            switch (powerUpMyTeam)
            {
                case true:
                    
                    foreach (var soldierOfMyTeam in matches.Keys)
                    {
                        soldierOfMyTeam.BaseHitPoint += baseHitPointIncreasePerSoldier;
                    }
                    Console.WriteLine($"Takımın saldırısı {baseHitPointIncreasePerSoldier} artacak şekilde güçlendi");
                    break;
                case false:
                    foreach (var soldierOfEnemyTeam in matches.Values)
                    {
                        soldierOfEnemyTeam.BaseHitPoint += baseHitPointIncreasePerSoldier;
                    }
                    Console.WriteLine($"Düşman takımın saldırısı {baseHitPointIncreasePerSoldier} artacak şekilde güçlendi");
                    break;
                default:
                    break;
            }
            
        }
        public int[] CalculateDeadMansCount(Dictionary<AbstractSoldier, AbstractSoldier> matches)
        {
            int[] deadMansCount = { 0, 0 };
            foreach (var item in matches)
            {
                if (item.Key.IsAlive == false)
                    deadMansCount[0] += 1;
                if (item.Value.IsAlive == false)
                    deadMansCount[1] += 1;
            }
            return deadMansCount;
        }
        public void ShowResultOfGame()
        {
            int[] deadMans = CalculateDeadMansCount(Matches);
            if (deadMans[0] < deadMans[1])
            {
                Console.WriteLine($"Oyunu kazandınız... Karşı takımın {deadMans[1]} askeri öldü sizin ise {deadMans[0]}");
            }
            else if (deadMans[1] < deadMans[0])
            {
                Console.WriteLine($"Oyunu kaybettiniz. Karşı takımın {deadMans[1]} askeri öldü sizin ise {deadMans[0]}");
            }
            else
            {
                Console.WriteLine($"Oyun berabere. Karşı takımın {deadMans[1]} askeri öldü sizin ise {deadMans[0]}");
            }
            
        }

        public AbstractSoldier CreateInstanceOfSoldier(int number)
        {
            AbstractSoldier soldier = new Piyade();
            switch (number)
            {
                case 0:
                    soldier = new Piyade();
                    break;
                case 1:
                    soldier = new TecrübeliAsker();
                    break;
                case 2:
                    soldier = new OzelKuvvet();
                    break;
                default:
                    break;
            }
            return soldier;
        }
        public AbstractGun CreateInstanceOfGun(int number)
        {
            AbstractGun gun = new Pistol();
            switch (number)
            {
                case 0:
                    gun = new Pistol();
                    break;
                case 1:
                    gun = new M4A1();
                    break;
                case 2:
                    gun = new RocketLauncher();
                    break;
                default:
                    break;
            }
            return gun;
        }
        public AbstractArmor CreateInstanceOfArmor(int number)
        {
            AbstractArmor armor = new StandartCelikYelek();
            switch (number)
            {
                case 0:
                    armor = new StandartCelikYelek();
                    break;
                case 1:
                    armor = new SertlestirilmisCelikYelek();
                    break;
                case 2:
                    armor = new YeniTeknolojiCelikYelek();
                    break;
                default:
                    break;
            }
            return armor;
        }
        public void RandomGenerateEnemyArmy(int soldierCount= 5)
        {
            Random rand = new Random();
            for (int i = 0; i<soldierCount; i++)
            {
                int randInt = rand.Next(0, 3);
                AbstractSoldier soldier = CreateInstanceOfSoldier(randInt);
                if (checkBalanceIsEnough(EnemyBalance, soldier.SoldierPrice))
                {
                    soldier.Name = soldier.Name +" " +(i + 1).ToString() + " Düşman";
                    AddSoliderToArmy(soldier, false);
                }
                else
                {
                    soldier = CreateInstanceOfSoldier(0);
                    soldier.Name = soldier.Name + (i + 1).ToString() +" Düşman";
                    AddSoliderToArmy(soldier, false);
                }
                EnemyBalance -= soldier.SoldierPrice;

                randInt = rand.Next(0, 3);
                AbstractGun gun = CreateInstanceOfGun(randInt);
                if (checkBalanceIsEnough(EnemyBalance, soldier.SoldierPrice))
                {
                    soldier.Gun = gun;
                }
                else
                {
                    gun = CreateInstanceOfGun(0);
                    soldier.Gun = gun;
                }
                EnemyBalance -= gun.Price;

                randInt = rand.Next(0, 3);
                AbstractArmor armor = CreateInstanceOfArmor(randInt);
                if (checkBalanceIsEnough(EnemyBalance, soldier.SoldierPrice))
                {
                    soldier.Armor = armor;
                }
                else
                {
                    armor = CreateInstanceOfArmor(0);
                    soldier.Armor = armor;

                }
                EnemyBalance -= armor.Price;
            }
            
        }
        public bool checkBalanceIsEnough(int balance, int nextPrice = 0)
        {
            if (balance <= 0)
            {
                return false;
            }
            if (balance - nextPrice < 0)
            {
                return false;
            }
            return true;
        }
        



    }
}
