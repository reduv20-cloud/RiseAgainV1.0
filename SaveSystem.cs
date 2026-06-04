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

        kingdom!.NormalizeBuildingNames();
        kingdom.EnsureBuildingsExisti();

        if (kingdom == null)
        {
            return new Kingdom();
        }

        return kingdom;
    }

    public static bool SaveExists()
    {
        return File.Exists(saveFilePath);
    }
}