using Godot;
using System;

public partial class RotorRotate : Node2D
{
    [Export]
    private float rotateSpeed = 100f;

    public override void _PhysicsProcess(double delta)
    {
        RotationDegrees += rotateSpeed * (float)delta;
    }
}
