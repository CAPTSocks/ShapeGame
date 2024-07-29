using Godot;
using System;
using System.Runtime.Versioning;

public partial class PlayerHealthComponent : Node2D
{
	[Export]
	private int health = 100;
	private float maxHealth;
	[Signal] public delegate void HealthChangedEventHandler(int currentHealthEvt);


	public override void _Ready()
	{
		maxHealth = health;
	}

	public void IncrementHealth(int incrementAmount)
	{
		health += incrementAmount;
		Mathf.Clamp(health, 0, maxHealth);
		EmitSignal(SignalName.HealthChanged, health);
		if (health == 0)
		{
			Die();
		}

	}

	private void Die()
	{
		//Signal to die! RIP
		GD.Print("You Died");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
