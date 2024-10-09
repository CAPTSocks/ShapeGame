using Godot;
using System;

public partial class ScrollingBG : ParallaxBackground
{
    public override void _Process(double delta)
    {
        ScrollBaseOffset = new Vector2(ScrollBaseOffset.X, ScrollBaseOffset.Y + (float)(200 * delta));
    }
}
