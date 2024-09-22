using Godot;
using System;
using System.Runtime.Serialization;

public partial class MotorcycleEnemy : BaseEnemy
{
    private ShapeCast2D shapeCast;
    private bool turnLeft = false;
    private bool hasTurned = false;
    private bool turnBack = false;
    private float startXPos;
    private Vector2 viewPortCenter;
    private Timer turnTimer;

    public override void _Ready()
    {
        base._Ready();
        shapeCast = GetNode<ShapeCast2D>("ShapeCast2D");
        startXPos = GlobalPosition.X;
        viewPortCenter = GetViewportRect().Size / 2;
        turnTimer = GetNode<Timer>("TurnTimer");
        bulletSpawn = GetNode<Node2D>("Body/BulletSpawn");

    }

    private void HandleAnimation(int animationToPlay)
    {
        switch (animationToPlay)
        {
            case 0:
                animPlayer.Play("TurnLeft");
                turnTimer.WaitTime = .25f;
                GD.Print(animationToPlay + " Animation played");
                turnTimer.Start();
                break;
            case 1:
                animPlayer.Play("TurnRight");
                turnTimer.WaitTime = .25f;
                GD.Print(animationToPlay + " Animation played");
                turnTimer.Start();
                break;
            case 2:
                animPlayer.Play("StraightFromLeft");
                GD.Print(animationToPlay + " Animation played");
                break;
            case 3:
                animPlayer.Play("StraightFromRight");
                GD.Print(animationToPlay + " Animation played");
                break;
        }
    }

    private void TurnTimerTimeOut()
    {
        if (turnLeft)
        {
            HandleAnimation(2);
          //  hasTurned = false;
        }
        else if (!turnLeft)
        {
            HandleAnimation(3);
          //  hasTurned = false;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;
        velocity = Vector2.Down * Speed;

        if (shapeCast.IsColliding())
        {
            if (startXPos > viewPortCenter.X)
            {
                velocity = new Vector2(-300, velocity.Y);
                turnLeft = true;
                if (hasTurned == false)
                {
                    HandleAnimation(0);
                    hasTurned = true;
                }
            }
            else
            {
                velocity = new Vector2(300, velocity.Y);
                turnLeft = false;
                if (hasTurned == false)
                {
                    HandleAnimation(1);
                    hasTurned = true;
                }
            }
        }
        else 
        {
            if (hasTurned == true)
            {
                hasTurned = false;
            }
        }

        Velocity = velocity;
        MoveAndSlide();
    }
}
