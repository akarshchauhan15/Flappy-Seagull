using Godot;
using System;

public partial class Pipe : Node2D
{
    [Signal]
    public delegate void AddScoreEventHandler();
    [Signal]
    public delegate void GameOverEventHandler(bool t);
  
    public static float Speed = 350;
    public static float Gap = 0;

    bool Entered = false;

    Main main;
    Player player;
    public override void _Ready()
    {
        main = GetNode<Main>("../../../Main");
        player = GetNode<Player>("../../Player");

        AddScore += main.HUD.IncreaseScore;
        GameOver += main.StopTimer;
        GameOver += player.GameOver;

        GetNode<Area2D>("Upper").Translate(new Vector2(0, -Gap));
        GetNode<Area2D>("Lower").Translate(new Vector2(0, Gap));
    }
    public override void _PhysicsProcess(double delta)
    {
        Position -= new Vector2(Speed, 0) * (float)delta;
    }
    private void Kill()
    {
        QueueFree();
    }
    private void IncreaseScore(Node2D body)
    {
        if (!Entered && Player.alive)
        {
            EmitSignal(SignalName.AddScore);
            Entered = true;
        }
    }
    private void StopMovement(Node2D body) 
    {
        if (main.playing)
        EmitSignal(SignalName.GameOver, true);
    }
}
