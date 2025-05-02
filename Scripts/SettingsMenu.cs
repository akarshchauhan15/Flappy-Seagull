using Godot;

public partial class SettingsMenu : Panel
{
<<<<<<< Updated upstream
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
=======
    Button SoundButton;
    Hud hud;
    Control Cont;
    public override void _Ready()
    {
        hud = GetParent<Hud>();
        Cont = GetNode<Control>("Scroll/Control");

        Button ExitButton = GetNode<Button>("ExitButton");
        ExitButton.Pressed += OnSettingsExited;

        Button ResetButton = GetNode<Button>("ResetButton");
        ResetButton.Pressed += OnResetButtonPressed;

        Button ResetAccpted = GetNode<Button>("ResetConfirmationPanel/AcceptButton");
        ResetAccpted.Pressed += OnSettingsReset;

        Button ResetDeclined = GetNode<Button>("ResetConfirmationPanel/DeclineButton");
        ResetDeclined.Pressed += GetNode<Panel>("ResetConfirmationPanel").Hide;

        SoundButton = GetNode<Button>("Scroll/Control/Sound/SoundButton");
        SoundButton.Toggled += SoundButtonToggled;
    } 
    public void SetSettings()
    {
        Main.timer.WaitTime = ConfigController.LoadSliderSettings("Pipes", "Rate", Cont.GetNode<HSlider>("Pipes/RateSlider"));

        Pipe.Speed = ConfigController.LoadSliderSettings("Pipes", "Speed", Cont.GetNode<HSlider>("Pipes/SpeedSlider"));
        Pipe.Gap = ConfigController.LoadSliderSettings("Pipes", "Gap", Cont.GetNode<HSlider>("Pipes/GapSlider"));
>>>>>>> Stashed changes

        Player.Gravity = new Vector2(0, ConfigController.LoadSliderSettings("Player", "Gravity", Cont.GetNode<HSlider>("Player/GravitySlider")));
        Player.JumpVelocity = ConfigController.LoadSliderSettings("Player", "Jump", Cont.GetNode<HSlider>("Player/JumpSlider"));

        Player.PlaySound = (bool)ConfigController.config.GetValue("Sound", "Play");
    }
    public void UpdateSettings()
    {
<<<<<<< Updated upstream
        ConfigController.SaveSetting("Difficulty", "Speed", GetNode<HSlider>("Panel/Difficulty/SpeedSlider").Value);
        ConfigController.SaveSetting("Difficulty", "Gap", GetNode<HSlider>("Panel/Difficulty/GapSlider").Value);
        ConfigController.SaveSetting("Difficulty", "Rate", GetNode<HSlider>("Panel/Difficulty/RateSlider").Value);

        ConfigController.SaveSetting("Player", "Jump", GetNode<HSlider>("Panel/Player/JumpSlider").Value);
        ConfigController.SaveSetting("Player", "Gravity", GetNode<HSlider>("Panel/Player/GravitySlider").Value);

        ConfigController.SaveSetting("Sound", "Play", GetNode<CheckButton>("Panel/Sound/CheckButton").ButtonPressed);
=======
        ConfigController.SaveSetting("Pipes", "Speed", Cont.GetNode<HSlider>("Pipes/SpeedSlider").Value);
        ConfigController.SaveSetting("Pipes", "Gap", Cont.GetNode<HSlider>("Pipes/GapSlider").Value);
        ConfigController.SaveSetting("Pipes", "Rate", Cont.GetNode<HSlider>("Pipes/RateSlider").Value);

        ConfigController.SaveSetting("Player", "Jump", Cont.GetNode<HSlider>("Player/JumpSlider").Value);
        ConfigController.SaveSetting("Player", "Gravity", Cont.GetNode<HSlider>("Player/GravitySlider").Value);

        ConfigController.SaveSetting("Sound", "Play", Cont.GetNode<Button>("Sound/SoundButton").ButtonPressed);
    }
    private void OnSettingsExited()
    {
        UpdateSettings();
        SetSettings();
        if (!hud.Anim.IsPlaying()) hud.Anim.Play("CloseSettings");
    }
    private void OnSettingsReset()
    {
        ConfigController.ResetSettings();
        SetSettings();
        GetNode<Panel>("ResetConfirmationPanel").Hide();
        GetNode<Panel>("ResetConfirmationPanel").GrabFocus();
    }
    private void SoundButtonToggled(bool Value)
    {
        if (!Value)
            SoundButton.Text = "Enable";
        else
            SoundButton.Text = "Disable";
>>>>>>> Stashed changes
    }
    private void OnResetButtonPressed() => hud.Anim.Play("OpenResetConfirmation");
}
