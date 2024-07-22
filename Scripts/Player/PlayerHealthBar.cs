using Godot;
using System;
using System.Xml.Schema;

public partial class PlayerHealthBar : Sprite2D
{
	private float maxHealth;
	private TextureProgressBar healthBar;
	[Export]
	private Texture2D fullHealth, HalfHealth, quarterHealth, lowHealth, flashHealthTexture;
	private bool flashHealth = false;
	private Timer timer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		healthBar = GetNode<TextureProgressBar>("HealthBar");
		timer = GetNode<Timer>("Timer");
		maxHealth = (float)healthBar.Value;
	}

	private void OnPlayerHealthChanged(int newHealthValue)
	{
		healthBar.Value = newHealthValue;
		Mathf.Clamp(healthBar.Value, 0, maxHealth);
		switch (healthBar.Value)
		{
			case var expression when healthBar.Value <= (10 / maxHealth) * 100:
				healthBar.TextureProgress = lowHealth;
				StartFlashLowHealth();
				break;

			case var expression when healthBar.Value <= (25 / maxHealth) * 100:
				healthBar.TextureProgress = quarterHealth;
				StopFlash();
				break;

			case var expression when healthBar.Value <= (50 / maxHealth) * 100:
				healthBar.TextureProgress = HalfHealth;
				StopFlash();
				break;

			default:
				healthBar.TextureProgress = fullHealth;
				StopFlash();
				break;
		}
	}

	private void StartFlashLowHealth()
	{
		if (flashHealth != true)
		{
			flashHealth = true;
			timer.Start();
		}
	}

	private void StopFlash()
	{
		if (flashHealth != false)
		{
			flashHealth = false;
			timer.Stop();
		}
	}

	private void Flash()
	{
		if (flashHealth)
		{
			 if (healthBar.TextureProgress == lowHealth)
			 {
				healthBar.TextureProgress = flashHealthTexture;
			 }
			 else
			 {
				healthBar.TextureProgress = lowHealth;
			 }
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
