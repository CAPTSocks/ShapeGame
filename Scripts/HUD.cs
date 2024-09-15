using Godot;
using System;

public partial class HUD : CanvasLayer
{
	private RichTextLabel timeText, scoreLabel;
	private int score;
	private double timeElapsed = 0;
	private Button restartButton;
	private GM gmRef;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		timeText = GetNode<RichTextLabel>("HUDContainer/Time");
		scoreLabel = GetNode<RichTextLabel>("HUDContainer/Score");
		restartButton = GetNode<Button>("RestartButton");
		gmRef = (GM)GetNode("/root/Gm");
		gmRef.ScoreUpdate += ChangeScore;
		restartButton.Visible = false;
	}

	private void ChangeScore(int points)
	{
		score += points;
		scoreLabel.Text = "[center]" + score.ToString();
		
	}

	private void EnableRestartButton()
	{
		restartButton.Visible = true;
	}

	private void RestartButtonPressed()
	{
		gmRef.RestartLevel();
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
