using Godot;
using System;
using System.Runtime.Versioning;

public partial class PlayerHealthComponent : Node2D
{
	[Export]
	private int health = 100;
	private float maxHealth;
	private bool playerIsDead = false;
	[Signal] public delegate void HealthChangedEventHandler(int currentHealthEvt);
	[Signal] public delegate void PlayerDiedEventHandler();


	public override void _Ready()
	{
		maxHealth = health;
	}

	public void IncrementHealth(int incrementAmount)
	{
		if (!playerIsDead)
		{
			health += incrementAmount;
			Mathf.Clamp(health, 0, maxHealth);
			EmitSignal(SignalName.HealthChanged, health);
			if (health <= 0)
			{
				Die();
			}
		}

	}

	private void Die()
	{
		//Signal to die! RIP
		playerIsDead = true;
		EmitSignal(SignalName.PlayerDied);
		GD.Print("You Died");
	}
}
