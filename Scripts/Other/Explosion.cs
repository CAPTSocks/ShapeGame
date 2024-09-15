using Godot;
using System;

public partial class Explosion : AnimationPlayer
{

    private Timer timer;
    public override void _Ready()
    {
        timer = GetNode<Timer>("Timer");
        timer.WaitTime = .5f;
        this.Play("Explode");
        timer.Start(); 
    }

    private void DeleteExplosion()
    {
        this.QueueFree();
    }
}
