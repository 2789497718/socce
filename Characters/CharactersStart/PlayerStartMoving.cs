using Godot;
using System;
using Soccer;
using Soccer.Characters;

public partial class PlayerStartMoving : PlayerStart
{
	private Vector2 vector2;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
		// Vector2 vector2 = Input.GetVector("p1_left", "p1_right", "p1_up", "p1_down");
		if (_player._controlScheme == ControlScheme.CPU)
		{
			return;
		}
		else
		{
			UpdatePlayerVelocity();
		}
	   
		_player.SetMovementAnimation();
		
	}

	public void UpdatePlayerVelocity()
	{
		 vector2 = KeyUtils.GetMovementVector(_player._controlScheme);
		_player.Velocity = vector2 * _player.Speed;
		// 3. 状态检查
		// 同样使用 _player 来获取控制方案
		bool isShootPressed = KeyUtils.IsActionJustPressed(_player._controlScheme, KeyUtils.Action.SHOOT);
		if (isShootPressed)
		{
			GD.Print(vector2);
		}
		if (vector2!=Vector2.Zero  && isShootPressed)
		{
			GD.Print("EmitSignal");
			EmitSignal(SignalName.StateTransitionRequested, (int)State.TACKLING);
		}
	}
	
}
