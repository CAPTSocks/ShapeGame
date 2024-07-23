using Godot;
using System;

public partial class EnemyBullet : BaseBullet
{
	private void OnBodyEntered(Node body)
	{
		if (body.Name == "Player")
		{
			var health = body.GetNode<PlayerHealthComponent>("HealthComponent");
			if (health != null)
			{
				health.IncrementHealth(damage);
			}
			QueueFree();
		}
	}
}
