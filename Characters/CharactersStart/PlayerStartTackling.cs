using Godot;
using System;
using Soccer;

public partial class PlayerStartTackling : PlayerStart
{
	private const int DURATION_TACKLE = 200;
	private ulong time_start_tackle = Time.GetTicksMsec();


	public override void _EnterTree()
	{
		AnimationPlayer.Play("tackle");
		time_start_tackle = Time.GetTicksMsec();
		GD.Print("tackle");
		base._EnterTree();
	}
	
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Time.GetTicksMsec()-time_start_tackle> DURATION_TACKLE)
		{
			EmitSignal(SignalName.StateTransitionRequested, (int)State.RECOVERING);
		}
	}

}
