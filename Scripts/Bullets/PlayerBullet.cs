using Godot;
using System;

public partial class PlayerBullet : BaseBullet
{
	public override void _Ready()
	{
		base._Ready();
	}
	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		QueueFree();
	}

	private void OnBodyEntered(Node body)
	{
		if (body.IsInGroup("Enemy"))
		{
			var enemy = (BaseEnemy)body;

			var health = enemy.GetNode<EnemyHealthComponent>("HealthComp");
			if (health != null)
			{
				health.IncrementHealth(damage);
				if (health.daed == true)
				{
					var gm = GetNode("/root/Gm");
					gm.EmitSignal("ScoreUpdate", enemy.ScoreAmount);
				}
			}
			QueueFree();
		}
	}
}
