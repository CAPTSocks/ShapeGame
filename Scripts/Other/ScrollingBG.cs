using Godot;
using System;

public partial class ScrollingBG : ParallaxBackground
{
    public override void _Process(double delta)
    {
        ScrollOffset = new Vector2(ScrollOffset.X, ScrollOffset.Y + (float)(200 * delta));
    }
}
