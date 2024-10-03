using Godot;
using System;

[GlobalClass]
public partial class ScoreObject : Resource
{
    [Export] public string name = new string("ABC");
    [Export] public int score = 0000;
}
