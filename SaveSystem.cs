using System.Text.Json;

public static class SaveSystem
{
    private static string saveFilePath = Path.Combine(AppContext.BaseDirectory,"savegame.json");
    
    public static void SaveGame(Kingdom kingdom)
    {
        string json = JsonSerializer.Serialize(kingdom, new JsonSerializerOptions
            {
            WriteIndented = true
        });
        File.WriteAllText(saveFilePath, json);
    }

    public static Kingdom LoadGame()
    {
        string json = File.ReadAllText(saveFilePath);

        Kingdom? kingdom = JsonSerializer.Deserialize<Kingdom>(json);

        if (kingdom == null)
        {
            return new Kingdom();
        }

        kingdom.NormalizeBuildingNames();
        kingdom.EnsureBuildingsExist();

        return kingdom;
    }

    public static bool SaveExists()
    {
        return File.Exists(saveFilePath);
    }
}