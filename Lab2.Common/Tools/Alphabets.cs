namespace Lab2.Common.Tools;

public static class Alphabets
{
    public static readonly (string Letters, double Frequencies) En = ("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 0.065);

    public static Dictionary<string, (string, double)> All = new()
    {
        { "En", En }
    };
}