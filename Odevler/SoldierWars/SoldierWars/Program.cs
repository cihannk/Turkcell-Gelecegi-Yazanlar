using SoldierWars.Entities;
using SoldierWars.Entities.Abstract;
using SoldierWars.Entities.Real;
using SoldierWars.Entities.Real.Soldier;
using SoldierWars.Logic;

namespace SoldierWars
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Game gameObj = new Game();
            Console.WriteLine("SoldierWars");
            Console.WriteLine("----------------");
            ShowAllOptions(gameObj);
            ChooseSoldiersAndTypesScreen(gameObj);
            Console.WriteLine("Askerlerin");
            gameObj.ShowSoldiers(gameObj.Soldiers);
            Console.WriteLine("----------------");
            Console.WriteLine("Düşman askerleri");
            gameObj.RandomGenerateEnemyArmy();
            gameObj.ShowSoldiers(gameObj.SoldiersOpponent);
            Console.WriteLine("-----------------");
            gameObj.RandomMatch();
            gameObj.BattleAllSoldierObjects(gameObj.Matches);
            Console.WriteLine("Askerlerin");
            gameObj.ShowSoldiers(gameObj.Soldiers);
            Console.WriteLine("----------------");
            Console.WriteLine("Düşman askerleri");
            gameObj.ShowSoldiers(gameObj.SoldiersOpponent);
            Console.WriteLine("-----------------");
            gameObj.ShowResultOfGame();
        }
        static void ShowAllOptions(Game gameObj)
        {
            gameObj.ShowSoldierOptions();
            gameObj.ShowGunOptions();
            gameObj.ShowVestOptions();
        }
        static void ChooseSoldiersAndTypesScreen(Game gameObj)
        {
            int soldierNumber = 1;
            while (gameObj.RemainingSoliderCount>0)
            {
                gameObj.ShowBalanceAndSoldiersCount();
                AbstractSoldier soldier = ChooseSoldierType(gameObj, soldierNumber.ToString());
                gameObj.SubstractFromRemainingSoldierCount();
                ChooseSoldierGunType(gameObj, soldier);
                ChooseSoldierVestType(gameObj, soldier);
                soldierNumber++;
            }
        }
        static AbstractSoldier ChooseSoldierType(Game gameObj, string soldierName)
        {
            bool isDefault = false;
            Console.WriteLine("Hangi asker türünü seçmek istiyorsun (1,2,3): ");
            string soldierOption = Console.ReadLine();
            AbstractSoldier soldier = gameObj.CreateInstanceOfSoldier(0);
            switch (soldierOption)
            {

                case "1":
                    break;
                case "2":
                    if (gameObj.checkBalanceIsEnough(gameObj.Balance, gameObj.SoldierOptions[1].SoldierPrice))
                        soldier = gameObj.CreateInstanceOfSoldier(1);
                    else
                        isDefault = true;
                    break;
                case "3":
                    if (gameObj.checkBalanceIsEnough(gameObj.Balance, gameObj.SoldierOptions[2].SoldierPrice))
                        soldier = gameObj.CreateInstanceOfSoldier(2);
                    else
                        isDefault = true;
                    break;
                default:
                    break;
            }
            if (isDefault)
                Console.WriteLine("Paranız yetmediğinden default asker atandı");
            soldier.Name = soldier.Name + " "+ soldierName;
            gameObj.AddSoliderToArmy(soldier, true);
            return soldier;
        }
        static void ChooseSoldierVestType(Game gameObj, AbstractSoldier soldier)
        {
            Console.WriteLine("Hangi zırh türünü seçmek istiyorsun (1,2,3): ");
            bool isDefault = false;
            string vestOption = Console.ReadLine();
            AbstractArmor armor = gameObj.CreateInstanceOfArmor(0);
            switch (vestOption)
            {
                case "1":
                    break;
                case "2":
                    if (gameObj.checkBalanceIsEnough(gameObj.Balance, gameObj.VestOptions[1].Price))
                        armor = gameObj.CreateInstanceOfArmor(1);
                    else
                        isDefault=true;
                    break;
                case "3":
                    if (gameObj.checkBalanceIsEnough(gameObj.Balance, gameObj.VestOptions[2].Price))
                        armor = gameObj.CreateInstanceOfArmor(2);
                    else
                        isDefault = true;
                    break;
                default:
                    break;
            }
            if (isDefault)
                Console.WriteLine("Paranız yetmediğinden default zırh atandı");
            gameObj.CustomizeSoldierArmor(armor, soldier);
        }
        static void ChooseSoldierGunType(Game gameObj, AbstractSoldier soldier)
        {
            Console.WriteLine("Hangi silah türünü seçmek istiyorsun (1,2,3): ");
            bool isDefault = false;
            string gunOption = Console.ReadLine();
            AbstractGun gun = gameObj.CreateInstanceOfGun(0);
            switch (gunOption)
            {
                case "1":
                    break;
                case "2":
                    if (gameObj.checkBalanceIsEnough(gameObj.Balance, gameObj.GunOptions[1].Price))
                        gun = gameObj.CreateInstanceOfGun(1);
                    else
                        isDefault= true;
                    break;
                case "3":
                    if (gameObj.checkBalanceIsEnough(gameObj.Balance, gameObj.GunOptions[2].Price))
                        gun = gameObj.CreateInstanceOfGun(2);
                    else
                        isDefault= true;
                    break;
                default:
                    break;
            }
            if (isDefault)
                Console.WriteLine("Paranız yetmediğinden default silah atandı");
            gameObj.CustomizeSoldierGun(gun, soldier);
        }
    }
}


