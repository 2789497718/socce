using Godot;
using System;
using Soccer;

public partial class PlayerStateRecovering : PlayerStart
{
	private const int DURATION_RECOVERING = 500;
	private ulong time_start_Recoveriug = Time.GetTicksMsec();


	public override void _EnterTree()
	{
		
		time_start_Recoveriug = Time.GetTicksMsec();
		_player.Velocity=Vector2.Zero;
		AnimationPlayer.Play("recover");
		base._EnterTree();
	}
	
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Time.GetTicksMsec()-time_start_Recoveriug> DURATION_RECOVERING)
		{
			EmitSignal(SignalName.StateTransitionRequested, (int)State.MOVING);
		}
	}
}
