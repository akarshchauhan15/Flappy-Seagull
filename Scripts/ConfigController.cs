using Godot;
using System;

public partial class ConfigController : Node
{
    public static ConfigFile config = new ConfigFile();
    public static ConfigFile preset = new ConfigFile();

    public static string path = "res://settings.ini";

    public override void _Ready()
    {
        if (!FileAccess.FileExists(path))
            ResetSettings();
        else
            config.Load(path);

        preset.Load("res://Configurations/SettingsConfiguration.cfg");
    }
    public static void ResetSettings()
    {
        config.SetValue("Pipes", "Speed", 2.0);
        config.SetValue("Pipes", "Interval", 2.0);
        config.SetValue("Pipes", "Rate", 2.0);

        config.SetValue("Player", "Gravity", 2.0);
        config.SetValue("Player", "Jump", 2.0);

        config.SetValue("Sound", "Play", true);

        config.Save(path);
    }
    public static void SaveSetting(string Section, string Key, Variant Value)
    {
        config.SetValue(Section, Key, Value);
        config.Save(path);
    }
    public static float LoadSliderSettings(string Section, string Key, HSlider Slider) 
    {
        Slider.Value = (float)config.GetValue(Section, Key);
        return (float) preset.GetValue(Key, (config.GetValue(Section, Key).ToString()));
    }
}
