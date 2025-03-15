using Godot;
using System;

public partial class SettingsMenu : Panel
{
    public void OnSettingsExited()
    {
        UpdateSettings();
        SetSettings();
        Hide();
        GetNode<Control>("../StartMenu").Show();
    }
    public void SetSettings()
    {
        Main.timer.WaitTime = (float)ConfigController.LoadSliderSettings("Difficulty", "Rate");

        Pipe.Speed = (float)ConfigController.LoadSliderSettings("Difficulty", "Speed");
        Pipe.Gap = (float)ConfigController.LoadSliderSettings("Difficulty", "Gap");

        Player.Gravity = new Vector2(0, (float)ConfigController.LoadSliderSettings("Player", "Gravity"));
        Player.JumpVelocity = (float)ConfigController.LoadSliderSettings("Player", "Jump");

        Player.PlaySound = (bool)ConfigController.config.GetValue("Sound", "Play");
    }
    public void UpdateSettings()
    {
        ConfigController.SaveSetting("Difficulty", "Speed", GetNode<HSlider>("Panel/Difficulty/SpeedSlider").Value);
        ConfigController.SaveSetting("Difficulty", "Gap", GetNode<HSlider>("Panel/Difficulty/GapSlider").Value);
        ConfigController.SaveSetting("Difficulty", "Rate", GetNode<HSlider>("Panel/Difficulty/RateSlider").Value);

        ConfigController.SaveSetting("Player", "Jump", GetNode<HSlider>("Panel/Player/JumpSlider").Value);
        ConfigController.SaveSetting("Player", "Gravity", GetNode<HSlider>("Panel/Player/GravitySlider").Value);

        ConfigController.SaveSetting("Sound", "Play", GetNode<CheckButton>("Panel/Sound/CheckButton").ButtonPressed);
    }
}
