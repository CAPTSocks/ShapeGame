using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class HUD : CanvasLayer
{
	private RichTextLabel timeText, scoreLabel;
	private int score;
	private double timeElapsed = 0;
	private Button StartLevelButton;
	private GM gmRef;
	private SceneManager sceneManager;
	private bool startTime = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		timeText = GetNode<RichTextLabel>("HUDContainer/Time");

		StartLevelButton = GetNode<Button>("StartLevelButton");
		gmRef = (GM)GetNode("/root/Gm");
		gmRef.ScoreUpdate += ChangeScore;
		gmRef.StartGame += StartGame;
		scoreLabel = GetNode<RichTextLabel>("HUDContainer/Score");
		sceneManager = (SceneManager)GetNode("/root/SceneManager");
	}

	private void StartGame()
	{
		score = 0;
		startTime = true;
	}

	private void ChangeScore(int points)
	{
		score += points;
		scoreLabel.Text = "[center]" + score.ToString();
	}

	private void EnableRestartButton()
	{
		//restartButton.Visible = true;
	}

	private void StartGameButtonPressed()
	{
		gmRef.StartTheGame();
		StartLevelButton.Visible = false;
	}

	public override void _ExitTree()
	{
		gmRef.ScoreUpdate -= ChangeScore;
		gmRef.StartGame -= StartGame;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (startTime)
		{
			timeElapsed += delta;
			TimeSpan time = TimeSpan.FromSeconds(timeElapsed);
			string timeString = "[center]" + $"{(int)time.TotalMinutes}:{time.Seconds:00}";
			timeText.Text = timeString;
		}
	}
}
