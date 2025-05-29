using Godot;
using System;

public partial class PlaneEnemy : BaseEnemy
{
    public override void _Ready()
    {
        base._Ready();
        animPlayer.Play("PlaneAnimation");
    }
}
