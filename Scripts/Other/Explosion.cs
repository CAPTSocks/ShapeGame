using Godot;
using System;

public partial class Explosion : AnimationPlayer
{

    private Timer timer;
    public override void _Ready()
    {
        this.Play("Explode");
    }

    private void DeleteExplosion()
    {
        this.GetParent().QueueFree();
    }
}
