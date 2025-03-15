using Godot;
using Godot.Collections;
using System;

public partial class Hud : CanvasLayer
{
    public Label ScoreLabel;
    Label QuitLabel;
    Tween tween;

    public int Score = 0;
    double time = 0;

    public override void _Ready()
    {
        ScoreLabel = GetNode<Label>("Overlay/ScoreLabel");
        QuitLabel = GetNode<Label>("QuitLabel");
    }
    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionJustPressed("Exit"))
        {
            tween = CreateTween();
            tween.Finished += Exit;
            tween.TweenProperty(QuitLabel, "modulate:a", 1, 2);

        }
        else if (Input.IsActionJustReleased("Exit"))
        {
            tween.Kill();
            QuitLabel.Set("modulate", new Color(1, 1, 1, 0));
        }
    }
    public void IncreaseScore()
    {
        if (Player.PlaySound)
        Player.score.Play();
        Score++;
        ScoreLabel.Text = Score.ToString();
    }   
    private void OnSettingsPressed()
    {
        GetNode<Control>("StartMenu").Hide();
        Panel Settings = GetNode<Panel>("SettingsMenu");
        Settings.Show();
        Settings.GetNode<HSlider>("Panel/Difficulty/SpeedSlider").GrabFocus();
    }
    private void Exit() => GetTree().Quit();
}
