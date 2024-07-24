using Godot;
using System;

public partial class PlayerBullet : BaseBullet
{
    public override void _Ready()
    {
        
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
			var gm = GetNode("/root/Gm");
			gm.EmitSignal("ScoreUpdate", enemy.ScoreAmount);
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
