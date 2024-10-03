using Godot;
using System;

public partial class GM : Node
{
	private int score = 0;
	private RichTextLabel scoreLabel;
	private DumbAss dmitri = new DumbAss(); 
	[Signal] public delegate void ScoreUpdateEventHandler (int points);
	private HighScoreResource scores;

    public override void _Ready()
    {
        scores = (HighScoreResource)GD.Load("res://Resources/HighScoresResource.tres");
    }

	private void gameOver()
	{
		
	}
}
