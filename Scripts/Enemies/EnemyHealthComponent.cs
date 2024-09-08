using Godot;
using System;

public partial class EnemyHealthComponent : Node
{
	[Export] private int health = 10;
	[Export] private Color hitColor;
	private Sprite2D sprite; 
	private Color baseColor;
	private Timer hitTimer;
	public bool daed = false;

    public override void _Ready()
    {
		sprite = GetParent().GetNode<Sprite2D>("Body");
		hitTimer = GetNode<Timer>("HitTimer");
        baseColor = sprite.Modulate;
    }
    public void IncrementHealth(int amount)
	{
		health += amount; 
		ShowHit(); 

		if (health <= 0)
		{
			daed = true;
			Die();
		}
	}

	private void ShowHit()
	{
		sprite.Modulate = hitColor;
		hitTimer.Start(); 
	}

	private void HitTimerOut() 
	{
		sprite.Modulate = baseColor;
	}

	private void Die()
	{
		GetParent().QueueFree();
	}


}
