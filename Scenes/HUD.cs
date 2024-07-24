using Godot;
using System;

public partial class HUD : CanvasLayer
{
	private RichTextLabel timeText, scoreLabel;
	private int score;
	private double timeElapsed = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		timeText = GetNode<RichTextLabel>("Time");
		scoreLabel = GetNode<RichTextLabel>("Score");
		var gm = (GM)GetNode("/root/Gm");
		gm.ScoreUpdate += ChangeScore;
	}

	private void ChangeScore(int points)
	{
		score += points;
		scoreLabel.Text = "[center]" + score.ToString();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		timeElapsed += delta;
		TimeSpan time = TimeSpan.FromSeconds(timeElapsed);
		string timeString = $"{(int)time.TotalMinutes}:{time.Seconds:00}";
		timeText.Text = timeString;
	}
}
