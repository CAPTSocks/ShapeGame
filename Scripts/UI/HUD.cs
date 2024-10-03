using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class HUD : CanvasLayer
{
	private RichTextLabel timeText, scoreLabel;
	private int score;
	private double timeElapsed = 0;
	private Button restartButton;
	private GM gmRef;
	private SceneManager sceneManager; 
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		timeText = GetNode<RichTextLabel>("HUDContainer/Time");
		
		restartButton = GetNode<Button>("RestartButton");
		gmRef = (GM)GetNode("/root/Gm");
		gmRef.ScoreUpdate += ChangeScore;
		scoreLabel = GetNode<RichTextLabel>("HUDContainer/Score");
		sceneManager = (SceneManager)GetNode("/root/SceneManager");

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
		restartButton.Visible = false;
		sceneManager.RestartLevel();
	}

    public override void _ExitTree()
    {
       	gmRef.ScoreUpdate -= ChangeScore;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		timeElapsed += delta;
		TimeSpan time = TimeSpan.FromSeconds(timeElapsed);
		string timeString = "[center]" + $"{(int)time.TotalMinutes}:{time.Seconds:00}";
		timeText.Text = timeString;
	}
}
