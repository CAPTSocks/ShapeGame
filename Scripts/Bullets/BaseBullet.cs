using Godot;
using System;

public partial class BaseBullet : CharacterBody2D
{
	[Export]
	protected float speed = 1400.0f;
	[Export]
	protected int damage = -10;
	protected Vector2 moveDirection = new Vector2(0,0); 
	protected Timer timer;
	protected BulletTrail currentTrail;

    public override void _Ready()
    {
		timer = GetNode<Timer>("Timer");
        timer.Start();
		
    }

	private void OnTimerTimeout()
	{
		QueueFree();
	}

    public void SetupBullet(Vector2 spawnPos, Vector2 moveTowardsPos, int newDamage, float newSpeed)
	{
		Position = spawnPos;
		moveDirection = Position.DirectionTo(moveTowardsPos);
		var rot = (GlobalPosition - moveTowardsPos).Normalized();
		Rotation = rot.Angle() - 1.578f;
		GD.Print(RotationDegrees);
		damage = newDamage;
		speed = newSpeed;
		MakeTrail();
		
	}

	protected void MakeTrail()
	{
		if (currentTrail != null)
		{
			//currentTrail.Stop();
		}
		currentTrail = new BulletTrail().Setup();
		currentTrail.parent = this;
		AddChild(currentTrail);

	}

	public override void _PhysicsProcess(double delta)
	{
		//MakeTrail();
		Velocity = moveDirection * speed;
		MoveAndSlide();
	}
}
