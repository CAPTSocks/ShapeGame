using Godot;
using System;

public partial class HelicopterEnemy : BaseEnemy
{

    public override void _Ready()
    {
        base._Ready();
        bulletSpawn = GetNode<Node2D>("Body/BulletSpawnNode");
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }
}
