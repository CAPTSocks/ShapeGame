using Godot;
using System;
using System.Xml.Resolvers;
using System.Xml.Schema;

public partial class BulletTrail : Line2D
{
    private int maxPoints =  50;
    private Curve2D curve = new Curve2D();
    public Node2D parent;
    private Timer timer;

    public override void _Ready()
    {
        timer = GetNode<Timer>("Timer");
        timer.Start(); 
    }

    public BulletTrail Setup()
    {
        var scene = GD.Load<PackedScene>("res://Scenes/Bullets/BulletTrail.tscn");
        return (BulletTrail)scene.Instantiate();
    }

    public async void Stop()
    {
        SetProcess(false);
        var tw = GetTree().CreateTween();
        tw.TweenProperty(this, "modulate:a", 0.0, .2);
        GD.Print("Before Tween");
        await ToSignal(tw, Tween.SignalName.Finished);
        GD.Print("AfterTween");
        QueueFree();
    }

    private void timerOut() 
    {
        // if (parent != null)
        // {
        //     curve.AddPoint(parent.GlobalPosition);
        //     if (curve.GetBakedPoints().Length > maxPoints)
        //     {
        //         curve.RemovePoint(0);
        //         Stop();
        //     }
        //     Points = curve.GetBakedPoints();
        // }
    }
    public override void _Process(double delta)
    {
        if (parent != null)
        {
            curve.AddPoint(parent.GlobalPosition);
            if (curve.GetBakedPoints().Length > maxPoints)
            {
                curve.RemovePoint(0);
                //Stop();
            }
            Points = curve.GetBakedPoints();
        }
    }
}
