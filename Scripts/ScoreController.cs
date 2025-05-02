using Godot;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public partial class ScoreController : Node
{
    public static List<ScoreEntry> Scores = new List<ScoreEntry>();
    public static string Path = "res://scores.json";

    public static void AddScores(int Score)
    {
        Scores.Add(new ScoreEntry(Score));
        Scores = Scores.OrderByDescending(entry => entry.Score).Take(10).ToList();

        SaveScores();
    }
    public static void LoadScores()
    {
        if (!FileAccess.FileExists(Path))
            return;
        
        FileAccess File = FileAccess.Open(Path, FileAccess.ModeFlags.Read);
        string Json = File.GetAsText();
        File.Close();

        Scores = JsonSerializer.Deserialize<List<ScoreEntry>>(Json);
    }
    private static void SaveScores()
    {
        var Json = JsonSerializer.Serialize(Scores);
        FileAccess File = FileAccess.Open(Path, FileAccess.ModeFlags.Write);
        File.StoreString(Json);
        File.Close();
    }
}