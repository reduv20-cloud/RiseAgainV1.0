using System.Net.Security;

public class Building
{
    public string Name { get; set; }
    public int Level { get; set; }

    public Building()
    {
        Name = "";
        Level = 1;
    }

    public Building (string name, int level)
    {
        Name = name;
        Level = level;
    }
}