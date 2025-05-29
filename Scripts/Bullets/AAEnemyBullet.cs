using Godot;
using System;
using System.ComponentModel;

public partial class AAEnemyBullet : EnemyBullet
{
	private CollisionShape2D cs;
	private float playerYPos = 0;
	private bool hasExpanded = false; 
	private AnimationPlayer ap;
	private Timer timer;
	private Sprite2D bulletSprite;
	[Export] private float scaleChangeSize = 5;
	[Export] private float expandSpeed = 0.5f;
    public override void _Ready()
    {
        base._Ready();
		cs = GetNode<CollisionShape2D>("Area2D/CollisionShape2D");
		ap = GetNode<AnimationPlayer>("AnimationPlayer");
		bulletSprite = GetNode<Sprite2D>("Sprite2D");
		timer = GetNode<Timer>("Timer");
		playerYPos = GetTree().Root.GetNode<CharacterBody2D>("Main/Player").Position.Y; 
    }

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

	// private async void Expand()
	// {
	// 	GD.Print("Expand!");
	// 	speed = 0; 
	// 	var tween = GetTree().CreateTween();
	// 	tween.TweenProperty(cs,"scale:x", scaleChangeSize, expandSpeed); 
	// 	tween.Parallel().TweenProperty(cs,"scale:y", scaleChangeSize, expandSpeed); 
	// 	GD.Print("Before Tween");
	// 	await ToSignal(tween, Tween.SignalName.Finished);
	// 	GD.Print("AfterTween");
	// 	QueueFree(); 
	// }

	private void PlayExplodeAnimation()
	{
		speed = 0;
		bulletSprite.Visible = false;
		ap.Play("Explode");
		timer.WaitTime = ap.CurrentAnimationLength + .05;
		timer.Start();
	}

	private void Timeout()
	{
		QueueFree();
	}

    public override void _Process(double delta)
	{
		if (Position.Y >= playerYPos && hasExpanded == false)
		{
			//Expand();
			PlayExplodeAnimation();
			hasExpanded = true;
		}
	}
}
