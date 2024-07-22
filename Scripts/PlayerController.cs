using Godot;

public partial class PlayerController : CharacterBody2D
{

	private Vector2 startPressPos;
	private Vector2 endPressPos;
	private Vector2 moveToPos;
	private bool canMove = false;
	private bool swipe = false;
	[Export]
	private PackedScene bullet;
	[Export]
	private float moveSpeed = 100f;
	[Export]
	private float moveDistance = 300f;

	private Timer shootTimer;

	public override void _Ready()
	{
		GD.Print("test");
		shootTimer = GetNode<Timer>("ShootTimer");
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
		newBullet.setMoveDirection(Position, pos);
		GetParent().AddChild(newBullet);
	}

	private void Move()
	{
		//Move Right
		if (startPressPos.X > endPressPos.X)
		{
			moveToPos = new Vector2(this.Position.X - moveDistance, this.Position.Y);
			canMove = true;
		}

		//Move Left
		if (startPressPos.X < endPressPos.X)
		{
			moveToPos = new Vector2(this.Position.X + moveDistance, this.Position.Y);
			canMove = true;
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


