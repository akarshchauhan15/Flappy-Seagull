using Godot;
using System;

public partial class ConfigController : Node
{
    public static ConfigFile config = new ConfigFile();
    public static ConfigFile preset = new ConfigFile();

    static string path = "res://settings.ini";

    public override void _Ready()
    {
        if (!FileAccess.FileExists(path))
        {
            config.SetValue("Difficulty", "Speed", 2.0);
            config.SetValue("Difficulty", "Gap", 2.0);
            config.SetValue("Difficulty", "Rate", 2.0);

            config.SetValue("Player", "Gravity", 2.0);
            config.SetValue("Player", "Jump", 2.0);

            config.SetValue("Sound", "Play", true);

            config.Save(path);
        }
        else
            config.Load(path);

        preset.Load("res://Configurations/SettingsConfiguration.cfg");
    }
    public static void SaveSetting(string Section, string Key, Variant Value)
    {
        config.SetValue(Section, Key, Value);
        config.Save(path);
    }
    public static Variant LoadSliderSettings(string Section, string Key) 
    {
        return ConfigController.preset.GetValue(Key, (ConfigController.config.GetValue(Section, Key).ToString()));
    }
}
