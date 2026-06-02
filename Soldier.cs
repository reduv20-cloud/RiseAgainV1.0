public class Soldier
{
    public string Type { get; set; }
    public int Level { get; set; }
    public int Count { get; set; }

    public Soldier()
    {
        Type = " ";
        Level = 1;
        Count = 0;
    }

    public Soldier(string type, int level, int count)
    {
        Type = type;
        Level = level;
        Count = count;
    }
}