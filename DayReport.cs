public class DayReport
{
    public int Day { get; set;  }
    public int FoodProduced { get; set; }
    public int WoodProduced { get; set; }
    public int StoneProduced { get; set; }
    public int IronProduced { get; set; }
    public int GoldProduced { get; set; }
    public int FoodConsumed { get; set; }
    public bool StarvationHappening { get; set; }
    public int MissingFood { get; set; }
    public int PeopleLostFromHunger { get; set; }
    public int ThreatIncrease { get; set; }
    public bool IsGameOver { get; set; }
    public List<string> Messages { get; set; } = new List<string>();

}