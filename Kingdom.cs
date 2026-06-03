public class Kingdom
{
    public int Day { get; set; } = 1;
    public int Level { get; set; } = 1;

    public int Population { get; set; } = 10;
    public int Food { get; set; } = 50;
    public int Wood { get; set; } = 30;
    public int Stone { get; set; } = 20;
    public int Gold { get; set; } = 20;
    public int Iron { get; set; } = 10;
    public Building Barracks { get; set; }
    public Building Farms { get; set; }
    public Building GoldMine { get; set; }
    public Building StoneQuarry { get; set; }
    public Building LumberMill { get; set; }
    public Building TrainingBarracks { get; set; }
    public Building IronMine { get; set; }

    public bool IsGameOver { get; set; } = false;
    public int ThreatLevel { get; set; } = 0;

    public Kingdom()
    {
        Barracks = new Building("Barracks", 1);
        Farms = new Building("Farms", 1);
        GoldMine = new Building("Gold Mine", 1);
        IronMine = new Building("Iron Mine", 1);
        StoneQuarry = new Building("Stone Quarry", 1);
        LumberMill = new Building("Lumber Mill", 1);
        TrainingBarracks = new Building("Training Barracks", 1);
    }

    public List<Soldier> Army { get; set; } = new List<Soldier>();

    public int GetMaxPopulation()
    {
        return 10 + Farms.Level * 5;
    }

    public int GetDailyFoodConsumtion()
    {
        return Population;
    }

    public void ShowStatus()
    {
        Console.WriteLine("===============================");
        Console.WriteLine("           Rise Again");
        Console.WriteLine("===============================");
        Console.WriteLine($"Day: {Day}");
        Console.WriteLine($"Kingdom Level: {Level}");
        Console.WriteLine($"Threat Level: {ThreatLevel}");
        Console.WriteLine($"Threat Status: {GetThreatStatus()}");
        Console.WriteLine($"Attack Chance: {GetAttackChance()}%");
        Console.WriteLine();
        Console.WriteLine($"Population: {Population} / {GetMaxPopulation()}");
        Console.WriteLine($"Food: {Food}");
        Console.WriteLine($"Daily rations needed: {GetDailyFoodConsumtion()}");
        Console.WriteLine($"Wood: {Wood}");
        Console.WriteLine($"Stone: {Stone}");
        Console.WriteLine($"Iron: {Iron}");
        Console.WriteLine($"Gold: {Gold}");

        Console.WriteLine();
        Console.WriteLine("Buildings:");
        Console.WriteLine($"{Barracks.Name}: Level {Barracks.Level}");
        Console.WriteLine($"{Farms.Name}: Level {Farms.Level}");
        Console.WriteLine($"{GoldMine.Name}: Level {GoldMine.Level}");
        Console.WriteLine($"{IronMine.Name}: Level {IronMine.Level}");
        Console.WriteLine($"{LumberMill.Name}: Level {LumberMill.Level}");
        Console.WriteLine($"{StoneQuarry.Name}: Level {StoneQuarry.Level}");

        Console.WriteLine();
        Console.WriteLine($"{TrainingBarracks.Name}: Level {TrainingBarracks.Level}");
        Console.WriteLine("Army:");
        Console.WriteLine($"Total Army Power: {GetArmyPower()}");

        if ( Army.Count == 0)
        {
            Console.WriteLine("No Soldier Trained");
        }
        else
        {
            foreach (Soldier soldier in Army)
            {
                int basePower = GetSoldierBasePower(soldier.Type);
                int groupPower = basePower * soldier.Level * soldier.Count;

                Console.WriteLine($"{soldier.Type} Level {soldier.Level}: {soldier.Count}");
            }
        }

        Console.WriteLine("===============================");
    }

    public int GetSoldierCost(int baseCost, int soldierLevel)
    {
        return (int)Math.Ceiling(baseCost * Math.Pow(1.25, soldierLevel - 1));
    }

    public void TrainSoldier(string soldierType, int soldierLevel)
    {
        if (soldierLevel < 1 || soldierLevel > 10)
        {
            Console.WriteLine("Soldier reached his peak performance");
            return;
        }

        if (soldierLevel > TrainingBarracks.Level)
        {
            Console.WriteLine($"Training Barracks is not high enough.");
            Console.WriteLine($"Training Barracks Level: {TrainingBarracks.Level}");
            Console.WriteLine($"Required Level: {soldierLevel}");
            return;
        }

        int foodCost = 0;
        int goldCost = 0;
        int woodCost = 0;
        int stoneCost = 0;
        int ironCost = 0;

        if (soldierType == "Archer")
        {
            foodCost = GetSoldierCost(8, soldierLevel);
            woodCost = GetSoldierCost(6, soldierLevel);
            goldCost = GetSoldierCost(4, soldierLevel);
        }
        else if (soldierType == "Swordsman")
        {
            foodCost = GetSoldierCost(8, soldierLevel);
            ironCost = GetSoldierCost(6, soldierLevel);
            goldCost = GetSoldierCost(6, soldierLevel);
        }
        else if (soldierType == "Infantry")
        {
            foodCost = GetSoldierCost(10, soldierLevel);
            ironCost = GetSoldierCost(8, soldierLevel);
            goldCost = GetSoldierCost(6, soldierLevel);
            woodCost = GetSoldierCost(6, soldierLevel);
        }
        else
        {
            Console.WriteLine("Invalid Soldier Type.");
            return;
        }

        Console.WriteLine($"Train {soldierType} Level {soldierLevel}");
        Console.WriteLine($"Cost: {foodCost} food, {goldCost} gold, {ironCost} iron ores ,{woodCost} wood, {stoneCost} stone");
        Console.WriteLine();

        if (Food < foodCost || Wood < woodCost || Stone < stoneCost || Gold < goldCost || Iron < ironCost)
        {
            Console.WriteLine("Not enought resources to train soldier");
            return;
        }

        Food -= foodCost;
        Wood -= woodCost;
        Stone -= stoneCost;
        Gold -= goldCost;
        Iron -= ironCost;

        Soldier? exisistingSoldierGroup = Army.FirstOrDefault(
            soldier => soldier.Type == soldierType && soldier.Level == soldierLevel     //de retinut procedura de atribuire grup
            );

        if (exisistingSoldierGroup == null)
        {
            Army.Add(new Soldier(soldierType, soldierLevel, 1));
            return;
        }
        else
        {
            exisistingSoldierGroup.Count++;
        }

        Console.WriteLine($"{soldierType} Level {soldierLevel} trained");
    }

    public void NextDay()
    {
        Console.Clear();

        Console.WriteLine("A new day begins...");
        Console.WriteLine();

        int foodProduced = Farms.Level * 10;
        int woodProduced = LumberMill.Level * 4;
        int stoneProduced = StoneQuarry.Level * 3;
        int ironProduced = IronMine.Level * 6;
        int goldProduced = GoldMine.Level * 4;

        Food += foodProduced;
        Wood += woodProduced;
        Stone += stoneProduced;
        Iron += ironProduced;
        Gold += goldProduced;

        Console.WriteLine($"Farms produced +{foodProduced} food");
        Console.WriteLine($"Lumber Mill produced +{woodProduced} wood");
        Console.WriteLine($"Stone Quarry produced +{stoneProduced} stone");
        Console.WriteLine($"Iron Mine produced +{ironProduced} ores");
        Console.WriteLine($"Mine produced +{goldProduced} gold");

        Console.WriteLine();

        int foodNeeded = GetDailyFoodConsumtion();

        if (Food >= foodNeeded)
        {
            Food -= foodNeeded;

            Console.WriteLine($"Population consumed {foodNeeded} food rations");
        }

        else
        {
            int missingFood = foodNeeded - Food;
            int peopleLost = missingFood;

            Food = 0;
            Population -= peopleLost;

            if (Population < 0)
            {
                Population = 0;
            }

            Console.WriteLine($"Not enought food. The kingdom needed {foodNeeded} rations");
            Console.WriteLine($"Missing rations: {missingFood}");
            Console.WriteLine($"{peopleLost} people died from hunger");

            if (Population <= 0)
            {
                IsGameOver = true;

                Console.WriteLine();
                Console.WriteLine("All people have died!");
                Console.WriteLine("         GAME OVER");
            }

            return;

        }

        if (Day % 10 == 0)
        {
            Level++;
            Console.WriteLine($"Kingdom reached level {Level}!");
        }

        IncreaseThreat();

        TryEnemyAttack();

        Day++;
    }

    public void GrowPopulation()
    {
        int maxPopulation = GetMaxPopulation();

        if (Population >= maxPopulation)
        {
            Console.WriteLine("Population cannot grow. Not enought housing.");
            Console.WriteLine("Upgrade Barracks to increase max population");
        }

        int foodCost = 10;
        int goldCost = 3;

        Console.WriteLine("Grow Population");
        Console.WriteLine($"Current population: {Population} / {maxPopulation}");
        Console.WriteLine($"Cost: {foodCost} food, {goldCost} gold");

        if (Food >= foodCost && Gold >= goldCost)
        {
            Food -= foodCost;
            Gold -= goldCost;
            Population++;

            Console.WriteLine("Population increased by 1.");

        }

        else
        {
            Console.WriteLine("Not enought resources to grow population.");
        }
    }

    public void UpgradeBuiling(Building building)
    {
        int maxBuildingLevel = GetMaxBuildingLevel();

        if (building.Level >= maxBuildingLevel)
        {
            Console.WriteLine($"{building.Name} cannot be upgraded further.");
            Console.WriteLine($"Maximum allowed level is {maxBuildingLevel}");
            return;
        }
        
        int woodCost = building.Level * 4;
        int stoneCost = building.Level * 4;
        int goldCost = building.Level * 8;

        Console.WriteLine($"Upgrade {building.Name}");
        Console.WriteLine($"Current level {building.Level} .");
        Console.WriteLine($"Cost {woodCost} wood, {stoneCost} stone, {goldCost}, gold");
        Console.WriteLine();

        if (Wood >= woodCost && Stone >= stoneCost && Gold >= goldCost)
        {
            Wood -= woodCost;
            Stone -= stoneCost;
            Gold -= goldCost;

            building.Level++;

            Console.WriteLine($"{building.Name} upgraded to level {building.Level}.");
        }
        else
        {
            Console.WriteLine("Not enough resources.");
        }
    }

    public List<Building> GetAllBuildings()
    {
        return new List<Building>
        {
            Farms,
            Barracks,
            GoldMine,
            IronMine,
            StoneQuarry,
            LumberMill,
            TrainingBarracks
        };
    }

    public int GetMaxBuildingLevel()
    {
        if (Level < 3)
        {
            return int.MaxValue;
        }

        return Level + 3;
    }

    public int GetUpgradeCost(int baseCost)
    {
        return (int)Math.Ceiling(baseCost * Math.Pow(1.2, Level - 1));
    }

    public int GetRequiredPopulationForLevelUp()
    {
        return Level * 10;
    }

    public bool AreBuildingsReadyForKingdomLevelUp()
    {
        int requiredBuildingLevel = Level + 1;

        foreach (Building building in GetAllBuildings())
        {
            if (building.Level < requiredBuildingLevel)
            {
                return false;
            }
        }

        return true;
    }

    public void LevelUpKingdom()
    {
        int requiredPopulation = GetRequiredPopulationForLevelUp();

        int foodCost = GetUpgradeCost(100);
        int woodCost = GetUpgradeCost(80);
        int goldCost = GetUpgradeCost(50);
        int stoneCost = GetUpgradeCost(60);

        Console.WriteLine("Kingdom Level Up");
        Console.WriteLine($"Current Kingdom Level: {Level}");
        Console.WriteLine($"Next Kingdom level: {Level + 1}");
        Console.WriteLine();
        Console.WriteLine("Requirements:");
        Console.WriteLine($"Population: {Population} / {requiredPopulation}");
        Console.WriteLine($"All buidlings must be at least {Level + 1}");
        Console.WriteLine();
        Console.WriteLine("Cost:");
        Console.WriteLine($"{foodCost} food, {woodCost} wood, {stoneCost} stone, {goldCost} gold");
        Console.WriteLine();

        if (Population < requiredPopulation)
        {
            Console.WriteLine("Not enought population");
            return;
        }

        if (!AreBuildingsReadyForKingdomLevelUp())
        {
            Console.WriteLine("Buildings do not reach the required level.");

            foreach (Building building in GetAllBuildings())
            {
                if (building.Level < Level + 1)
                {
                    Console.WriteLine($"- {building.Name} must be at leat level: {Level +1}. Current level: {building.Level}");
                }
            }
            return;
        }

        if (Food < foodCost || Wood < woodCost || Stone < stoneCost || Gold < goldCost)
        {
            Console.WriteLine("Not enought resources");
            return;
        }

        Food -= foodCost;
        Wood -= woodCost;
        Stone -= stoneCost;
        Gold -= goldCost;

        Level++;

        Console.WriteLine($"Kingdom upgraded to level: {Level}");
    }

    public void ShowLevelUpRequirements()
    {
        int requiredPopulation = GetRequiredPopulationForLevelUp();

        int foodCost = GetUpgradeCost(100);
        int woodCost = GetUpgradeCost(80);
        int goldCost = GetUpgradeCost(50);
        int stoneCost = GetUpgradeCost(60);

        Console.WriteLine("======================================");
        Console.WriteLine("            KINGDOM LEVEL REQUIREMENTS");
        Console.WriteLine($"Current Kingdom Level: {Level}");
        Console.WriteLine($"Next Kingdom Level: {Level + 1}");
        Console.WriteLine();
        Console.WriteLine("Population requirements:");
        Console.WriteLine($"Population: {Population} / {requiredPopulation}");
        Console.WriteLine();
        Console.WriteLine("Building Requirements:");
        Console.WriteLine($"All buidlings must be at least: {Level + 1}");

        foreach (Building building in GetAllBuildings())
        {
            string status = building.Level >= Level + 1 ? "OK" : "NOT READY";  //un al mod de a scrie if status >= lvl+1 choose A or B
            Console.WriteLine($"{building.Name}: Level {building.Level} / Required {Level +1} - {status}");
        }

        Console.WriteLine();
        Console.WriteLine("Resource cost:");
        Console.WriteLine($"Food: {Food} / {foodCost}");
        Console.WriteLine($"Wood: {Wood} / {woodCost}");
        Console.WriteLine($"Stone: {Stone} / {stoneCost}");
        Console.WriteLine($"Gold: {Gold} / {goldCost}");
        Console.WriteLine("======================================");

    }

    public int GetSoldierBasePower(string soldierType)
    {
        if (soldierType == "Archer")
        { 
            return 2;
        }

        if (soldierType == "Swordsman")
        {
            return 3;
        }

        if (soldierType == "Infantry")
        {
            return 4;
        }

        return 0;
    }

    public int GetArmyPower()
    {
        int totalPower = 0;

        foreach (Soldier soldier in Army)
        {
            int basePower = GetSoldierBasePower(soldier.Type);
            int groupPower = basePower * soldier.Level * soldier.Count;

            totalPower += groupPower;
        }

        return totalPower;
    }

    public void IncreaseThreat()
    {
        Random random = new Random();

        int threatIncrease = random.Next(1, 4);

        ThreatLevel += threatIncrease;

        Console.WriteLine($"Threat incresed by {threatIncrease}");
    }

    public string GetThreatStatus()
    {
        if (ThreatLevel < 10)
        {
            return "Safe";
        }

        if (ThreatLevel <20)
        {
            return "Tension";
        }

        if (ThreatLevel < 30)
        {
            return "Dangerous";
        }

        return "Attack likely";
    }

    public int GetAttackChance()
    {
        if (ThreatLevel < 10)
        {
            return 0;
        }

        if (ThreatLevel < 20)
        {
            return 10;
        }

        if (ThreatLevel < 30)
        {
            return 25;
        }

        return 50;
    }

    public void TryEnemyAttack()
    {
        int attackChance = GetAttackChance();

        if (attackChance == 0)
        {
            return;
        }

        Random random = new Random();

        int roll = random.Next(1, 101);

        Console.WriteLine($"Enemy attack roll: {roll} / Chance: {attackChance}%");

        if (roll <= attackChance)
        {
            int enemyPower = ThreatLevel * 2 + Level * 2;

            Console.WriteLine();
            Console.WriteLine("Enemy attack!");
            Console.WriteLine($"Enemy Power: {enemyPower}");
            Console.WriteLine($"Your army power: {GetArmyPower()}");

            ResolveEnemyAttack(enemyPower);
        }
        else
        {
            Console.WriteLine();
        }

    }

    public void ResolveEnemyAttack(int enemyPower)
    {
        int armyPower = GetArmyPower();

        if (armyPower >= enemyPower)
        {
            int goldReward = enemyPower / 2;
            int ironReward = enemyPower / 4;

            Gold += goldReward;
            Iron += ironReward;

            Console.WriteLine();
            Console.WriteLine("Your army defeted the enemy kingdom successfully");
            Console.WriteLine($"Loot gained: +{goldReward} gold, +{ironReward} iron ores");

            ReduceThreat(15, 25);
        }
        else
        {
            int goldLost = enemyPower / 4;
            int foodLost = enemyPower / 2;

            Gold -= goldLost;
            Food -= foodLost;
            Population -= 1;

            if (Gold < 0)
            {
                Gold = 0;
            }

            if (Food < 0)
            {
                Food = 0;
            }

            if (Population < 0)
            {
                Population = 0;
            }

            Console.WriteLine();
            Console.WriteLine("Your army failed to stop the enemy troops");
            Console.WriteLine($"Lost: {goldLost} gold, {foodLost} food, 1 population");

            ReduceThreat(5, 10);

            if (Population <= 0)
            {
                IsGameOver = true;

                Console.WriteLine();
                Console.WriteLine("All people have died");
                Console.WriteLine("GAME OVER");
            }
        }
    }

    private void ReduceThreat(int minAmount, int maxAmount)
    {
        Random random = new Random();

        int threatReduction = random.Next(minAmount, maxAmount + 1);

        ThreatLevel -= threatReduction;

        if (ThreatLevel < 0)
        {
            ThreatLevel = 0;
        }

        Console.WriteLine($"Threat reduced by {threatReduction}");
        Console.WriteLine($"Current Threat Level: {ThreatLevel}");
    }
}