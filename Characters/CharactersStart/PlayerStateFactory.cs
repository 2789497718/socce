using Godot;
using System;
using System.Collections.Generic; // 推荐使用 System.Collections.Generic 而不是 Godot.Collections
using System.Diagnostics;
using Soccer;

public partial class PlayerStateFactory : Node
{
	
	private Dictionary<State, Func<PlayerStart>> _stateFactories=new Dictionary<State, Func<PlayerStart>>
	{
		// 当需要 MOVING 时，执行箭头后面的代码：new 出来一个新的
		{ State.MOVING, () => new PlayerStartMoving() },
		{ State.TACKLING, () => new PlayerStartTackling() },
		{ State.RECOVERING, () => new PlayerStateRecovering() }
	};
	
	public PlayerStart GetFreshStart(State state)
	{
		Debug.Assert(_stateFactories.ContainsKey(state),"该状态不存在！！！！！");
		return _stateFactories[state].Invoke();
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
