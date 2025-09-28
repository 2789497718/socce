using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export]
    private float Speed;
    [Export]
    private AnimationPlayer AnimationPlayer;

    public override void _Process(double delta)
    {
        Vector2 vector2 = Input.GetVector("p1_left", "p1_right", "p1_up", "p1_down");
        Velocity = vector2 * Speed;
        if (Velocity.Length() > 0.1f)
        {
            AnimationPlayer.Play("run");
        }
        else
        {
            AnimationPlayer.Play("idle");
        }

        MoveAndSlide();
        base._Process(delta);
    }
}
