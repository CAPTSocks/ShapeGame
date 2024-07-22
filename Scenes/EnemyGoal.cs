using Godot;
using System;

public partial class EnemyGoal : Node2D
{
	private void OnBodyEntered(Node body)
	{
		if (body.IsInGroup("Enemy"))
		{
			var enemy = body.GetParent().GetNode<BaseEnemy>(body.Name.ToString());
			enemy?.ReachedEndOfScreen();
		}
	}
}
