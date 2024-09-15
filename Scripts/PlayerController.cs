using System.Text.Unicode;
using Godot;

public partial class PlayerController : CharacterBody2D
{

	private Vector2 startPressPos;
	private Vector2 endPressPos;
	private Vector2 moveToPos;
	private bool canMove = false;
	private bool isDead = false;
	private bool swipe = false;
	private AnimationPlayer anim;
	[Export]
	private PackedScene bullet;
	[Export]
	private float moveSpeed = 100f;
	[Export]
	private float moveDistance = 300f;
	[Export] private int bulletDamage = -10;
	[Export] private float bulletSpeed = 1000;

	private Timer shootTimer;
	private Node2D bulletSpawn;
	private int movePos = 0;
	private PlayerHealthComponent healthComponentAccess;

	public override void _Ready()
	{
		shootTimer = GetNode<Timer>("ShootTimer");
		anim = GetNode<AnimationPlayer>("AnimationPlayer");
		bulletSpawn = GetNode<Node2D>("BulletSpawn");
		healthComponentAccess = GetNode<PlayerHealthComponent>("HealthComponent");

		healthComponentAccess.PlayerDied += PlayerDied;
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton press)
		{
			if (press.IsActionPressed("Swipe"))
			{
			//	shootTimer.Start();
				startPressPos = press.Position;
			}

			if (press.IsActionReleased("Swipe") && !isDead)
			{
				endPressPos = press.Position;
				if (startPressPos.DistanceTo(endPressPos) >= 250)
				{
					Move();
					swipe = false;
				//	shootTimer.Stop();
				}
				else
				{
					Shoot(press.Position);
					//shootTimer.Stop();
				}
			}
		}
	}

	private void Shoot(Vector2 pos)
	{
		var newBullet = bullet.Instantiate<PlayerBullet>();
		newBullet.SetupBullet(bulletSpawn.GlobalPosition, pos, bulletDamage, bulletSpeed);
		GetParent().AddChild(newBullet);
	}

	private void Move()
	{
		//Move Left
		if (startPressPos.X > endPressPos.X && movePos > -2)
		{
			moveToPos = new Vector2(this.Position.X - moveDistance, this.Position.Y);
			canMove = true;
			anim.Play("TurnLeft");
			movePos --;
		}
		
		//Move Right
		if (startPressPos.X < endPressPos.X && movePos < 2)
		{
			moveToPos = new Vector2(this.Position.X + moveDistance, this.Position.Y);
			canMove = true;
			anim.Play("TurnRight");
			movePos ++;
		}
	}

	private void animationFinished(StringName name)
	{
		if (name == "TurnRight")
		{
			anim.Play("StraightFromRight");
		}
		else if (name == "TurnLeft")
		{
			anim.Play("StraightFromLeft");
		}
	}

	private void PlayerDied()
	{
		GD.Print("Player is Dead");
		isDead = true;
	}

	private void OnBodyEntered(Node body)
	{
		GD.Print("test");
		if (body.IsInGroup("AirEnemy"))
		{
			
			var enemy = body.GetNode<EnemyHealthComponent>("HealthComp");
			if (enemy != null)
			{
				enemy.IncrementHealth(-100);
			}
			
			if (healthComponentAccess != null)
			{
				healthComponentAccess.IncrementHealth(-25);
			}
		}
	}

	public override void _Process(double delta)
	{
		if (canMove)
		{
			Position = new Vector2(Mathf.MoveToward(Position.X, moveToPos.X, moveSpeed * (float)delta), Position.Y);
		}

		if (Position.X == moveToPos.X)
		{
			canMove = false;
		}

	}
}


