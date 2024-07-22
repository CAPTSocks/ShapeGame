using Godot;
using System;

public partial class PlayerBullet : BaseBullet
{
	private void OnBodyEntered(Node body)
	{
		if (body.IsInGroup("Enemy"))
		{
			//var health = body.GetNode<HealthComponent>("HealthComponent");
			//if (health != null)
			//{
			//health.IncrementHealth(damage)
			//}
			body.QueueFree();
			QueueFree();
		}
	}
}
