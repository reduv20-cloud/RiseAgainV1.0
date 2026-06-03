using System.IO.Pipes;

public class Game
{
    private Kingdom? kingdom;
    private bool isRunning = true;

    public void ShowMainMenu()
    {
        while (isRunning)
        {
            Console.Clear();

            Console.WriteLine("===============================");
            Console.WriteLine("           Rise Again");
            Console.WriteLine("===============================");
            Console.WriteLine("1. Start Game");
            Console.WriteLine("2. New Game");
            Console.WriteLine("0. Quit Game");
            Console.WriteLine("===============================");
            Console.Write("Choose an option: ");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    StartGame();
                    break;

                case "2":
                    NewGame();
                    break;

                case "0":
                    QuitGame();
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    private void StartGame()
    {
        if (SaveSystem.SaveExists())
        {
            kingdom = SaveSystem.LoadGame();

            Console.WriteLine();
            Console.WriteLine("Saved game loaded");
            Pause();

            ShowGameMenu();

        }

        else
        {
            Console.WriteLine();
            Console.WriteLine("No saved game found");
            Console.WriteLine("Start a new game first");
            Pause();
        }

    }

    private void NewGame()
    {
        kingdom = new Kingdom();

        SaveSystem.SaveGame(kingdom);

        Console.WriteLine();
        Console.WriteLine("New game created");
        Pause();

        ShowGameMenu();
    }

    private void QuitGame()
    {
        isRunning = false;

        Console.WriteLine();
        Console.WriteLine("Game closed.");

    }

    private void ShowGameMenu()
    {
        bool isInGame = true;

        while(isInGame)
        {
            Console.Clear();

            kingdom!.ShowStatus();


            Console.WriteLine();
            Console.WriteLine("Actions:");
            Console.WriteLine("1. Next Day");
            Console.WriteLine("2. Grow Population");
            Console.WriteLine("3. Level Up Kingdom");
            Console.WriteLine("4. Upgrade Farm");
            Console.WriteLine("5. Upgrade Barracks");
            Console.WriteLine("6. Upgrade Mine");
            Console.WriteLine("7. Upgrade Stone Quarry");
            Console.WriteLine("8. Upgrade Lumber Mill");
            Console.WriteLine("9. Show LevelUp Requirements");
            Console.WriteLine("10. Train Soldier");
            Console.WriteLine("11. Upgrade Training Barracks");
            Console.WriteLine("12. Upgrade Iron Mine");
            Console.WriteLine("0. Save and return to main menu");
            Console.WriteLine();

            Console.Write("Choose an option: ");
            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    kingdom!.NextDay();
                    SaveSystem.SaveGame(kingdom);
                    Pause();
                    break;

                case "2":
                    kingdom!.GrowPopulation();
                    SaveSystem.SaveGame(kingdom);
                    Pause();
                    break;

                case "3":
                    kingdom!.LevelUpKingdom();
                    SaveSystem.SaveGame(kingdom);
                    Pause();
                    break;

                case "4":
                    kingdom!.UpgradeBuiling(kingdom.Farms);
                    SaveSystem.SaveGame(kingdom);
                    Pause();
                    break;

                case "5":
                    kingdom!.UpgradeBuiling(kingdom.Barracks);
                    SaveSystem.SaveGame(kingdom);
                    Pause();
                    break;

                case "6":
                    kingdom!.UpgradeBuiling(kingdom.Mine);
                    SaveSystem.SaveGame(kingdom);
                    Pause();
                    break;

                case "7":
                    kingdom!.UpgradeBuiling(kingdom.StoneQuarry);
                    SaveSystem.SaveGame(kingdom); ;
                    Pause();
                    break;
                
                case "8":
                    kingdom!.UpgradeBuiling(kingdom.LumberMill);
                    SaveSystem.SaveGame(kingdom);
                    Pause();
                    break;

                case "9":
                    kingdom!.ShowLevelUpRequirements();
                    Pause();
                    break;

                case "10":
                    ShowTrainSoldierMenu();
                    SaveSystem.SaveGame(kingdom);
                    Pause();
                    break;

                case "11":
                    kingdom!.UpgradeBuiling(kingdom.TrainingBarracks);
                    SaveSystem.SaveGame(kingdom);
                    Pause();
                    break;

                case "12":
                    kingdom!.UpgradeBuiling(kingdom!.IronMine);
                    SaveSystem.SaveGame(kingdom);
                    Pause();
                    break;

                case "0":
                    SaveSystem.SaveGame(kingdom);
                    isInGame = false;
                    break;

                default:
                    Console.WriteLine("Invalid option");
                    Pause();
                    break;

            }
        }
    }

    private void ShowTrainSoldierMenu()
    {
        Console.Clear();

        Console.WriteLine("===============================");
        Console.WriteLine("            TRAIN SOLDIER");
        Console.WriteLine("===============================");
        Console.WriteLine($"Training Barracks Level: {kingdom!.TrainingBarracks.Level}");
        Console.WriteLine();
        Console.WriteLine("Choose Soldier Type:");
        Console.WriteLine("1. Archer");
        Console.WriteLine("2. Swordsman");
        Console.WriteLine("3. Infantry");
        Console.WriteLine("4. Back");
        Console.WriteLine();

        Console.WriteLine("Choosde an option:");
        string? typeInput = Console.ReadLine();

        string soldierType;

        switch (typeInput)
        {
            case "1":
                soldierType = "Archer";
                break;

            case "2":
                soldierType = "Swordsman";
                break;

            case "3":
                soldierType = "Infantry";
                break;

            case "4":
                return;

            default:
                Console.WriteLine("Invalid soldier type");
                return;
        }

        Console.WriteLine();
        Console.WriteLine($"Choose soldier level between 1 and {kingdom.TrainingBarracks.Level}");
        Console.WriteLine("Soldier level: ");

        string? levelInput = Console.ReadLine();

        bool isValidLevel = int.TryParse(levelInput, out int soldierLevel);

        if (!isValidLevel)
        {
            Console.WriteLine("Invalid Level");
            return;
        }

        if (soldierLevel < 1 || soldierLevel > kingdom.TrainingBarracks.Level)
        {
            Console.WriteLine($"Invalid Level. You can train soldiers from lelev 1 to level {kingdom.TrainingBarracks.Level}");
            return;
        }

        kingdom.TrainSoldier(soldierType, soldierLevel);
    }

    private void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("Press ENTER to continue...");
        Console.ReadLine();
    }

}