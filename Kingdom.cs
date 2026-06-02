public class Kingdom
{
    public int Day { get; set; } = 1;
    public int Level { get; set; } = 1;

    public int Population { get; set; } = 10;
    public int Food { get; set; } = 50;
    public int Wood { get; set; } = 30;
    public int Stone { get; set; } = 20;
    public int Gold { get; set; } = 20;

    public Building Barracks { get; set; }
    public Building Farms { get; set; }
    public Building Mine { get; set; }
    public Building StoneQuarry { get; set; }
    public Building LumberMill { get; set; }
    public Building TrainingBarracks { get; set; }

    public bool IsGameOver { get; set; } = false;

    public Kingdom()
    {
        Barracks = new Building("Barracks", 1);
        Farms = new Building("Farms", 1);
        Mine = new Building("Mine", 1);
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
        Console.WriteLine();
        Console.WriteLine($"Population: {Population} / {GetMaxPopulation()}");
        Console.WriteLine($"Food: {Food}");
        Console.WriteLine($"Daily rations needed: {GetDailyFoodConsumtion()}");
        Console.WriteLine($"Wood: {Wood}");
        Console.WriteLine($"Stone: {Stone}");
        Console.WriteLine($"Gold: {Gold}");

        Console.WriteLine();
        Console.WriteLine("Buildings:");
        Console.WriteLine($"{Barracks.Name}: {Barracks.Level}");
        Console.WriteLine($"{Farms.Name}: {Farms.Level}");
        Console.WriteLine($"{Mine.Name}: {Mine.Level}");
        Console.WriteLine($"{LumberMill.Name}: {LumberMill.Level}");
        Console.WriteLine($"{StoneQuarry.Name}: {StoneQuarry.Level}");

        Console.WriteLine();
        Console.WriteLine($"{TrainingBarracks.Name}: Level {TrainingBarracks.Level}");
        Console.WriteLine("Army:");

        if ( Army.Count == 0)
        {
            Console.WriteLine("No Soldier Trained");
        }
        else
        {
            foreach (Soldier soldier in Army)
            {
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

        int foodCost = GetSoldierCost(10, soldierLevel);
        int goldCost = GetSoldierCost(4, soldierLevel);
        int woodCost = 0;
        int stoneCost = 0;

        if (soldierType == "Archer")
        {
            woodCost = GetSoldierCost(6, soldierLevel);
        }
        else if (soldierType == "Swordsman")
        {
            stoneCost = GetSoldierCost(5, soldierLevel);
            goldCost = GetSoldierCost(8, soldierLevel);
        }
        else if (soldierType == "Infantry")
        {
            stoneCost = GetSoldierCost(3, soldierLevel);
            woodCost = GetSoldierCost(2, soldierLevel);
        }
        else
        {
            Console.WriteLine("Invalid Soldier Type.");
            return;
        }

        Console.WriteLine($"Train {soldierType} Level {soldierLevel}");
        Console.WriteLine($"Cost: {foodCost} food, {goldCost} gold, {woodCost} wood, {stoneCost} stone");
        Console.WriteLine();

        if (Food < foodCost || Wood < woodCost || Stone < stoneCost || Gold < goldCost)
        {
            Console.WriteLine("Not enought resources to train soldier");
            return;
        }

        Food -= foodCost;
        Wood -= woodCost;
        Stone -= stoneCost;
        Gold -= goldCost;

        Soldier? exisistingSoldierGroup = Army.FirstOrDefault(
            soldier => soldier.Type == soldierType && soldier.Level == soldierLevel     //cde retinut procedura de atribuire grup
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
        int goldProduced = Mine.Level * 4;

        Food += foodProduced;
        Wood += woodProduced;
        Stone += stoneProduced;
        Gold += goldProduced;

        Console.WriteLine($"Farms produced +{foodProduced} food");
        Console.WriteLine($"Lumber Mill produced +{woodProduced} wood");
        Console.WriteLine($"Stone Quarry produced +{stoneProduced} stone");
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
            Mine,
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
}