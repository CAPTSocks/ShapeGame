using System;
using System.Diagnostics;
using Godot;

public partial class BaseEnemy : CharacterBody2D
{
	[Export]
	private float Speed = 300.0f;
	[Export] private float shotDelay = 1f;
	[Export]
	private PackedScene bullet;
	private Node2D target;
	private Timer timer;
	private bool FirstSetup = true, reachedEnd = false;
	private RandomNumberGenerator random = new RandomNumberGenerator();
	private enum EnemyStates
	{
		started,
		shooting,
		ended
	}
	private EnemyStates currentState = EnemyStates.started;

	public override void _Ready()
	{
		timer = GetNode<Timer>("Timer");
		target = GetParent().GetNode<CharacterBody2D>("Player");
		SetupEnemy();
	}

	public void SetupEnemy()
	{
		timer.WaitTime = random.RandfRange(1.5f, 5f);
		GD.Print(timer.WaitTime);
		timer.Start();
	}

	private void Shoot()
	{
		var newBullet = bullet.Instantiate<EnemyBullet>();
		newBullet.setMoveDirection(Position, target.Position);
		GetParent().AddChild(newBullet);
	}

	private void TimeOut()
	{
		switch (currentState)
		{

			case EnemyStates.started:
				timer.WaitTime = shotDelay;
				Shoot();
				currentState = EnemyStates.shooting;
				break;

			case EnemyStates.shooting:
				Shoot();
				break;

			case EnemyStates.ended:
				GD.Print("Im die");
				QueueFree();
				break;
		}
	}

	public void ReachedEndOfScreen()
	{
		timer.Stop();
		currentState = EnemyStates.ended;
		timer.WaitTime = 5f;
		timer.Start();
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		velocity = Vector2.Down * Speed;

		Velocity = velocity;
		MoveAndSlide();
	}
}
