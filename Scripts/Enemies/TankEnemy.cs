using Godot;
using System;

public partial class TankEnemy : BaseEnemy
{
	private Sprite2D turretSprite;

    public override void _Ready()
    {
        base._Ready();
        turretSprite = GetNode<Sprite2D>("TankBody/TankTurret");
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        turretSprite.LookAt(target.GlobalPosition);
		turretSprite.RotationDegrees += 90; 
    }
}
