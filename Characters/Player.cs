using Godot;
using Soccer.Characters;

public partial class Player : CharacterBody2D
{
    [Export]
    private float Speed;
    [Export]
    private AnimationPlayer AnimationPlayer;
    
    [Export]
    private ControlScheme _controlScheme;
    [Export]
    private Sprite2D _sprite2D;
    
    private Vector2 heading=Vector2.Right;
    public override void _Process(double delta)
    {
       // Vector2 vector2 = Input.GetVector("p1_left", "p1_right", "p1_up", "p1_down");
       if (_controlScheme == ControlScheme.CPU)
       {
          return;
       }
       else
       {
           Vector2 vector2 = KeyUtils.GetMovementVector(_controlScheme);
           Velocity = vector2 * Speed;
       }
       
        SetMovementAnimation();
    
    
        SetHeading();
        MoveAndSlide();
        base._Process(delta);
    }

    private void SetHeading()
    {
        if (Velocity.X>0)
        {
            heading=Vector2.Right;
            _sprite2D.SetFlipH(false);
        }
        else if (Velocity.X<0)
        {
            heading=Vector2.Left;
            _sprite2D.SetFlipH(true);
        }
    }

    private void SetMovementAnimation()
    {
        if (Velocity.Length() > 0.1f)
        {
            AnimationPlayer.Play("run");
        }
        else
        {
            AnimationPlayer.Play("idle");
        }
    }
}
