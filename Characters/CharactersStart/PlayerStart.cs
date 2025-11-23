using Godot;
using System;
using Soccer;
[GlobalClass]
public partial class PlayerStart : Node
{
	[Signal]
	public delegate void StateTransitionRequestedEventHandler(State playerStart);
	
	public Player _player;
	public AnimationPlayer AnimationPlayer;
	public void SetUp(Player player1)
	{
		_player = player1;
		AnimationPlayer = _player.AnimationPlayer;
	}
	
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
