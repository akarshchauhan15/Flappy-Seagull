using Godot;
using Godot.NativeInterop;
using System;

public partial class Player : CharacterBody2D
{
    [Signal]
    public delegate void FellEventHandler(bool t);

    public static float JumpVelocity = 500;
    public static Vector2 Gravity = new Vector2(0, 980);

    Main main;
    Timer timer;
    AnimationPlayer anim;
    Tween tween;
    AudioStreamPlayer flap;
    public static AudioStreamPlayer score;

    public static bool alive = true;

    public static bool PlaySound = true;

    public override void _Ready()
    {
        main = GetParent<Main>();
        anim = GetNode<AnimationPlayer>("AnimationPlayer");
        timer = GetNode<Timer>("RespawnTimer");

        flap = GetNode<AudioStreamPlayer>("Sound/Flap");
        score = GetNode<AudioStreamPlayer>("Sound/Score");

        anim.Play("default");

        Fell += main.StopTimer;
        main.GetNode<Area2D>("Floor").BodyEntered += FellToFloor;
    }
    public override void _PhysicsProcess(double delta)
    {
        if (main.playing)
        {
            Velocity += Gravity * (float)delta;

            if (Input.IsActionJustPressed("Flap"))
            { 
                Velocity = new Vector2(0, -JumpVelocity);
                if (PlaySound)
                flap.Play();
            }
        }
        if (!alive)
        {
            Velocity += Gravity * (float) delta;
            if (timer.TimeLeft <= 0 && Position.Y > 1500)
            {
                tween.Kill();
                Respawn();
            }
        }

        Rotation = (new Vector2(1000, Velocity.Y)).Angle();
        MoveAndSlide();
    }
    public void GameOver(bool DeathByPipe)
    {
        timer.Start();

        if (DeathByPipe && PlaySound)
            GetNode<AudioStreamPlayer>("Sound/Bonk").Play();

        GetNode<AnimatedSprite2D>("AnimatedSprite2D").Stop();
        alive = false;

        tween = CreateTween();
        tween.TweenProperty(this, "rotation_degrees", 500, 3).AsRelative().SetTrans(Tween.TransitionType.Quad);
        
    }
    public void Respawn()
    {
        alive = true;
        Velocity = Vector2.Zero;

        GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play();
        anim.Play("respawn");
    }
    public void FellToFloor(Node2D body)
    {
        if (main.playing)
        {
            EmitSignal(SignalName.Fell, true);
            GameOver(false);
        }
    }
    private void Finished()
    {
        main.HUD.GetNode<Control>("StartMenu").Show();
    }
}
