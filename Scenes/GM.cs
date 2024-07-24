using Godot;
using System;

public partial class GM : Node
{
	private int score = 0;
	private RichTextLabel scoreLabel;
	private DumbAss dmitri = new DumbAss(); 
	[Signal] public delegate void ScoreUpdateEventHandler (int points);
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
