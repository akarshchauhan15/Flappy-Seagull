using Godot;
using System;
using System.Globalization;

public partial class Hud : CanvasLayer
{
    public Label ScoreLabel;
    Label QuitLabel;
    Tween tween;
<<<<<<< Updated upstream
=======
    public AnimationPlayer Anim;
>>>>>>> Stashed changes

    public int Score = 0;
    double time = 0;

    public override void _Ready()
    {
        ScoreLabel = GetNode<Label>("Overlay/ScoreLabel");
        QuitLabel = GetNode<Label>("QuitLabel");

        GetNode<Button>("ScorePanel/ExitButton").Pressed += GetNode<Panel>("ScorePanel").Hide;
        ScoreController.LoadScores();

        SetScoreList();
    }
    public override void _Process(double delta)
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
    public void AddScore()
    {
        ScoreController.AddScores(Score);
        SetScoreList();
    }
    private void SetScoreList()
    {
        if (ScoreController.Scores.Count == 0)
        {
            GetNode<Label>("ScorePanel/NoScores").Show();
            return;
        }
        Label ScoreList = GetNode<Label>("ScorePanel/Scores");
        Label DateList = GetNode<Label>("ScorePanel/Dates");
        ScoreList.Text = "";
        DateList.Text = "";
        string Helper = "on";
        
        int Place = 1;
        DateTime Today = DateTime.Now;

        foreach (ScoreEntry Score in ScoreController.Scores)
        {
            string DisplayDate;
            int Difference = Today.Day - Score.Date.Day;

            switch (Difference)
            {
                case 0:
                    DisplayDate = "Today";
                    Helper = "at";
                    break;
                case 1:
                    DisplayDate = "Yesterday";
                    break;
                default:
                    DisplayDate = Score.Date.ToString("dd MMM", new CultureInfo("en-GB"));
                    break;
            }

            ScoreList.Text += $"{Score.Score}\n";
            DateList.Text += $"{DisplayDate} {Helper} {Score.Date.ToString("t", new CultureInfo("en-GB"))}\n";

            Place++;
        }
        Label HighScore = GetNode<Label>("ScorePanel/HighScore");

        HighScore.Text = ScoreController.Scores[0].Score.ToString();
    }
    private void OnSettingsPressed()
    {
        GetNode<Control>("StartMenu").Hide();
        Panel Settings = GetNode<Panel>("SettingsMenu");
        Settings.Show();
        Settings.GetNode<HSlider>("Panel/Difficulty/SpeedSlider").GrabFocus();
    }
    private void Exit() => GetTree().Quit();
<<<<<<< Updated upstream
=======
    private void OnScorePressed() {if (!Anim.IsPlaying()) Anim.Play("OpenScores"); }
    private void OnScoreClosed() {if (!Anim.IsPlaying()) Anim.Play("CloseScores"); }
>>>>>>> Stashed changes
}
