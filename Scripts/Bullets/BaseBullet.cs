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

    public override void _Ready()
    {
		timer = GetNode<Timer>("Timer");
        timer.Start();
    }

	private void OnTimerTimeout()
	{
		QueueFree();
	}

    public void setMoveDirection(Vector2 spawnPos, Vector2 moveTowardsPos)
	{
		Position = spawnPos;
		moveDirection = Position.DirectionTo(moveTowardsPos);
	}

	public override void _PhysicsProcess(double delta)
	{
		Velocity = moveDirection * speed;
		MoveAndSlide();
	}
}
