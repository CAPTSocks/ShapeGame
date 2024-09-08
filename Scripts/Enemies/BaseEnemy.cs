using System;
using Godot;

public partial class BaseEnemy : CharacterBody2D
{
	[Export] private float Speed = 300.0f;
	[Export] protected int damage = -5, scoreAmount = 25;
	[Export] private float bulletSpeed = 1000;
	[Export] private float shotDelay = 1f;
	[Export] private PackedScene bullet;
	protected Node2D target, bulletSpawn;
	private PlayerHealthComponent healthComponent;
	private Timer timer;
	private bool FirstSetup = true, reachedEnd = false;
	private RandomNumberGenerator random = new RandomNumberGenerator();
	private AnimationPlayer animPlayer;
	private enum EnemyStates
	{
		started,
		shooting,
		ended
	}

	public int ScoreAmount { get {return scoreAmount;} }
	private EnemyStates currentState = EnemyStates.started;

	public override void _Ready()
	{
		timer = GetNode<Timer>("Timer");
		target = GetParent().GetNode<CharacterBody2D>("Player");
		animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		SetupEnemy();
	}

	public void SetupEnemy()
	{
		timer.WaitTime = random.RandfRange(1.5f, 2.5f);
		timer.Start();
	}

	protected virtual void Shoot()
	{
		PlayShootAnimation(); 
		var newBullet = bullet.Instantiate<EnemyBullet>();
		if (bulletSpawn != null)
		{
		newBullet.SetupBullet(bulletSpawn.GlobalPosition, target.Position, damage, bulletSpeed);
		}
		else 
		{
			newBullet.SetupBullet(this.GlobalPosition, target.Position, damage, bulletSpeed);
		}
		GetParent().AddChild(newBullet);
	}

	private void PlayShootAnimation()
	{
		animPlayer.Play("SotExplosion");
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
				var gm = (GM)GetNode("/root/Gm");
				gm.EmitSignal("ScoreUpdate", -25);
				QueueFree();
				break;
		}
	}

	public void ReachedEndOfScreen()
	{
		timer.Stop();
		currentState = EnemyStates.ended;
		timer.WaitTime = 4f;
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
