using Godot;

public partial class PlayerController : CharacterBody2D
{

	private Vector2 startPressPos;
	private Vector2 endPressPos;
	private Vector2 moveToPos;
	private bool canMove = false;
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

	public override void _Ready()
	{
		GD.Print("test");
		shootTimer = GetNode<Timer>("ShootTimer");
		anim = GetNode<AnimationPlayer>("AnimationPlayer");
	}

	private void Timeout()
	{
		// GD.Print("Time up");
		// shootTimer.Stop();
		// swipe = true;
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

			if (press.IsActionReleased("Swipe"))
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
		newBullet.SetupBullet(Position, pos, bulletDamage, bulletSpeed);
		GetParent().AddChild(newBullet);
	}

	private void Move()
	{
		//Move Left
		if (startPressPos.X > endPressPos.X)
		{
			moveToPos = new Vector2(this.Position.X - moveDistance, this.Position.Y);
			canMove = true;
			anim.Play("TurnLeft");
		}
		
		//Move Right
		if (startPressPos.X < endPressPos.X)
		{
			moveToPos = new Vector2(this.Position.X + moveDistance, this.Position.Y);
			canMove = true;
			
			anim.Play("TurnRight");
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


