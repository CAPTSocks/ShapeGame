using Godot;
using System;

public partial class AAEnemy : BaseEnemy
{
	private Sprite2D turretSprite;

    public override void _Ready()
    {
        base._Ready();
        turretSprite = GetNode<Sprite2D>("Body/TurretSprite");
        bulletSpawn = GetNode<Node2D>("Body/TurretSprite/BulletSpawnNode");
        
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        turretSprite.LookAt(target.GlobalPosition);
		turretSprite.RotationDegrees -= 90; 
    }
}
