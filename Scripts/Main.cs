using Godot;
using GodotPlugins.Game;
using System;
using System.Text.RegularExpressions;

public partial class Main : CanvasLayer
{
    public static Timer timer;
    PackedScene Pipe;
    Random random;
    Node2D PipeContainer;

    public Hud HUD;

    public bool playing = false;

    public override void _Ready()
    {
        Pipe = (PackedScene) ResourceLoader.Load("res://Scenes/pipe.tscn");
        random = new Random();

        PipeContainer = GetNode<Node2D>("Pipes");
        timer = GetNode<Timer>("PipeTimer");
        HUD = GetNode<Hud>("HUD");

        HUD.GetNode<Button>("StartMenu/Button").Pressed += Play;
    }
    private void AddPipe()
    {
        var pipe = Pipe.Instantiate();
        PipeContainer.AddChild(pipe);

        pipe.SetDeferred("global_position", new Vector2(1400, (float) (random.NextDouble()-0.5) * 400 + 360));
        timer.Start();
    }
    public void Play()
    {
        HUD.GetNode<Control>("StartMenu").Hide();
        HUD.Score = 0;
        HUD.ScoreLabel.Text = "0";

        playing = true;
        AddPipe();
    }
    public void StopTimer(bool t)
    {
        timer.Stop();
        playing = false;
        HUD.AddScore();
    }
}
