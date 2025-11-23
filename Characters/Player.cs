using Godot;
using Soccer;
using Soccer.Characters;

public partial class Player : CharacterBody2D
{
	[Export]
	public float Speed;
	
	[Export]
	public AnimationPlayer AnimationPlayer;
	
	[Export]
	public ControlScheme _controlScheme;
	[Export]
	private Sprite2D _sprite2D;
	
	private Vector2 heading=Vector2.Right;

	private PlayerStart current_start;

	private PlayerStateFactory PlayerStateFactory=new PlayerStateFactory();


	public override void _Ready()
	{
		SwitchStart(State.MOVING);
		base._Ready();
	}

	public void SwitchStart(State state)
	{
		if (state!=null)
		{
			if (current_start!=null)
			{
				current_start.StateTransitionRequested -= SwitchStart;
				current_start.QueueFree();
			}
			current_start = PlayerStateFactory.GetFreshStart(state);
			current_start.SetUp(this);
			current_start.StateTransitionRequested += SwitchStart;
			current_start.Name = "PlayerStart" + state;
			CallDeferred("add_child", current_start);
		}
	}

	public override void _Process(double delta)
	{
		SetHeading();
		MoveAndSlide();
		base._Process(delta);
	}
	
	

	/// <summary>
	/// 转身
	/// </summary>
	public void SetHeading()
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

	public void SetMovementAnimation()
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
